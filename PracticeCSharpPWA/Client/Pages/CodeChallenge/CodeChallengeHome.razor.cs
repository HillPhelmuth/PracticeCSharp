﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorMonaco;
using BlazorMonaco.Bridge;
using Microsoft.AspNetCore.Components;
using Microsoft.CodeAnalysis;
using Newtonsoft.Json;
using PracticeCSharpPWA.Shared;
using PracticeCSharpPWA.Shared.Models.CodeEditorModels;

namespace PracticeCSharpPWA.Client.Pages.CodeChallenge
{
    public partial class CodeChallengeHome : IDisposable
    {
        [Inject]
        public CodeEditorService CodeEditorService { get; set; }
        [Inject]
        public CompilerService CompilerService { get; set; }
        [Inject]
        public AppStateService AppStateService { get; set; }
        
        public CodeChallenges CodeChallenges { get; set; }
        public Challenge selectedChallenge { get; set; }
       
        protected string CodeSnippet;
        bool takeChallenge = false;
        private bool isCodeCompiling;
        protected bool isChallengeSucceed;
        protected bool isChallengeFail;
        protected bool isChallengeReady;
        protected IEnumerable<MetadataReference> References;
        [Parameter] 
        public EventCallback<int> OnNotReady { get; set; }

        protected override async Task OnInitializedAsync()
        {
            References = AppStateService.References;
            Console.WriteLine($"refs: {References}");
            CodeChallenges = AppStateService.CodeChallenges;
            AppStateService.OnChange += StateHasChanged;
            if ((CodeChallenges?.Challenges) == null)
            {
                await OnNotReady.InvokeAsync(0);
                return;
            }
            isChallengeReady = true;
           
        }

        private void SolveChallenge() => takeChallenge = !takeChallenge;

        public async Task SubmitCode()
        {
            isCodeCompiling = true;
            StateHasChanged();
            await Task.Run(() =>
            {
                _ = HandleCodeSubmit();
            });
            //await HandleCodeSubmit();
        }
        public async Task HandleCodeSubmit()
        {
            var code = await Editor.GetValue();
            switch (selectedChallenge.Name)
            {
                case "Braces":
                    {
                        var appendCode = code + "\nreturn validBraces(\"[()]\");";
                        var isPassTrue = await CompilerService.SubmitPuzzle(appendCode, References);
                        Console.WriteLine($"first run: {isPassTrue}");
                        appendCode = code + "\nreturn validBraces(\"[(])\");";
                        var isPassFalse = !(await CompilerService.SubmitPuzzle(appendCode, References));
                        Console.WriteLine($"second run: {isPassFalse}");
                        isChallengeSucceed = isPassFalse && isPassTrue;
                        break;
                    }
                case "Prime":
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
                        isChallengeSucceed = is1False && is2True && is16False && is11True;
                        break;
                    }
                case "Rot13":
                    {
                        var appendCode = code + "\nreturn Rot13(\"Grfg\");";
                        var isGrfgTest = await CompilerService.SubmitPuzzle(appendCode, References, "Test");
                        Console.WriteLine($"isGrfgTest: {isGrfgTest}");
                        appendCode = code + "\nreturn Rot13(\"Grfgf\");";
                        var isGrfgfTests = await CompilerService.SubmitPuzzle(appendCode, References, "Tests");
                        Console.WriteLine($"isGrfgfTests: {isGrfgfTests}");
                        isChallengeSucceed = isGrfgTest && isGrfgfTests;
                        break;
                    }
                case "MultipleOf3or5":
                    {
                        var appendCode = code + "\nreturn Solution(10);";
                        var is10Equal23 = await CompilerService.SubmitPuzzle(appendCode, References, "23");
                        Console.WriteLine($"is10Equal23: {is10Equal23}");
                        isChallengeSucceed = is10Equal23;
                        break;
                    }
                case "AbbreviateName":
                    {
                        var appendCode = code + "\nreturn AbbrevName(\"Adam Holm\");";
                        var isAdamTest = await CompilerService.SubmitPuzzle(appendCode, References, "A.H");
                        Console.WriteLine($"isAdamTest: {isAdamTest}");
                        appendCode = code + "\nreturn AbbrevName(\"Kira Holm\");";
                        var isKiraTest = await CompilerService.SubmitPuzzle(appendCode, References, "K.H");
                        Console.WriteLine($"isKiraTest: {isKiraTest}");
                        isChallengeSucceed = isAdamTest && isKiraTest;
                        break;
                    }
            }

            isChallengeFail = !isChallengeSucceed;
            isCodeCompiling = false;
            StateHasChanged();
        }
        protected Task HandlePuzzleChanged(string challengeName)
        {
            Console.WriteLine($"Challenge from handler: {challengeName}");
            var challenge = CodeChallenges.Challenges.Find(x => x.Name == challengeName);
            CodeSnippet = challenge.Snippet;
            selectedChallenge = challenge;
            CodeEditorService.UpdateSnippet(CodeSnippet);
            Editor.SetValue(CodeSnippet);
            return Task.CompletedTask;
        }

        protected Task ShowAnswer()
        {
            CodeSnippet = selectedChallenge.Solution;
            Editor.SetValue(CodeSnippet);
            CodeEditorService.UpdateSnippet(CodeSnippet);
            return Task.CompletedTask;
        }

        protected void ChangePuzzle()
        {
            isChallengeFail = false;
            isChallengeSucceed = false;
            takeChallenge = false;
            StateHasChanged();
        }

        // Monaco Editor Settings
        protected MonacoEditor Editor { get; set; }
        protected StandaloneEditorConstructionOptions EditorOptionsPuzzle(MonacoEditor editor)
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

        public void Dispose() => AppStateService.OnChange -= StateHasChanged;

    }
}
