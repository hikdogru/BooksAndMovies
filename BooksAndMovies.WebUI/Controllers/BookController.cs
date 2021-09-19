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
    public class BookController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SearchBook(string query)
        {
            if (!string.IsNullOrEmpty(query))
            {
                var client = new RestClient("https://www.googleapis.com/books/v1/volumes?key=AIzaSyAWeKsrZKQlLMC2AaDxM1zRbLoBHoEMj8w&maxResults=5" + "&q=" + query);
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                IRestResponse response = await client.ExecuteAsync(request);
                var settings = new JsonSerializerSettings
                {
                    ContractResolver = new DefaultContractResolver { NamingStrategy = new CamelCaseNamingStrategy() }
                };
                var content = JsonConvert.DeserializeObject<BookJsonModel>(response.Content, settings);
                var books = content.Items ;
                return View("Search", books);
            }

            return null;

        }
    }
}
