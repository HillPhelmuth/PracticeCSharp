using System;
using System.Net.Http.Json;
using System.Threading.Tasks;
using PracticeCSharpPWA.Client.Shared;
using PracticeCSharpPWA.Shared.Models.FlashCardModels;

namespace PracticeCSharpPWA.Client.Pages.CardQuiz
{
    public class UserPageModel : FlashCardComponentBase, IDisposable
    {
        protected string deckName = "no deck selected";
        protected string quizTitle;
        protected bool userHasDecks;
        protected bool addNewToggle;
        protected bool isHideList;
        protected bool isSelectDeck;
        protected bool isReview;

        protected async Task SelectDeck(string deckname)
        {
            SelectedDeck = await Http.GetFromJsonAsync<Deck>($"api/FlashCards/{deckname}");
            await DeckState.UpdateSelectedDeck(SelectedDeck);
            isHideList = true;
            DeckCards = DeckState.SelectedDeck?.Cards;
            quizTitle = $"Test your {SelectedDeck.Subject} Knowledge";
            isSelectDeck = true;
            StateHasChanged();
        }

        protected void Reset()
        {
            userHasDecks = false;
            addNewToggle = false;
            isHideList = false;
            isReview = false;
            isSelectDeck = false;
            deckName = "no deck selected";
            userHasDecks = true;
            StateHasChanged();
        }
        public void Dispose() => DeckState.OnChange -= UpdateState;
        
    }
}
