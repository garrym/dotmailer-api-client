using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace dotMailer.Api
{
    public partial class Client
    {
        private readonly HttpClient httpClient;

        public Client(string username, string password)
        {
            httpClient = GetHttpClient(username, password);
        }

        public const string BaseAddress = "https://apiconnector.com/";

        #region Helpers

        private static HttpClient GetHttpClient(string username, string password)
        {
            var client = new HttpClient { BaseAddress = new Uri(BaseAddress) };

            var base64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(username + ":" + password));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64);

            return client;
        }

        private static bool IsValidResponse(HttpResponseMessage responseMessage, params HttpStatusCode[] acceptableStatusCodes)
        {
            return acceptableStatusCodes.Any(x => x == responseMessage.StatusCode);
        }

        #endregion

        #region HttpMethods

		private ServiceResult Get(Request request)
        {
            var response = httpClient.GetAsync(request.Url).Result;
            if (IsValidResponse(response, HttpStatusCode.OK))
            {
                var result = response.Content.ReadAsStringAsync().Result;
                return new ServiceResult(true, result);
            }
            return new ServiceResult(false, StatusCodeSpecificError("Failed to get object", response.StatusCode));
        }

        private ServiceResult<T> Get<T>(Request request)
        {
            var response = httpClient.GetAsync(request.Url).Result;
            if (IsValidResponse(response, HttpStatusCode.OK))
            {
                var result = response.Content.ReadAsAsync<T>().Result;
                return new ServiceResult<T>(true, result);
            }
            return new ServiceResult<T>(false, default(T), StatusCodeSpecificError("Failed to get object", response.StatusCode));
        }

        private ServiceResult<T> Post<T>(Request request)
        {
            var response = httpClient.PostAsJsonAsync(request.Url, string.Empty).Result;
            if (IsValidResponse(response, HttpStatusCode.Created))
            {
                var result = response.Content.ReadAsAsync<T>().Result;
                return new ServiceResult<T>(true, result);
            }
            return new ServiceResult<T>(false, default(T), StatusCodeSpecificError("Failed to post object", response.StatusCode));
        }

        private ServiceResult<T> Post<T>(Request request, T data)
        {
            var response = httpClient.PostAsJsonAsync(request.Url, data).Result;
            if (IsValidResponse(response, HttpStatusCode.Created))
            {
                var result = response.Content.ReadAsAsync<T>().Result;
                return new ServiceResult<T>(true, result);
            }
            return new ServiceResult<T>(false, data, StatusCodeSpecificError("Failed to post object", response.StatusCode));
        }

        private ServiceResult<TOutput> Post<TOutput, TInput>(Request request, TInput data)
        {
            var response = httpClient.PostAsJsonAsync(request.Url, data).Result;
            if (IsValidResponse(response, HttpStatusCode.Created))
            {
                var result = response.Content.ReadAsAsync<TOutput>().Result;
                return new ServiceResult<TOutput>(true, result);
            }
            return new ServiceResult<TOutput>(false, default(TOutput), StatusCodeSpecificError("Failed to post object", response.StatusCode));
        }

        private ServiceResult<T> Put<T>(Request request, T data)
        {
            var response = httpClient.PutAsJsonAsync(request.Url, data).Result;
            if (IsValidResponse(response, HttpStatusCode.OK))
            {
                var result = response.Content.ReadAsAsync<T>().Result;
                return new ServiceResult<T>(true, result);
            }
            return new ServiceResult<T>(false, data, StatusCodeSpecificError("Failed to put object", response.StatusCode));
        }

        private ServiceResult Delete(Request request)
        {
            var response = httpClient.DeleteAsync(request.Url).Result;
            if (IsValidResponse(response, HttpStatusCode.NoContent))
            {
                return new ServiceResult(true);
            }
            return new ServiceResult(false, StatusCodeSpecificError("Failed to delete object", response.StatusCode));
        }

        private ServiceResult<T> Delete<T>(Request request)
        {
            var response = httpClient.DeleteAsync(request.Url).Result;
            if (IsValidResponse(response, HttpStatusCode.OK))
            {
                var result = response.Content.ReadAsAsync<T>().Result;
                return new ServiceResult<T>(true, result);
            }
            return new ServiceResult<T>(false, default(T), StatusCodeSpecificError("Failed to delete object", response.StatusCode));
        }

        private static string StatusCodeSpecificError(string message, HttpStatusCode statusCode)
        {
            return string.Format("Error: {0} (Status Code: {1}, Status Description: {2})", message, (int)statusCode, statusCode);
        }

        #endregion
    }
}
