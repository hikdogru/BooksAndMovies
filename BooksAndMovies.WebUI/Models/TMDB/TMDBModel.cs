﻿using BooksAndMovies.WebUI.Models.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksAndMovies.WebUI.Models.TMDB
{
    public class TMDBModel
    {
        public MovieJsonModel MovieJsonModel { get; set; }
        public TVShowJsonModel TVShowJsonModel { get; set; }
        public async Task<List<TVShowModel>> GetTVShowsFromTMDB<T>(string url) where T : TVShowJsonModel
        {
            await GetContentFromTMDB(url: url, modelNo: 1);
            return TVShowJsonModel.Results;
        }

        public async Task<List<MovieModel>> GetMoviesFromTMDB<T>(string url) where T : MovieJsonModel
        {
            await GetContentFromTMDB(url: url, modelNo: 2);
            return MovieJsonModel.Results;
        }

        public async Task GetContentFromTMDB(string url, int modelNo)
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
    }
}