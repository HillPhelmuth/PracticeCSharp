using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracticeCSharpPWA.Shared.Models.FlashCardModels
{
    public class DeckStateService
    {
        public DeckStats DeckStats { get; private set; }
        public Deck SelectedDeck { get; private set; }
        public List<Card> Cards { get; private set; }
        public List<Deck> UserDecks { get; private set; }
        public string UserName { get; set; }

        public event Func<Task> OnChange;

        public async Task UpdateSelectedDeck(Deck deck, bool isNew = false)
        {
            DeckStats ??= new DeckStats { Deck = deck };
            SelectedDeck = deck;
            await NotifyStateChanged();
        }

        public async Task UpdateUserDecks(List<Deck> decks)
        {
            UserDecks = decks.Distinct().ToList();
            await NotifyStateChanged();
        }
        public async Task UpdateStats(bool isCorrect)
        {
            if (isCorrect)
                DeckStats.Correct++;
            else
                DeckStats.InCorrect++;
            var correct = DeckStats.Correct;
            var total = DeckStats.Correct + DeckStats.InCorrect;
            DeckStats.TotalPct = correct / total;
            await NotifyStateChanged();
        }
        public async Task ResetDeckStats(Deck deck)
        {
            DeckStats = new DeckStats
            {
                Deck = deck,
                Correct = 0,
                InCorrect = 0,
                TotalPct = 0
            };
            await NotifyStateChanged();
        }
        public async Task<DeckStats> GetDeckStats(Deck deck)
        {
            return null;
            //return await Database.GetDeckStats(deck);
        }
        public async Task UpdateDeckCards(Deck deck, List<Card> cards)
        {
            SelectedDeck = deck;
            Cards = cards;
            SelectedDeck.Cards = cards;
            await NotifyStateChanged();
        }

        public async Task<List<Deck>> GetUserDecks()
        {
            var userDecks = UserDecks;
            await UpdateUserDecks(userDecks);
            return userDecks.Distinct().ToList();
        }

        private async Task NotifyStateChanged()
        {
            if (OnChange != null) await OnChange.Invoke();
        }
    }
}
