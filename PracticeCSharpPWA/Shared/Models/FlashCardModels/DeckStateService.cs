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
        private async Task NotifyStateChanged()
        {
            if (OnChange != null) await OnChange.Invoke();
        }
    }
}
