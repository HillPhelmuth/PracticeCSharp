﻿using System.Collections.Generic;
using Newtonsoft.Json;

namespace PracticeCSharpPWA.Shared.Models.VideosModels
{
    public class VideoModel
    {
        public string Title { get; set; }
        public string VideoID { get; set; }
        public int PreferenceID { get; set; }
    }
    public class Videos
    {
        [JsonProperty("videosList")]
        public List<VideosList> VideosList { get; set; }
    }

    public class VideosList
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("videos")]
        public List<Video> Videos { get; set; }
    }

    public class Video
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("videoId")]
        public string VideoId { get; set; }

        [JsonProperty("preferenceId")]
        public long PreferenceId { get; set; }
    }
}
