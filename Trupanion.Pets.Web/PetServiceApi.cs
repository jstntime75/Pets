using System;
using System.Configuration;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;

namespace Trupanion.Pets.Web
{
    internal static class PetServiceApi
    {
        public static Task<T> GetAsync<T>(string apiEndpoint)
        {
            return ExecuteAsync<T>(new RestRequest(apiEndpoint, Method.GET));
        }

        public static Task<T> CreateAsync<T>(string apiEndpoint, T entity)
        {
            var request = new RestRequest(apiEndpoint, Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(entity);

            return ExecuteAsync<T>(request);
        }

        public static Task<T> UpdateAsync<T>(string apiEndpoint, T entity)
        {
            var request = new RestRequest(apiEndpoint, Method.PUT);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(entity);

            return ExecuteAsync<T>(request);
        }

        public static Task<T> DeleteAsync<T>(string apiEndpoint)
        {
            return ExecuteAsync<T>(new RestRequest(apiEndpoint, Method.DELETE));
        }

        private async static Task<T> ExecuteAsync<T>(RestRequest request)
        {
            var client = new RestClient(ConfigurationManager.AppSettings["trupanion:PetServiceUrl"]);

            IRestResponse response = await client.ExecuteTaskAsync(request, CancellationToken.None).ConfigureAwait(false);

            if (response.ErrorException != null)
            {
                throw response.ErrorException;
            }

            switch (response.StatusCode)
            {
                case HttpStatusCode.InternalServerError:
                    var message = JsonConvert.DeserializeObject<WebExceptionInternal>(response.Content);
                    throw new Exception(message.ExceptionMessage);
                case HttpStatusCode.NotFound:
                case HttpStatusCode.BadRequest:
                    return default(T);
                default:
                    break;
            }

            return JsonConvert.DeserializeObject<T>(response.Content);
        }

        private class WebExceptionInternal
        {
            public string Message { get; set; }
            public string ExceptionMessage { get; set; }
            public string ExceptionType { get; set; }
            public string StackTrace { get; set; }
        }
    }
}