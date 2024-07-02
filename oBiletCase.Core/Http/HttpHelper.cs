using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System.Net.Http;
using System.Text;

namespace oBiletCase.Core.Http
{
    public class HttpHelper : IHttpHelper
    {
        private readonly IHttpClientFactory _httpClient;

        public HttpHelper(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<TResponse> ConnectPost<TRequest, TResponse>(TRequest request, string clientName, string url, Dictionary<string, string> headers = null, bool camelCaseJson = true)
            where TRequest : IRequestModel, new()
            where TResponse : IResponseModel, new()
        {
            var client = _httpClient.CreateClient(clientName);

            AddHeadersToClient(headers, client);

            string jsonModel = "";
            if (camelCaseJson) jsonModel = request.ConvertToCamelCaseJson();
            else jsonModel = JsonConvert.SerializeObject(request);

            var content = new StringContent(jsonModel, Encoding.UTF8, "application/json");

            using HttpResponseMessage httpResponse = await client.PostAsync(url, content);

            return await GetApiResponse<TResponse>(httpResponse);
        }

        public async Task<DefaultApiResponse> ConnectPost<TRequest>(TRequest request, string clientName, string url, Dictionary<string, string> headers = null, bool camelCaseJson = true)
            where TRequest : IRequestModel, new()
        {
            var client = _httpClient.CreateClient(clientName);

            AddHeadersToClient(headers, client);

            string jsonModel = "";
            if (camelCaseJson) jsonModel = request.ConvertToCamelCaseJson();
            else jsonModel = JsonConvert.SerializeObject(request);
            if (jsonModel.Contains("QuestionOptions"))
            {
                JObject jsonObject = JObject.Parse(jsonModel);
                JArray questionOptionsArray = JArray.Parse((string)jsonObject["QuestionOptions"]);
                jsonObject["QuestionOptions"] = questionOptionsArray;
                jsonModel = jsonObject.ToString(Formatting.None);
            }          

            var content = new StringContent(jsonModel, Encoding.UTF8, "application/json");
            using HttpResponseMessage httpResponse = await client.PostAsync(url, content);

            return await GetApiResponse(httpResponse);
        }

        public async Task<TResponse> ConnectPostString<TResponse>(string request, string clientName, string url, Dictionary<string, string> headers = null)
            where TResponse : IResponseModel, new()
        {
            var client = _httpClient.CreateClient(clientName);

            AddHeadersToClient(headers, client);
            string jsonModel = request;

            var content = new StringContent(jsonModel, Encoding.UTF8, "application/json");

            using HttpResponseMessage httpResponse = await client.PostAsync(url, content);

            return await GetApiResponse<TResponse>(httpResponse);
        }

        /// <summary>
        /// Creates and external post request With XML payload. 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="clientName"></param>
        /// <param name="url"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<DefaultApiResponse> ConnectPostStringXML(string request, string clientName, string url, Dictionary<string, string> headers = null)

        {
            var client = _httpClient.CreateClient(clientName);

            AddHeadersToClient(headers, client);
            string xmlModel = request;

            var content = new StringContent(xmlModel, Encoding.UTF8, "application/xml");

            using HttpResponseMessage httpResponse = await client.PostAsync(url, content);

            return await GetApiResponse(httpResponse);
        }


        public async Task<TResponse> ConnectWithListRequest<TRequest, TResponse>(TRequest request, string clientName, string url, Dictionary<string, string> headers = null)
            where TResponse : IResponseModel, new()
        {
            var client = _httpClient.CreateClient(clientName);

            AddHeadersToClient(headers, client);

            string jsonModel = JsonConvert.SerializeObject(request, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });

            var content = new StringContent(jsonModel, Encoding.UTF8, "application/json");

            using HttpResponseMessage httpResponse = await client.PostAsync(url, content);


            return await GetApiResponse<TResponse>(httpResponse);

        }

        public async Task<TResponse> ConnectGet<TResponse>(string clientName, string url, Dictionary<string, string> headers = null)
          where TResponse : IResponseModel, new()
        {
            var client = _httpClient.CreateClient(clientName);

            AddHeadersToClient(headers, client);

            using HttpResponseMessage httpResponse = await client.GetAsync(url);


            return await GetApiResponse<TResponse>(httpResponse);
        }

        public async Task<DefaultApiResponse> ConnectGet(string clientName, string url, Dictionary<string, string> headers = null)

        {
            var client = _httpClient.CreateClient(clientName);

            AddHeadersToClient(headers, client);

            using HttpResponseMessage httpResponse = await client.GetAsync(url);


            return await GetApiResponse(httpResponse);
        }

        public async Task<DefaultApiResponse> ConnectPostMultipartFormData(MultipartFormDataContent content, string clientName, string url, Dictionary<string, string> headers = null)

        {
            var client = _httpClient.CreateClient(clientName);

            AddHeadersToClient(headers, client);

            using HttpResponseMessage httpResponse = await client.PostAsync(url, content);

            return await GetApiResponse(httpResponse);
        }

        private static void AddHeadersToClient(Dictionary<string, string> headers, HttpClient client)
        {
            if (headers is not null)
            {
                foreach (var item in headers)
                {
                    client.DefaultRequestHeaders.TryAddWithoutValidation(item.Key, item.Value);

                }
            }
        }
        private async Task<DefaultApiResponse> GetApiResponse(HttpResponseMessage httpResponse)
        {
            var statusCode = 0;
            try
            {
                httpResponse.EnsureSuccessStatusCode();

                try
                {
                    statusCode = Convert.ToInt32(httpResponse.StatusCode);
                }
                catch (Exception)
                {
                    statusCode = 500;
                }

                if (httpResponse.IsSuccessStatusCode)
                {
                    string responseBody = await httpResponse.Content.ReadAsStringAsync();
                    return new DefaultApiResponse { IsRequestSuccess = true, ResponseBody = responseBody, StatusCode = statusCode }; ;
                }


                return new DefaultApiResponse { IsRequestSuccess = false, StatusCode = statusCode };
            }
            catch (System.Exception e)
            {
                var responseResult = e.Message;

                return new DefaultApiResponse { IsRequestSuccess = false, JsonErrors = responseResult, StatusCode = statusCode };
            }
        }

        private async Task<TResponse> GetApiResponse<TResponse>(HttpResponseMessage httpResponse)
             where TResponse : IResponseModel, new()
        {
            try
            {
                httpResponse.EnsureSuccessStatusCode();

                if (httpResponse.IsSuccessStatusCode)
                {
                    var responseResult = await httpResponse.Content.ReadAsStringAsync();
                    TResponse responseModel = DeseriliazeJson<TResponse>(responseResult);
                    responseModel.StatusCode = Convert.ToInt32(httpResponse.StatusCode);
                    responseModel.IsRequestSuccess = true;
                    return responseModel;
                }


                return new TResponse { IsRequestSuccess = false };
            }
            catch (System.Exception ex)
            {
                var responseResult = await httpResponse.Content.ReadAsStringAsync();
                return new TResponse { IsRequestSuccess = false, JsonErrors = responseResult, StatusCode = Convert.ToInt32(httpResponse.StatusCode) };
            }


        }

        private static TResponse DeseriliazeJson<TResponse>(string responseResult) where TResponse : IResponseModel, new()
        {
            try
            {
                var result = JsonConvert.DeserializeObject<TResponse>(responseResult, new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                });


                return result;
            }
            catch (System.Exception ex)
            {
                return new TResponse { IsRequestSuccess = false };
            }
        }
        public async Task<TResponse> ConnectPut<TRequest, TResponse>(TRequest request, string clientName, string url, Dictionary<string, string> headers = null, bool camelCaseJson = true)
            where TRequest : IRequestModel, new()
            where TResponse : IResponseModel, new()
        {
            var client = _httpClient.CreateClient(clientName);

            AddHeadersToClient(headers, client);

            string jsonModel = "";
            if (camelCaseJson) jsonModel = request.ConvertToCamelCaseJson();
            else jsonModel = JsonConvert.SerializeObject(request);

            var content = new StringContent(jsonModel, Encoding.UTF8, "application/json");

            using HttpResponseMessage httpResponse = await client.PutAsync(url, content);

            return await GetApiResponse<TResponse>(httpResponse);
        }
        public async Task<TResponse> ConnectPost<TResponse>(string clientName, string url, Dictionary<string, string> headers = null) where TResponse : IResponseModel, new()
        {
            var client = _httpClient.CreateClient(clientName);

            AddHeadersToClient(headers, client);

            using HttpResponseMessage httpResponse = await client.PostAsync(url,null);

            return await GetApiResponse<TResponse>(httpResponse);
        }

    }
    public class DefaultApiResponse
    {
        public bool IsRequestSuccess { get; set; }
        public string JsonErrors { get; set; }
        public string ResponseBody { get; set; }
        public int? StatusCode { get; set; }
    }
}
