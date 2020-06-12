using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using PracticeCSharpPWA.Shared.Models.CodeEditorModels;

namespace PracticeCSharpPWA.Client.Pages.CodePractice
{
    public partial class CodeHome:IDisposable
    {
        [Inject]
        public CodeEditorService CodeEditorService { get; set; }
       
        protected bool importHistory;
        protected bool isAnimate = true;
        protected string CodeOutput;
        protected string codeSnippet;
        protected override Task OnInitializedAsync()
        {
            CodeEditorService.OnChange += StateHasChanged;
            return base.OnInitializedAsync();
        }
        protected void HandleOutputChange(string output)
        {
            CodeOutput = output;
            isAnimate = true;
            StateHasChanged();
        }

        protected Task UpdateCodeSnippet()
        {
            CodeEditorService.UpdateSnippet(codeSnippet);
            StateHasChanged();
            
            return Task.CompletedTask;
        }

        protected void ToggleAnimation() => isAnimate = false;

        public void Dispose() => CodeEditorService.OnChange -= StateHasChanged;
    }
}
