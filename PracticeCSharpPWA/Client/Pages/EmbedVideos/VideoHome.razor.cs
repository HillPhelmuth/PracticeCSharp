﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using MatBlazor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Newtonsoft.Json;
using PracticeCSharpPWA.Shared.Models.VideosModels;

namespace PracticeCSharpPWA.Client.Pages.EmbedVideos
{
    public partial class VideoHome
    {
        [Inject]
        protected NavigationManager NavigationManager { get; set; }
        public Videos Videos { get; set; }
        protected string selectedVideoId { get; set; }
        protected List<VideoModel> selectedList { get; set; }
        protected bool IsVideoReady;
        protected bool IsPageVideosReady;
       
        protected override async Task OnInitializedAsync()
        {
            var client = new HttpClient { BaseAddress = new Uri(NavigationManager.BaseUri) };
            var videosString = await client.GetStringAsync("VideoList.json");
            Console.WriteLine($"videos string: {videosString}");
            Videos = JsonConvert.DeserializeObject<Videos>(videosString);
            IsPageVideosReady = true;
        }
        protected void HandleVideoEnd(bool isEnd)
        {
            IsVideoReady = false;
            IsPageVideosReady = true;
        }
        protected Task PlayVideos()
        {
            IsVideoReady = true;
            IsPageVideosReady = false;
            return Task.CompletedTask;
        }
    }
}