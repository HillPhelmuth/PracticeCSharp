using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PracticeCSharpPWA.Client.Shared;
using PracticeCSharpPWA.Shared.ExtensionMethods;
using PracticeCSharpPWA.Shared.Models.FlashCardModels;

namespace PracticeCSharpPWA.Client.Pages.CardQuiz
{
    public class CardReviewModel : FlashCardComponentBase, IDisposable
    {
        
        protected Card DisplayCard { get; set; }
        protected List<AnswerData> Answers { get; set; }

        protected int trackNext = 1;
        protected int correctTotal;
        protected int wrongTotal;
        protected string message;
        protected bool enabled;
        protected bool isReady;

        protected override async Task OnInitializedAsync()
        {
            await UpdateState();
            DeckState.OnChange += UpdateState;
            AddTestAnswers();
            DisplayCard = DeckCards[0];
            Answers = DisplayCard.DisplayAnswers;
            Answers.Shuffle();
        }

        private void AddTestAnswers()
        {
            DeckCards = DeckCards.AddAltAnswers();
            DeckCards.Shuffle();
            if (DeckCards == null)
                return;
            isReady = true;
            enabled = true;
        }

        protected void GetNext()
        {
            if (DeckCards.Count <= trackNext)
            {
                return;
            }
            enabled = true;
            DisplayCard = DeckCards[trackNext];
            trackNext++;
            Answers = DisplayCard.DisplayAnswers;
            Answers.Shuffle();
            StateHasChanged();
        }
        protected async Task CheckAnswer(AnswerData answer)
        {
            bool isCorrect;
            if (answer.Answer == DisplayCard.Answer)
            {
                answer.IsCorrect = true;
                answer.IsIncorrect = false;
                answer.CssClass = "correct";
                correctTotal++;
                isCorrect = true;
            }
            else
            {
                answer.IsCorrect = false;
                answer.IsIncorrect = true;
                answer.CssClass = "wrong";
                wrongTotal++;
                isCorrect = false;
            }
            await DeckState.UpdateStats(isCorrect);
            var stats = DeckState.DeckStats;

            StateHasChanged();
        }
        protected void CardAnimEnd() => enabled = false;
        //protected void WrongAnimEnd(AnswerData answer) => answer.IsIncorrect = !answer.IsIncorrect;
        //protected void CorrectAnimEnd(AnswerData answer) => answer.IsCorrect = !answer.IsCorrect;

        public void Dispose() => DeckState.OnChange -= UpdateState;
    }
}
