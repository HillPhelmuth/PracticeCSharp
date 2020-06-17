using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PracticeCSharpPWA.Client.JsInteropExtensions;
using PracticeCSharpPWA.Shared.Models.VideosModels;

namespace PracticeCSharpPWA.Client.Pages.EmbedVideos
{
    public partial class VideoPlayer : ComponentBase, IDisposable
    {
        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        [Parameter]
        public EventCallback<bool> VideoEnded { get; set; }
        [Parameter]
        public string VideoId { get; set; }
        
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            var refThis = DotNetObjectReference.Create(this);
            await JSRuntime.StartYouTube();
            await Task.Delay(1000);
            await JSRuntime.InvokeAsync<object>("getYouTube", refThis, VideoId);
        }
        [JSInvokable]
        // ReSharper disable once UnusedMember.Global -JSInvokable used by javascript code
        public async Task GetNextVideo()
        {
            await Task.Delay(1000);
            await JSRuntime.StopYouTubePlayer();
            await VideoEnded.InvokeAsync(false);
        }
        public void Dispose() => JSRuntime.StopYouTubePlayer();
    }
}
