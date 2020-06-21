﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.CodeAnalysis;
using Newtonsoft.Json;
using PracticeCSharpPWA.Shared;
using PracticeCSharpPWA.Shared.Models.CodeEditorModels;
using PracticeCSharpPWA.Shared.Models.VideosModels;

namespace PracticeCSharpPWA.Client.Pages
{
    public partial class Index
    {
        [Inject]
        public AppStateService AppStateService { get; set; }
        [Inject]
        protected HttpClient Http { get; set; }
        private int tabIndex = 0;
        private bool isPageReady;
        protected override async Task OnInitializedAsync()
        {
            var challengeString = await Http.GetStringAsync("api/appData/code");
            var codeChallenges = JsonConvert.DeserializeObject<CodeChallenges>(challengeString);
            var videosString = await Http.GetStringAsync("api/appData/videos");
            var videos = JsonConvert.DeserializeObject<Videos>(videosString);
            var refs = AppDomain.CurrentDomain.GetAssemblies();
            var assemblyRefs = new List<MetadataReference>();

            foreach (var reference in refs.Where(x => !x.IsDynamic && !string.IsNullOrWhiteSpace(x.Location) && (x.FullName.Contains("System") || x.FullName.Contains("mscorlib") || x.FullName.Contains("netstandard"))))
            {
                var stream = await Http.GetStreamAsync($"_framework/_bin/{reference.Location}");
                assemblyRefs.Add(MetadataReference.CreateFromStream(stream));
            }
            var references = assemblyRefs;

            AppStateService.SetInitialAppState(codeChallenges, videos, references);
            isPageReady = true;
        }

        protected Task HandleNotReady(int tab)
        {
            tabIndex = 0;
            StateHasChanged();
            return Task.CompletedTask;
        }
    }
}
