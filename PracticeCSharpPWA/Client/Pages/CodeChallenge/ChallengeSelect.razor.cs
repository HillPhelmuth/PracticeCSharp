using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using PracticeCSharpPWA.Shared.Models.CodeEditorModels;

namespace PracticeCSharpPWA.Client.Pages.CodeChallenge
{
    public partial class ChallengeSelect
    {
        private string description;
        private string examples;
        private bool isChallengeSelected;
        public Challenge selectedChallenge { get; set; }
        [Parameter]
        public CodeChallenges CodeChallenges { get; set; }
        [Parameter]
        public EventCallback<string> OnPuzzleChanged { get; set; }


        protected Task SelectChallenge(Challenge challenge)
        {
            selectedChallenge = challenge;
            var challengeName = challenge.Name;
            description = selectedChallenge.Description;
            examples = selectedChallenge.Examples;
            isChallengeSelected = true;
            OnPuzzleChanged.InvokeAsync(challengeName);
            Console.WriteLine($"Puzzle selected: {selectedChallenge.Name}");
            StateHasChanged();
            return Task.CompletedTask;

        }
    }
}
