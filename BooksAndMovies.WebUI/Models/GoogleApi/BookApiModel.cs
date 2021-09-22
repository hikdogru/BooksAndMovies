using BooksAndMovies.WebUI.Models.Client;
using Newtonsoft.Json.Serialization;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksAndMovies.WebUI.Models.GoogleApi
{
    public class BookApiModel
    {
        public async Task<List<BookInfoModel>> GetBookFromGoogle(string url)
        {
            string clientUrl = url;
            var restApiModel = new RestApiModel(url: clientUrl, method: Method.GET);
            IRestResponse response = await restApiModel.GetRestResponse();
            var jsonConvertModel = new JsonConvertModel(new CamelCaseNamingStrategy());
            var bookJsonModel = jsonConvertModel.GetContent<BookJsonModel>(response.Content);
            return bookJsonModel.Items;
        }
    }
}
