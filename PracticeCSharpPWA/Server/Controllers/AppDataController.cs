using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PracticeCSharpPWA.Shared.Models.CodeEditorModels;

namespace PracticeCSharpPWA.Server.Controllers
{
    [Route("api/appData")]
    [ApiController]
    public class AppDataController : ControllerBase
    {
        [HttpGet("code")]
        public async Task<string> GetChallenges()
        {
            var filename = "ChallengeData1.json";
            string fileData = await ReadJsonFile(filename);
            return await Task.FromResult(fileData);
        }

        [HttpGet("videos")]
        public async Task<string> GetVideos()
        {
            var fileName = "VideoList1.json";
            string fileData = await ReadJsonFile(fileName);
            return await Task.FromResult(fileData);
        }
        private async Task<string> ReadJsonFile(string filename)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = assembly.GetManifestResourceNames().Single(s => s.EndsWith(filename));
            // Should be: PracticeCSharpPWA.Server.ChallengeData1.json
            Console.WriteLine($"file found: {resourceName}");
            await using var stream = assembly.GetManifestResourceStream(resourceName);
            using var sr = new StreamReader(stream ?? new MemoryStream());
            return await sr.ReadToEndAsync();
        }
    }
}
