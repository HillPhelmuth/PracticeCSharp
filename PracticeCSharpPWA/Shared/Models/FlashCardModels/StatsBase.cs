using System.ComponentModel.DataAnnotations.Schema;

namespace PracticeCSharpPWA.Shared.Models.FlashCardModels
{
    public class StatsBase
    {
        [Column(TypeName = "decimal(6,3)")]
        public decimal Correct { get; set; }
        [Column(TypeName = "decimal(6,3)")]
        public decimal InCorrect { get; set; }
        [Column(TypeName = "decimal(6,3)")]
        public decimal TotalPct { get; set; }
    }
}
