using BooksAndMovies.WebUI.Models.Client;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BooksAndMovies.WebUI.Models.TMDB
{
    public class TMDBModel
    {
        public string APIKey { get; set; }
        public string WebsiteRootUrl { get; set; }
        public MovieJsonModel MovieJsonModel { get; set; }
        public TVShowJsonModel TVShowJsonModel { get; set; }

        public TMDBModel()
        {
            APIKey = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ApiKeys")["TMDBApiKey"];
            WebsiteRootUrl = "https://api.themoviedb.org/3/";
        }
        public async Task<List<TVShowModel>> GetTVShowsFromTMDB(string url)
        {
            await GetContentFromTMDB(url: url, modelNo: 1);
            return TVShowJsonModel.Results;
        }

        public async Task<List<MovieModel>> GetMoviesFromTMDB(string url)
        {
            await GetContentFromTMDB(url: url, modelNo: 2);
            return MovieJsonModel.Results;
        }

        private async Task GetContentFromTMDB(string url, int modelNo)
        {
            string clientUrl = url;
            var restApiModel = new RestApiModel(url: clientUrl, method: Method.GET);
            IRestResponse response = await restApiModel.GetRestResponse();
            var jsonConvertModel = new JsonConvertModel(new SnakeCaseNamingStrategy());
            if (modelNo == 1)
                TVShowJsonModel = jsonConvertModel.GetContent<TVShowJsonModel>(response.Content);
            else
                MovieJsonModel = jsonConvertModel.GetContent<MovieJsonModel>(response.Content);

        }

        public void PostContentToTMDB(string url, byte[] data)
        {
            var json = Encoding.UTF8.GetString(data);
            var client = new RestApiModel(url: url, method: Method.POST);
            var request = client.Request;
            request.AddParameter("application/json", json, ParameterType.RequestBody);
            var response = client.GetRestResponse(request);

        }

        public void DeleteContentFromTMDB(string url)
        {
            var client = new RestApiModel(url: url, Method.DELETE);
            var request = client.Request;
            var response = client.GetRestResponse(request);
        }
    }
}
