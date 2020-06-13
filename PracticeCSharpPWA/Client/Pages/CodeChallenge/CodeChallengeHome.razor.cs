using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.CodeAnalysis;
using PracticeCSharpPWA.Shared.Models.CodeEditorModels;

namespace PracticeCSharpPWA.Client.Pages.CodeChallenge
{
    public partial class CodeChallengeHome
    {
        [Inject] 
        public CodeEditorService CodeEditorService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public CompilerService CompilerService { get; set; }

        protected Puzzle selectedPuzzle;
        protected bool isAnimate = true;
        protected string CodeOutput;
        protected string CodeSnippet;
        //protected string CodeSubmit;
        bool takeChallenge = false;
        protected bool isPuzzleSucceed;
        protected bool isPuzzleFail;
        protected IEnumerable<MetadataReference> References;

        protected override async Task OnInitializedAsync()
        {
            var refs = AppDomain.CurrentDomain.GetAssemblies();
            var client = new HttpClient { BaseAddress = new Uri(NavigationManager.BaseUri) };

            var assemblyRefs = new List<MetadataReference>();

            foreach (var reference in refs.Where(x => !x.IsDynamic && !string.IsNullOrWhiteSpace(x.Location)))
            {
                var stream = await client.GetStreamAsync($"_framework/_bin/{reference.Location}");
                assemblyRefs.Add(MetadataReference.CreateFromStream(stream));
            }
            References = assemblyRefs;
            Console.WriteLine($"refs: {References}");
            CodeEditorService.OnChange += StateHasChanged;
            
        }
        void SolveChallenge()
        {
            takeChallenge = !takeChallenge;
        }
        protected void HandleOutputChange(string output)
        {
            CodeOutput = output;
            isAnimate = true;
            StateHasChanged();
        }

        public async Task HandleCodeSubmit(string code)
        {
            switch (selectedPuzzle)
            {
                case Puzzle.Braces:
                {
                    var appendCode = code + "\nreturn validBraces(\"[()]\");";
                    var isPassTrue = await CompilerService.SubmitPuzzle(appendCode, References);
                    Console.WriteLine($"first run: {isPassTrue}");
                    appendCode = code + "\nreturn validBraces(\"[(])\");";
                    var isPassFalse = !(await CompilerService.SubmitPuzzle(appendCode, References));
                    Console.WriteLine($"second run: {isPassFalse}");
                    isPuzzleSucceed = isPassFalse && isPassTrue;
                    break;
                }
                case Puzzle.Prime:
                {
                    var appendCode = code + "\nreturn IsPrime(1);";
                    var is1False = !(await CompilerService.SubmitPuzzle(appendCode, References));
                    Console.WriteLine($"is1False: {is1False}");
                    appendCode = code + "\nreturn IsPrime(2);";
                    var is2True = await CompilerService.SubmitPuzzle(appendCode, References);
                    Console.WriteLine($"is2True: {is2True}");
                    appendCode = code + "\nreturn IsPrime(16);";
                    var is16False = !(await CompilerService.SubmitPuzzle(appendCode, References));
                    Console.WriteLine($"is16False: {is16False}");
                    appendCode = code + "\nreturn IsPrime(11);";
                    var is11True = await CompilerService.SubmitPuzzle(appendCode, References);
                    Console.WriteLine($"is11True: {is11True}");
                    isPuzzleSucceed = is1False && is2True && is16False && is11True;
                    break;
                }
                case Puzzle.Rot13:
                {
                    var appendCode = code + "\nreturn Rot13(\"Grfg\");";
                    var isGrfgTest = await CompilerService.SubmitPuzzle(appendCode, References, "Test");
                    Console.WriteLine($"isGrfgTest: {isGrfgTest}");
                    appendCode = code + "\nreturn Rot13(\"Grfgf\");";
                    var isGrfgfTests = await CompilerService.SubmitPuzzle(appendCode, References, "Tests");
                    Console.WriteLine($"isGrfgfTests: {isGrfgfTests}");
                    isPuzzleSucceed = isGrfgTest && isGrfgfTests;
                    break;
                }
            }

            isPuzzleFail = !isPuzzleSucceed;
            StateHasChanged();
        }

        protected Task HandlePuzzleChanged(Puzzle puzzle)
        {
            CodeSnippet = puzzle switch
            {
                Puzzle.Braces => CodeSnippets.BRACESPUZZLE,
                Puzzle.Prime => CodeSnippets.PRIMEPUZZLE,
                Puzzle.Rot13 => CodeSnippets.ROT13PUZZLE,
                _ => CodeSnippet
            };
            selectedPuzzle = puzzle;
            CodeEditorService.UpdateSnippet(CodeSnippet);
            return Task.CompletedTask;
        }

        protected void ChangePuzzle()
        {
            isPuzzleFail = false;
            isPuzzleSucceed = false;
            takeChallenge = false;
            StateHasChanged();
        }
        protected void ToggleAnimation() => isAnimate = false;
    }
}
