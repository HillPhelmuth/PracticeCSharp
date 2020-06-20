using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PracticeCSharpPWA.Shared;
using PracticeCSharpPWA.Shared.Models.CodeEditorModels;
using PracticeCSharpPWA.Shared.Models.FlashCardModels;

namespace PracticeCSharpPWA.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddSingleton<DeckStateService>();
            builder.Services.AddSingleton<CodeEditorService>();
            builder.Services.AddSingleton<CompilerService>();
            builder.Services.AddSingleton<AppStateService>();
            await builder.Build().RunAsync();
        }
    }
}
