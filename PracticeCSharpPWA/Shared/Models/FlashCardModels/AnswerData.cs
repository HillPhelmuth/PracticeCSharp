namespace PracticeCSharpPWA.Shared.Models.FlashCardModels
{
    public class AnswerData
    {
        public string Answer { get; set; }
        public bool IsIncorrect { get; set; }
        public bool IsCorrect { get; set; }
        public string CssClass { get; set; }
    }
}
