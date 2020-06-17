using System;
using System.Collections.Generic;
using System.Linq;
using PracticeCSharpPWA.Shared.Models.FlashCardModels;

namespace PracticeCSharpPWA.Shared.ExtensionMethods
{
    public static class FlashCardExtensions
    {
        private static readonly Random Rng = new Random();
        public static List<Card> AddAltAnswers(this List<Card> cards)
        {
            var answers = cards.Select(x => x.Answer).ToArray();
            foreach (var card in cards)
            {
                int altAnswer;
                (card.DisplayAnswers ??= new List<AnswerData>()).Clear();

                for (int i = 0; i < 3; i++)
                {
                    var altDisplayLoop = new AnswerData();
                    altAnswer = Rng.Next(0, answers.Length);
                    altDisplayLoop.Answer = answers[altAnswer];
                    if (!card.DisplayAnswers.Contains(altDisplayLoop) && card.Answer != altDisplayLoop.Answer)
                    {
                        card.DisplayAnswers.Add(altDisplayLoop);
                    }
                    
                }
                var altDisplay = new AnswerData() { Answer = card.Answer };
                altDisplay.Answer = card.Answer;
                card.DisplayAnswers.Add(altDisplay);
            }
            return cards;
        }

        public static void Shuffle<T>(this IList<T> cards)
        {
            int n = cards.Count;
            while (n > 1)
            {
                n--;
                int k = Rng.Next(n + 1);
                T value = cards[k];
                cards[k] = cards[n];
                cards[n] = value;
            }
        }
        public static bool IsCorrectAnswer(this string selectedAnswer, string cardAnswer)
        {
            return selectedAnswer == cardAnswer;
        }
    }
}
