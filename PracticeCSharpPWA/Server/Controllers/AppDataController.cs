using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            string fileName = "ChallengeData1.json";
            string fileData;
            using (var sr = new StreamReader(fileName))
            {
                fileData = await sr.ReadToEndAsync();
            }

            return await Task.FromResult(fileData);
        }

        [HttpGet("videos")]
        public async Task<string> GetVideos()
        {
            string fileName = "VideoList1.json";
            string fileData;
            using (var sr = new StreamReader(fileName))
            {
                fileData = await sr.ReadToEndAsync();
            }

            return await Task.FromResult(fileData);
        }
    }
}
