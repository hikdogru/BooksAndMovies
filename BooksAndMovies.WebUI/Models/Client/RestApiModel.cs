using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksAndMovies.WebUI.Models.Client
{
    public class RestApiModel
    {
        private readonly string _url;
        private readonly Method _method;

        public RestClient Client { get; set; }
        public RestRequest Request { get; set; }

        public RestApiModel(string url, Method method)
        {
            _url = url;
            _method = method;
            GetRestClient();
        }

        private void GetRestClient()
        {
            Client = new RestClient(_url);
            Client.Timeout = -1;
            GetRestRequest();
        }

        private void GetRestRequest()
        {
            Request = new RestRequest(_method);
        }

        public async Task<IRestResponse> GetRestResponse()
        {
            return await Client.ExecuteAsync(Request);
        }



    }
}
