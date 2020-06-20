using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Scripting.Hosting;
using PracticeCSharpPWA.Client.Shared;
using PracticeCSharpPWA.Shared;
using PracticeCSharpPWA.Shared.Models.CodeEditorModels;

namespace PracticeCSharpPWA.Client.Pages.CodePractice
{
    public partial class ReplShell : ComponentBase, IDisposable
    {
        [Inject]
        protected NavigationManager NavigationManager { get; set; }
        [Inject]
        public CodeEditorService CodeEditorService { get;  set; }
        [Inject]
        protected AppStateService AppStateService { get; set; }
        public string InfoOutput { get; set; } = "";
        public string Input { get; set; } = "";
        protected CSharpCompilation runningCompilation;
        protected IEnumerable<MetadataReference> References;
        protected object[] submissionStates = { null, null };
        protected int submissionIndex = 0;
        protected List<string> history = new List<string>();
        protected int historyIndex = 0;
        protected string CodeOutput { get; set; }
        [Parameter]
        public EventCallback<string> CodeOutputChanged { get; set; }
        
        protected override async Task OnInitializedAsync()
        {
            var refs = AppDomain.CurrentDomain.GetAssemblies();
            var Http = new HttpClient {BaseAddress = new Uri(NavigationManager.BaseUri)};

            var assemblyRefs = new List<MetadataReference>();

            foreach (var reference in refs.Where(x => !x.IsDynamic && !string.IsNullOrWhiteSpace(x.Location) && (x.FullName.Contains("System") || x.FullName.Contains("mscorlib") || x.FullName.Contains("netstandard"))))
            {
                var stream = await Http.GetStreamAsync($"_framework/_bin/{reference.Location}");
                assemblyRefs.Add(MetadataReference.CreateFromStream(stream));
            }
            References = AppStateService.References;
            AppStateService.OnChange += StateHasChanged;
            CodeEditorService.Evaluate += SubmitMonaco;

        }
        protected void OnKeyDown(KeyboardEventArgs e)
        {
            switch (e.Key)
            {
                case "ArrowUp" when historyIndex > 0:
                    historyIndex--;
                    Input = history[historyIndex];
                    break;
                case "ArrowDown" when historyIndex + 1 < history.Count:
                    historyIndex++;
                    Input = history[historyIndex];
                    break;  
                case "Escape":
                    Input = "";
                    historyIndex = history.Count;
                    break;
            }
        }
        public async Task SubmitMonaco()
        {
            var code = CodeEditorService.MonacoCode;
            await RunSubmission(code);
            StateHasChanged();
        }
        public async Task Run(KeyboardEventArgs e)
        {
            if (e.Key != "Enter")
                return;

            var code = Input;
            if (!string.IsNullOrEmpty(code))
                history.Add(code);
            historyIndex = history.Count;
            Input = "";
            CodeComponentBase.HistoryShared = history;
            await RunSubmission(code);
        }
        public async Task RunSubmission(string code)
        {
            InfoOutput += $@"<br /><span class=""info"">{HttpUtility.HtmlEncode(code)}</span>";


            var previousOut = Console.Out;
            try
            {
                if (TryCompile(code, out var script, out var errorDiagnostics))
                {
                    var writer = new StringWriter();
                    Console.SetOut(writer);

                    var entryPoint = runningCompilation.GetEntryPoint(CancellationToken.None);
                    var type = script.GetType($"{entryPoint.ContainingNamespace.MetadataName}.{entryPoint.ContainingType.MetadataName}");
                    var entryPointMethod = type.GetMethod(entryPoint.MetadataName);

                    var submission = (Func<object[], Task>)entryPointMethod.CreateDelegate(typeof(Func<object[], Task>));

                    if (submissionIndex >= submissionStates.Length)
                    {
                        Array.Resize(ref submissionStates, Math.Max(submissionIndex, submissionStates.Length * 2));
                    }

                    var returnValue = await (Task<object>)submission(submissionStates);
                    if (returnValue != null)
                    {
                        Console.WriteLine(CSharpObjectFormatter.Instance.FormatObject(returnValue));
                    }

                    CodeOutput = writer.ToString();
                    await CodeOutputChanged.InvokeAsync(CodeOutput);
                    var output = HttpUtility.HtmlEncode(writer.ToString());
                    if (!string.IsNullOrWhiteSpace(output))
                    {
                        InfoOutput += $"<br />{output}";
                    }
                }
                else
                {
                    var errorOutput = "";
                    foreach (var diag in errorDiagnostics)
                    {
                        InfoOutput += $@"<br / ><span class=""error"">CompileError: {HttpUtility.HtmlEncode(diag)}</span>";
                        errorOutput += HttpUtility.HtmlEncode(diag);
                    }

                    CodeOutput = $"COMPILE ERROR: {errorOutput}";
                    await CodeOutputChanged.InvokeAsync(CodeOutput);
                }
            }
            catch (Exception ex)
            {
                InfoOutput += $@"<br /><span class=""error"">Catch: {HttpUtility.HtmlEncode(CSharpObjectFormatter.Instance.FormatException(ex))}</span>";
            }
            finally
            {
                Console.SetOut(previousOut);
            }
        }


        //Tries to compile, if successful, it outputs the DLL Assembly. If unsuccessful, it will output the error message
        protected bool TryCompile(string source, out Assembly assembly, out IEnumerable<Diagnostic> errorDiagnostics)
        {
            assembly = null;
            var scriptCompilation = CSharpCompilation.CreateScriptCompilation(
                Path.GetRandomFileName(),
                CSharpSyntaxTree.ParseText(source, CSharpParseOptions.Default.WithKind(SourceCodeKind.Script).WithLanguageVersion(LanguageVersion.Preview)),
                References,
                new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary, usings: new[]
                {
                    "System",
                    "System.IO",
                    "System.Collections.Generic",
                    "System.Collections",
                    "System.Console",
                    "System.Diagnostics",
                    "System.Dynamic",
                    "System.Linq",
                    "System.Linq.Expressions",
                    "System.Net.Http",
                    "System.Text",
                    "System.Threading.Tasks"
                }),
                runningCompilation
            );

            errorDiagnostics = scriptCompilation.GetDiagnostics().Where(x => x.Severity == DiagnosticSeverity.Error);
            if (errorDiagnostics.Any())
            {
                return false;
            }

            using var peStream = new MemoryStream();
            var emitResult = scriptCompilation.Emit(peStream);

            if (!emitResult.Success) return false;
            submissionIndex++;
            runningCompilation = scriptCompilation;
            assembly = Assembly.Load(peStream.ToArray());
            return true;

        }

        public void Dispose()
        {
            AppStateService.OnChange -= StateHasChanged;
            CodeEditorService.Evaluate -= SubmitMonaco;
        }
    }
}
