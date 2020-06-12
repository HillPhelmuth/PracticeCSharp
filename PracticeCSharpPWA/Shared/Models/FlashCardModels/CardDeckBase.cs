using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace PracticeCSharpPWA.Shared.Models.FlashCardModels
{
    public class CardDeckBase
    {
        [JsonIgnore]
        [NotMapped]
        public string ConfirmDelete { get; set; }
        [JsonIgnore]
        [NotMapped]
        public bool IsDeleteConfirm { get; set; }
        [JsonIgnore]
        [NotMapped]
        public string CssConfirmClass { get; set; }
    }
}
