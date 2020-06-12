using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PracticeCSharpPWA.Shared.Models.FlashCardModels;

namespace PracticeCSharpPWA.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizDeckController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Deck> GetDecks()
        {
            var deckList = JsonConvert.DeserializeObject<DeckList>(System.IO.File.ReadAllText("CSharpDecks.json"));
            return deckList.Decks;
            //return _flashCardsDb.GetUserDecks().Result;
        }
        [HttpGet("{name}")]
        public Deck GetDeck(string name)
        {
            var deckList = JsonConvert.DeserializeObject<DeckList>(System.IO.File.ReadAllText("CSharpDecks.json"));

            return deckList.Decks.Find(x => x.Name == name);
            //return _flashCardsDb.GetUserDecks().Result;
        }
    }
}
