using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace PracticeCSharpPWA.Shared.Models.FlashCardModels
{
    [Serializable]
    [Table("Cards")]
    public class Card : CardDeckBase
    {
        [JsonIgnore]
        public int ID { get; set; }
        [JsonProperty("deckId")]
        public int Decks_ID { get; set; }
        [JsonProperty("question")]
        public string Question { get; set; }
        [JsonProperty("answer")]
        public string Answer { get; set; }
        [JsonIgnore]
        [NotMapped]
        public List<AnswerData> DisplayAnswers { get; set; }
    }
}
