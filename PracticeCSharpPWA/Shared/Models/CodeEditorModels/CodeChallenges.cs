using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace PracticeCSharpPWA.Shared.Models.CodeEditorModels
{
    public class CodeChallenges
    {
        [JsonProperty("challenges")]
        public List<Challenge> Challenges { get; set; }
    }
    public partial class Challenge
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("difficulty")]
        public string Difficulty { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("examples")]
        public string Examples { get; set; }

        [JsonProperty("snippet")]
        public string Snippet { get; set; }
        [JsonProperty("solution")]
        public string Solution { get; set; }
    }
}
