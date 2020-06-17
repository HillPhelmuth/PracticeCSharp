using System;
using System.Net.Http;
using System.Threading.Tasks;
using BlazorMonaco;
using BlazorMonaco.Bridge;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PracticeCSharpPWA.Shared.Models.CodeEditorModels;

namespace PracticeCSharpPWA.Client.Pages.CodePractice
{
    public partial class MonacoEdit : ComponentBase, IDisposable
    {
        [Inject]
        public IJSRuntime jsRuntime { get; set; }
        [Inject]
        protected HttpClient Http { get; set; }
        [Inject]
        public CodeEditorService CodeEditorService { get; set; }
        protected bool IsCodeReady { get; set; }
        protected MonacoEditor Editor { get; set; }
        protected string ValueToSet { get; set; }
        [Parameter]
        public EventCallback<string> OnCodeSubmit { get; set; }
        [Parameter]
        public string CodeSnippet { get; set; }
        [Parameter]
        public bool IsPuzzle { get; set; }

        protected override Task OnInitializedAsync()
        {
            Editor = new MonacoEditor();
            CodeEditorService.OnChange += StateHasChanged;
            CodeEditorService.OnSnippetChange += UpdateSnippet;
            return Task.CompletedTask;
        }
        public async Task SubmitCode()
        {
            var code = await Editor.GetValue();
            if (IsPuzzle)
            {
                await OnCodeSubmit.InvokeAsync(code);
                Console.WriteLine($"puzzle submission: {code}");
                return;
            }
            CodeEditorService.EvaluateCode(code);
        }

        protected async Task UpdateSnippet()
        {
            CodeSnippet = CodeEditorService.CodeSnippet;
            await Editor.SetValue(CodeSnippet);
            Console.WriteLine($"Event Fired - Editor updated to {await Editor.GetValue()}");
            StateHasChanged();
            //return Task.CompletedTask;
        }
       
        protected StandaloneEditorConstructionOptions EditorOptionsRoslyn(MonacoEditor editor)
        {
            return new StandaloneEditorConstructionOptions
            {
                AutomaticLayout = true,
                AutoIndent = true,
                HighlightActiveIndentGuide = true,
                Language = "csharp",
                Value = CodeSnippet ?? "private string MyProgram() \n" +
                        "{\n" +
                        "    string input = \"this does not\"; \n" +
                        "    string modify = input + \" suck!\"; \n" +
                        "    return modify;\n" +
                        "}\n" +
                        "return MyProgram();"
            };
        }

        
        protected async Task EditorOnDidInit(MonacoEditor editor)
        {
            await Editor.AddCommand((int)KeyMode.CtrlCmd | (int)KeyCode.KEY_H, (editor, keyCode) =>
            {
                Console.WriteLine("Ctrl+H : Initial editor command is triggered.");
            });
        }

        protected void OnContextMenu(EditorMouseEvent eventArg)
        {
            Console.WriteLine("OnContextMenu : " + System.Text.Json.JsonSerializer.Serialize(eventArg));
        }

        protected void Reset()
        {
            ValueToSet = "";
            IsCodeReady = false;
            StateHasChanged();
        }

        public void Dispose()
        {
            CodeEditorService.OnChange -= StateHasChanged;
            CodeEditorService.OnSnippetChange -= UpdateSnippet;
        }
    }
}
