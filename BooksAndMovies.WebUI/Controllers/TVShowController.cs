using BooksAndMovies.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksAndMovies.WebUI.Controllers
{
    public class TVShowController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SearchTVShow(string query)
        {
            if (!string.IsNullOrEmpty(query))
            {
                var client = new RestClient("https://api.themoviedb.org/3/search/tv?api_key=ebd943da4f3d062ae4451758267b1ca9&language=en-US&page=1" + "&query=" + query);
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                IRestResponse response = await client.ExecuteAsync(request);
                var settings = new JsonSerializerSettings
                {
                    ContractResolver = new DefaultContractResolver { NamingStrategy = new SnakeCaseNamingStrategy() }
                };
                var content = JsonConvert.DeserializeObject<TVShowJsonModel>(response.Content, settings);
                var tvShows = content.Results;
                return View("Search" , tvShows);
            }

            return null;

        }

    }
}
