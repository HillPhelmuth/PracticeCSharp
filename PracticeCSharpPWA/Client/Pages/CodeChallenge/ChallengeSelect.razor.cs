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
        public Puzzle selectedPuzzle { get; set; }
        private readonly Puzzle[] puzzles = Enum.GetValues(typeof(Puzzle)).Cast<Puzzle>().ToArray();
        private string description;
        private string examples;
        private bool isPuzzleSelected;
        public Challenge selectedChallenge { get; set; }
        [Parameter]
        public CodeChallenges CodeChallenges { get; set; }
        [Parameter] 
        public EventCallback<string> OnPuzzleChanged { get; set; }

        //protected async Task SelectPuzzle()
        //{
        //    await Task.Delay(200);
        //    description = selectedChallenge.Description;
        //    examples = selectedChallenge.Examples;
        //    isPuzzleSelected = true;
        //    await OnPuzzleChanged.InvokeAsync(selectedChallenge);
        //    Console.WriteLine($"Puzzle selected: {selectedChallenge.Name}");
        //    StateHasChanged();
            
        //}
        protected Task SelectPuzzle(Challenge challenge)
        {
            selectedChallenge = challenge;
            var challengeName = challenge.Name;
            description = selectedChallenge.Description;
            examples = selectedChallenge.Examples;
            isPuzzleSelected = true;
            OnPuzzleChanged.InvokeAsync(challengeName);
            Console.WriteLine($"Puzzle selected: {selectedChallenge.Name}");
            StateHasChanged();
            return Task.CompletedTask;

        }
    }
}
