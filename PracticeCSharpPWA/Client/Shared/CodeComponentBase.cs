using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Components;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Scripting.Hosting;

namespace PracticeCSharpPWA.Client.Shared
{
    public class CodeComponentBase : ComponentBase
    {
        
        protected int submissionIndex;
        protected CSharpCompilation runningCompilation;
        protected object[] submissionStates;
        protected IEnumerable<MetadataReference> references;
        public string CodeOutput { get; set; }
        public static List<string> HistoryShared { get; set; }
        public string InfoOutput { get; set; }


        public async Task SubmitMonaco(string code)
        {
            await RunSubmission(code);
            StateHasChanged();
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

                    var returnValue = await ((Task<object>)submission(submissionStates));
                    if (returnValue != null)
                    {
                        Console.WriteLine(CSharpObjectFormatter.Instance.FormatObject(returnValue));
                    }

                    CodeOutput = writer.ToString();
                    var output = HttpUtility.HtmlEncode(writer.ToString());
                    if (!string.IsNullOrWhiteSpace(output))
                    {
                        InfoOutput += $"<br />{output}";
                    }
                }
                else
                {
                    foreach (var diag in errorDiagnostics)
                    {
                        InfoOutput += $@"<br / ><span class=""error"">CompileError: {HttpUtility.HtmlEncode(diag)}</span>";
                    }
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
                references,
                new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary, usings: new[]
                {
                    "System",
                    "System.IO",
                    "System.Collections.Generic",
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
    }
}
