using System.Collections.Generic;
using System.Threading.Tasks;
using MatBlazor;
using Microsoft.AspNetCore.Components.Web;
using PracticeCSharpPWA.Shared.Models.VideosModels;

namespace PracticeCSharpPWA.Client.Pages.EmbedVideos
{
    public partial class VideoHome
    {
        public List<VideoModel> CollectionsVideos1 { get; set; }
        public List<VideoModel> CollectionsVideos2 { get; set; }
        public List<VideoModel> CollectionsVideos3 { get; set; }
        public List<VideoModel> StringsVideos1 { get; set; }
        public List<VideoModel> StringsVideos2 { get; set; }
        public List<VideoModel> StringsVideos3 { get; set; }
        public List<VideoModel> ConditionalsLoops1 { get; set; }
        public List<VideoModel> ConditionalsLoops2 { get; set; }
        public List<VideoModel> ConditionalsLoops3 { get; set; }
        protected List<VideoModel> selectedList { get; set; }
        protected bool IsVideoReady;
        protected bool isVideoSelection;
        protected ForwardRef CollectionsForwardRef = new ForwardRef();
        protected ForwardRef StringsForwardRef = new ForwardRef();
        protected ForwardRef ConditionalsLoopsForwardRef = new ForwardRef();
        protected BaseMatMenu Collections;
        protected BaseMatMenu Strings;
        protected BaseMatMenu ConditionalsLoops;

        protected override Task OnInitializedAsync()
        {
            GenerateCollectionsVideos();
            GenerateStringsVideos();
            GenerateConditionalLoops();
            return Task.CompletedTask;
        }
        public void CollectClick(MouseEventArgs e)
        {
            this.Collections.OpenAsync();
        }
        public void StringsClick(MouseEventArgs e)
        {
            this.Strings.OpenAsync();
        }

        public void ConditionalsClick(MouseEventArgs e)
        {
            this.ConditionalsLoops.OpenAsync();
        }
        private void GenerateStringsVideos()
        {
            StringsVideos1 = new List<VideoModel>();
            StringsVideos2 = new List<VideoModel>();
            StringsVideos3 = new List<VideoModel>();

            StringsVideos1.Add(new VideoModel
            {
                Title = "Strings: C# Tutorial For Beginners",
                VideoID = "=-G-yY_EQcL0",
                PreferenceID = 0
            });
            StringsVideos1.Add(new VideoModel
            {
                Title = "Working With Strings | C# | Tutorial 6",
                VideoID = "kEw3uV4dpXE",
                PreferenceID = 0
            });
            StringsVideos2.Add(new VideoModel
            {
                Title = "C# Programming Tutorial #3 - Strings in Depth",
                VideoID = "aWTw4Qvmkus",
                PreferenceID = 0
            });
            StringsVideos2.Add(new VideoModel
            {
                Title = "C# Controlling Programmatic Flow, and Manipulating Types",
                VideoID = "-ei37fhhY-mU",
                PreferenceID = 0
            });
            StringsVideos3.Add(new VideoModel
            {
                Title = "C# Tutorial 2 Looping Arrays StringBuilder",
                VideoID = "bBG2o905sRQ",
                PreferenceID = 0
            });
            StringsVideos3.Add(new VideoModel
            {
                Title = "How to Parse Strings With the Split Function in C Sharp",
                VideoID = "m0UKmK89jpo",
                PreferenceID = 0
            });
        }

        private void GenerateCollectionsVideos()
        {
            CollectionsVideos1 = new List<VideoModel>();
            CollectionsVideos2 = new List<VideoModel>();
            CollectionsVideos3 = new List<VideoModel>();

            CollectionsVideos1.Add(new VideoModel
            {
                Title = "C# Tutorial 11 Collections",
                VideoID = "qLeF_wpnVto",
                PreferenceID = 0
            });
            CollectionsVideos1.Add(new VideoModel
            {
                Title = "C# Tutorial 11 Collections",
                VideoID = "l6s7AvZx5j8",
                PreferenceID = 0
            });
            CollectionsVideos2.Add(new VideoModel
            {
                Title = "C# Tutorial 11 Collections",
                VideoID = "gXyoJA579QI",
                PreferenceID = 0
            });
            CollectionsVideos2.Add(new VideoModel
            {
                Title = "C# Tutorial 11 Collections",
                VideoID = "-sqKcvVMz5AM",
                PreferenceID = 0
            });
            CollectionsVideos3.Add(new VideoModel
            {
                Title = "C# Tutorial 11 Collections",
                VideoID = "IfGYbkbcZ-Q",
                PreferenceID = 0
            });
            CollectionsVideos3.Add(new VideoModel
            {
                Title = "C# Tutorial 11 Collections",
                VideoID = "mf7Bn8utej8",
                PreferenceID = 0
            });
        }
        private void GenerateConditionalLoops()
        {
            ConditionalsLoops1 = new List<VideoModel>();
            ConditionalsLoops2 = new List<VideoModel>();
            ConditionalsLoops3 = new List<VideoModel>();
            ConditionalsLoops1.Add(new VideoModel
            {
                Title = "",
                VideoID = "IzzNzSXkCMM",
                PreferenceID = 0
            });
            ConditionalsLoops2.Add(new VideoModel
            {
                Title = "",
                VideoID = "d7lbHiTiIF8",
                PreferenceID = 0
            });
            ConditionalsLoops3.Add(new VideoModel
            {
                Title = "",
                VideoID = "q4csanUFQMo",
                PreferenceID = 0
            });
        }

        protected Task PlayVideos()
        {
            IsVideoReady = true;
            return Task.CompletedTask;
        }
    }
}