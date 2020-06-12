using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace PracticeCSharpPWA.Shared.Models.FlashCardModels
{
    [Serializable]
    [Table("Decks")]
    public class Deck : CardDeckBase
    {
        [JsonProperty("id")]
        public int ID { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("subject")]
        public string Subject { get; set; }
        [JsonProperty("userId")]
        public string User_ID { get; set; }
        [JsonProperty("cards")]
        [NotMapped]
        public List<Card> Cards { get; set; }
    }
    public class DeckList
    {
        [JsonProperty("decks")]
        public List<Deck> Decks { get; set; }
    }
}
