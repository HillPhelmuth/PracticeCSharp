using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;
using MatBlazor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Newtonsoft.Json;
using PracticeCSharpPWA.Shared;
using PracticeCSharpPWA.Shared.Models.VideosModels;

namespace PracticeCSharpPWA.Client.Pages.EmbedVideos
{
    public partial class VideoHome
    {
        [Inject]
        public AppStateService AppStateService { get; set; }
        public Videos Videos { get; set; }
        protected string selectedVideoId { get; set; }
        protected bool IsVideoReady;
        protected bool IsPageVideosReady;
        protected override Task OnInitializedAsync()
        {
            Videos = AppStateService.Videos;
            IsPageVideosReady = true;
            return Task.CompletedTask;
        }
        protected void HandleVideoEnd(bool isEnd)
        {
            IsVideoReady = false;
        }
        protected async Task PlayVideos()
        {
            if (IsVideoReady)
            {
                IsVideoReady = false;
                StateHasChanged();
                await Task.Delay(500);
            }
            IsVideoReady = true;
            StateHasChanged();
        }
    }
}