using System;
using System.Net;
using RestSharp;

namespace BusinessSafe.Application.RestAPI
{
    public interface IRestClientAPI
    {
        T Execute<T>(RestRequest request) where T : new();
    }

    public class RestClientAPI : IRestClientAPI
    {
        private readonly RestClient _restClient;

        public RestClientAPI(RestClient restClient)
        {
            _restClient = restClient;
            
        }

        public T Execute<T>(RestRequest request) where T : new()
        {
            var response = _restClient.Execute<T>(request);
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return response.Data;
                case HttpStatusCode.NotFound:
                    throw new Exception("Resource not found!");
                default:
                    throw new Exception(string.Concat(  "Unspecified exception. Status code: ",response.StatusCode.ToString()));
            }
        }

    }
}