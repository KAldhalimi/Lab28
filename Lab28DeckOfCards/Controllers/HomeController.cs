using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Lab28DeckOfCards.Models;
using System.Net.Http;

namespace Lab28DeckOfCards.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private HttpClient _httpClient;
        public HomeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://deckofcardsapi.com");
        }
        //private readonly IHttpClientFactory _httpClient;

        //public HomeController(IHttpClientFactory httpClient)
        //{
        //    _httpClient = httpClient;
        //    var client = _httpClient.CreateClient();
        //    client.BaseAddress = new Uri("https://deckofcardsapi.com");
        //}
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("/api/deck/new/shuffle/?deck_count=1");
            var content = await response.Content.ReadAsAsync<Deck>();
            return View(content);
        }
        public async Task<IActionResult> DrawCards(Deck deck)
        {
            var response = await _httpClient.GetAsync($"/api/deck/{deck.deck_id}/draw/?count={deck.numDraw}");
            var content = await response.Content.ReadAsAsync<Deck>();
            return View(content);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
