﻿using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PracticeCSharpPWA.Shared.Models.FlashCardModels
{
    [Serializable]
    [Table("DeckStats")]
    public class DeckStats : StatsBase
    {
        public int ID { get; set; }
        public int Decks_ID { get; set; }

        [NotMapped]
        public Deck Deck { get; set; }
    }
}
