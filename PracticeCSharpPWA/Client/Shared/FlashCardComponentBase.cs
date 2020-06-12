using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using PracticeCSharpPWA.Shared.Models.FlashCardModels;

namespace PracticeCSharpPWA.Client.Shared
{
    public abstract class FlashCardComponentBase : ComponentBase
    {
        [Inject]
        protected DeckStateService DeckState { get; set; }
        [Inject]
        protected HttpClient Http { get; set; }
        public List<Deck> UserDecks { get; set; }
        public Deck SelectedDeck { get; set; }
        public List<Card> DeckCards { get; set; }
        protected DeckStats DeckStats { get; set; }
        protected Task UpdateState()
        {
            UserDecks = DeckState.UserDecks;
            SelectedDeck = DeckState.SelectedDeck;
            if (SelectedDeck != null)
            {
                DeckStats = DeckState.DeckStats;
                DeckCards = SelectedDeck?.Cards ?? DeckState.Cards;
            }
            StateHasChanged();
            return Task.CompletedTask;
        }
    }
}
