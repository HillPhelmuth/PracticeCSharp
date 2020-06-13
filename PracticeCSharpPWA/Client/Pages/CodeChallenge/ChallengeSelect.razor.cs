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
        [Parameter] 
        public EventCallback<Puzzle> OnPuzzleChanged { get; set; }
        protected Task SelectPuzzle()
        {
            switch (selectedPuzzle)
            {
                case Puzzle.Braces:
                    description = ChallengeInfo.BracesDescription;
                    examples = ChallengeInfo.BracesExamples;
                    break;
                case Puzzle.Prime:
                    description = ChallengeInfo.PrimeDescription;
                    examples = ChallengeInfo.PrimeExamples;
                    break;
                case Puzzle.Rot13:
                    description = ChallengeInfo.Rot13Description;
                    examples = ChallengeInfo.Rot13Examples;
                    break;
            }
            isPuzzleSelected = true;
            OnPuzzleChanged.InvokeAsync(selectedPuzzle);
            StateHasChanged();
            return Task.CompletedTask;
        }
    }
}
