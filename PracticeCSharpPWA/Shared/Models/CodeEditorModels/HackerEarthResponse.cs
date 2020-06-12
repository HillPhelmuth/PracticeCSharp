using System;
using Newtonsoft.Json;

namespace PracticeCSharpPWA.Shared.Models.CodeEditorModels
{
    [Serializable]
    public class HackerEarthResponse
    {
        [JsonProperty("errors")]
        public Errors Errors { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("code_id")]
        public string CodeId { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("compile_status")]
        public string CompileStatus { get; set; }

        [JsonProperty("run_status")]
        public RunStatus RunStatus { get; set; }
    }

    public partial class Errors
    {
        public string error { get; set; }
    }

    public class RunStatus
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("time_used")]
        public string TimeUsed { get; set; }

        [JsonProperty("memory_used")]
        //[JsonConverter(typeof(ParseStringConverter))]
        public long MemoryUsed { get; set; }

        [JsonProperty("output")]
        public string Output { get; set; }

        [JsonProperty("output_html")]
        public string OutputHtml { get; set; }

        [JsonProperty("signal")]
        public string Signal { get; set; }

        [JsonProperty("status_detail")]
        public string StatusDetail { get; set; }

        [JsonProperty("time_limit")]
        public long TimeLimit { get; set; }

        [JsonProperty("memory_limit")]
        public long MemoryLimit { get; set; }
    }
}
