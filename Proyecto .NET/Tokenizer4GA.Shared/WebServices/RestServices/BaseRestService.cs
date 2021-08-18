using Tokenizer4GA.Shared.Constants;
using Tokenizer4GA.Shared.Localization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Tokenizer4GA.Shared.WebServices.RestServices
{
    public abstract class BaseRestService
    {
        internal static bool UseDevelopmentServer { get; } = true;

        private static readonly HttpClient _httpClient;

        static BaseRestService()
        {
            _httpClient = new HttpClient{
                Timeout = TimeSpan.FromMilliseconds(Numbers.TimeoutMilliseconds)
            };
            
            _httpClient.DefaultRequestHeaders
              .Accept
              .Add(new MediaTypeWithQualityHeaderValue(Headers.ApplicationJson));
        }

        protected abstract string Server { get; }

        #region GET
        protected async Task<BaseResponse> GetAsync(string uri, BaseRequest baseRequest)
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, uri);

            SetAuthorizationHeader(baseRequest, request);

            SetAppVersionHeader(baseRequest, request);

            HttpResponseMessage response = null;
            try
            {
                response = await _httpClient.SendAsync(request);
            }
            catch (TaskCanceledException)
            {
                if (!string.IsNullOrWhiteSpace(baseRequest.BearerToken))
                    return new BaseResponse()
                    {
                        ResponseCode = 777
                    };
                return new BaseResponse()
                {
                    ResponseCode = 408
                };
            }

            var baseResponse = CreateBaseResponse(response);

            GetAuthorizationHeader(response, baseResponse);

            GetMessageHeader(response, baseResponse);

            return baseResponse;
        }

        protected async Task<BaseResponse> GetAsync<TRequestData>(string uri, BaseRequest<TRequestData> baseRequest)
        {
            var queryOrSingleParameter = CreateQueryOrSingleParameter(baseRequest);

            using var request = new HttpRequestMessage(HttpMethod.Get, $"{uri}{queryOrSingleParameter}");

            SetAuthorizationHeader(baseRequest, request);

            SetAppVersionHeader(baseRequest, request);

            HttpResponseMessage response = null;
            try
            {
                response = await _httpClient.SendAsync(request);
            }
            catch (TaskCanceledException)
            {
                if (!string.IsNullOrWhiteSpace(baseRequest.BearerToken))
                    return new BaseResponse()
                    {
                        ResponseCode = 777
                    };
                return new BaseResponse()
                {
                    ResponseCode = 408
                };
            }

            var baseResponse = CreateBaseResponse(response);

            GetAuthorizationHeader(response, baseResponse);

            GetMessageHeader(response, baseResponse);

            return baseResponse;
        }

        protected async Task<BaseResponse<TResponseData>> GetAsync<TResponseData>(string uri, BaseRequest baseRequest)
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, uri);

            SetAuthorizationHeader(baseRequest, request);

            SetAppVersionHeader(baseRequest, request);

            HttpResponseMessage response = null;
            try
            {
                response = await _httpClient.SendAsync(request);
            }
            catch (TaskCanceledException)
            {
                if (!string.IsNullOrWhiteSpace(baseRequest.BearerToken))
                    return new BaseResponse<TResponseData>()
                    {
                        ResponseCode = 777
                    };
                return new BaseResponse<TResponseData>()
                {
                    ResponseCode = 408
                };
            }

            var baseResponse = CreateBaseResponse<TResponseData>(response);

            GetAuthorizationHeader(response, baseResponse);

            GetMessageHeader(response, baseResponse);

            await GetContent(response, baseResponse);

            return baseResponse;
        }

        protected async Task<BaseResponse<TResponseData>> GetAsync<TResponseData, TRequestData>(string uri, TRequestData baseRequest)
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, uri);

            HttpResponseMessage response = null;
            try
            {
                response = await _httpClient.SendAsync(request);
            }
            catch (TaskCanceledException)
            {
            }

            var baseResponse = CreateBaseResponse<TResponseData>(response);

            GetAuthorizationHeader(response, baseResponse);

            GetMessageHeader(response, baseResponse);

            await GetContent(response, baseResponse);

            return baseResponse;
        }


        protected async Task<BaseResponse<TResponseData>> GetAsync<TResponseData, TRequestData>(string uri, BaseRequest<TRequestData> baseRequest)
        {
            var queryOrSingleParameter = CreateQueryOrSingleParameter(baseRequest);

            using var request = new HttpRequestMessage(HttpMethod.Get, $"{uri}{queryOrSingleParameter}");

            SetAuthorizationHeader(baseRequest, request);

            SetAppVersionHeader(baseRequest, request);

            HttpResponseMessage response = null;
            try
            {
                response = await _httpClient.SendAsync(request);
            }
            catch (TaskCanceledException)
            {
                if (!string.IsNullOrWhiteSpace(baseRequest.BearerToken))
                    return new BaseResponse<TResponseData>()
                    {
                        ResponseCode = 777
                    };
                return new BaseResponse<TResponseData>()
                {
                    ResponseCode = 408
                };
            }

            var baseResponse = CreateBaseResponse<TResponseData>(response);

            GetAuthorizationHeader(response, baseResponse);

            GetMessageHeader(response, baseResponse);

            await GetContent(response, baseResponse);

            return baseResponse;
        }
        #endregion GET

        #region POST
        protected async Task<BaseResponse> PostAsync(string uri, BaseRequest baseRequest)
        {
            using var request = new HttpRequestMessage(HttpMethod.Post, uri);

            SetAuthorizationHeader(baseRequest, request);

            SetAppVersionHeader(baseRequest, request);

            SetEmptyContent(request);

            HttpResponseMessage response = null;
            try
            {
                response = await _httpClient.SendAsync(request);
            }
            catch (TaskCanceledException)
            {
                if (!string.IsNullOrWhiteSpace(baseRequest.BearerToken))
                    return new BaseResponse()
                    {
                        ResponseCode = 777
                    };
                return new BaseResponse()
                {
                    ResponseCode = 408
                };
            }

            var baseResponse = CreateBaseResponse(response);

            GetAuthorizationHeader(response, baseResponse);

            GetMessageHeader(response, baseResponse);

            return baseResponse;
        }

        protected async Task<BaseResponse> PostAsync<TRequestData>(string uri, BaseRequest<TRequestData> baseRequest)
        {
            using var request = new HttpRequestMessage(HttpMethod.Post, uri);

            SetAuthorizationHeader(baseRequest, request);

            SetAppVersionHeader(baseRequest, request);

            SetContent(baseRequest, request);

            HttpResponseMessage response = null;
            try
            {
                response = await _httpClient.SendAsync(request);
            }
            catch (TaskCanceledException)
            {
                if (!string.IsNullOrWhiteSpace(baseRequest.BearerToken))
                    return new BaseResponse()
                    {
                        ResponseCode = 777
                    };
                return new BaseResponse()
                {
                    ResponseCode = 408
                };
            }

            var baseResponse = CreateBaseResponse(response);

            GetAuthorizationHeader(response, baseResponse);

            GetMessageHeader(response, baseResponse);

            return baseResponse;
        }

        protected async Task<BaseResponse<TResponseData>> PostAsync<TResponseData>(string uri, BaseRequest baseRequest)
        {
            using var request = new HttpRequestMessage(HttpMethod.Post, uri);

            SetAuthorizationHeader(baseRequest, request);

            SetAppVersionHeader(baseRequest, request);

            SetEmptyContent(request);

            HttpResponseMessage response = null;
            try
            {
                response = await _httpClient.SendAsync(request);
            }
            catch (TaskCanceledException)
            {
                if (!string.IsNullOrWhiteSpace(baseRequest.BearerToken))
                    return new BaseResponse<TResponseData>()
                    {
                        ResponseCode = 777
                    };

                return new BaseResponse<TResponseData>()
                {
                    ResponseCode = 408
                };
            }

            var baseResponse = CreateBaseResponse<TResponseData>(response);

            GetAuthorizationHeader(response, baseResponse);

            GetMessageHeader(response, baseResponse);

            await GetContent(response, baseResponse);

            return baseResponse;
        }

        protected async Task<BaseResponse<TResponseData>> PostAsync<TResponseData, TRequestData>(string uri, BaseRequest<TRequestData> baseRequest)
        {
            using var request = new HttpRequestMessage(HttpMethod.Post, uri);

            SetAuthorizationHeader(baseRequest, request);

            SetAppVersionHeader(baseRequest, request);

            SetContent(baseRequest, request);

            HttpResponseMessage response = null;
            try
            {
                response = await _httpClient.SendAsync(request);
            }
            catch (TaskCanceledException)
            {
                if (!string.IsNullOrWhiteSpace(baseRequest.BearerToken))
                    return new BaseResponse<TResponseData>()
                    {
                        ResponseCode = 777
                    };

                return new BaseResponse<TResponseData>()
                {
                    ResponseCode = 408
                };
            }
            catch (Exception)
            {
                if (!string.IsNullOrWhiteSpace(baseRequest.BearerToken))
                    return new BaseResponse<TResponseData>()
                    {
                        ResponseCode = 777
                    };
                return new BaseResponse<TResponseData>()
                {
                    ResponseCode = 408
                };
            }

            var baseResponse = CreateBaseResponse<TResponseData>(response);

            GetAuthorizationHeader(response, baseResponse);

            GetMessageHeader(response, baseResponse);

            await GetContent(response, baseResponse);

            return baseResponse;
        }
        #endregion POST

        #region Helper methods
        private static string CreateQueryOrSingleParameter<TRequestData>(BaseRequest<TRequestData> baseRequest)
        {
            var query = string.Empty;
            if (baseRequest.Data != null)
            {
                var requestDataType = baseRequest.Data.GetType();
                if (requestDataType.IsPrimitive)
                    query = $"/{baseRequest.Data}";
                else
                {
                    var jObject = JObject.FromObject(baseRequest.Data,
                        new JsonSerializer { NullValueHandling = NullValueHandling.Ignore });
                    query = string.Join("&",
                        jObject.Children()
                            .Cast<JProperty>()
                            .Select(jProperty => $"{jProperty.Name}={HttpUtility.UrlEncode(jProperty.Value.ToString())}"));
                    if (!string.IsNullOrWhiteSpace(query))
                        query = $"?{query}";
                }
            }
            return query;
        }

        private static void SetAuthorizationHeader(BaseRequest baseRequest, HttpRequestMessage request)
        {
            if (!string.IsNullOrWhiteSpace(baseRequest.BearerToken))
                request.Headers.Authorization = new AuthenticationHeaderValue(Headers.Bearer, baseRequest.BearerToken);
        }

        private static void SetAppVersionHeader(BaseRequest baseRequest, HttpRequestMessage request)
        {
            if (!string.IsNullOrWhiteSpace(baseRequest.AppVersion))
            {
                request.Headers.Add("AppVersion", baseRequest.AppVersion); //"0.1.11"
            }
        }

        private static void SetEmptyContent(HttpRequestMessage request) =>
            request.Content = new StringContent(Strings.EmptyJson, Encoding.UTF8, Headers.ApplicationJson);

        private static void SetContent<TRequestData>(BaseRequest<TRequestData> baseRequest, HttpRequestMessage request)
        {
            if (baseRequest.Data != null)
            {
                var requestDataType = baseRequest.Data.GetType();
                if (requestDataType.IsPrimitive)
                    request.Content = new StringContent(baseRequest.Data.ToString(), Encoding.UTF8, Headers.TextPlain);
                else
                {
                    var jsonBody = JsonConvert.SerializeObject(baseRequest.Data,
                        Formatting.None,
                        new JsonSerializerSettings
                        {
                            NullValueHandling = NullValueHandling.Ignore
                        });
                    request.Content = new StringContent(jsonBody, Encoding.UTF8, Headers.ApplicationJson);
                }
            }
        }

        private static BaseResponse CreateBaseResponse(HttpResponseMessage response)
        {
            return new BaseResponse
            {
                ResponseCode = (int)response.StatusCode,
                Success = response.IsSuccessStatusCode
            };
        }

        private static BaseResponse<TResponseData> CreateBaseResponse<TResponseData>(HttpResponseMessage response)
        {
            return new BaseResponse<TResponseData>
            {
                ResponseCode = (int)response.StatusCode,
                Success = response.IsSuccessStatusCode
            };
        }

        private static void GetAuthorizationHeader(HttpResponseMessage response, BaseResponse baseResponse)
        {
            if (response.Headers.TryGetValues(Headers.AuthorizationHeader, out var authorizationHeaders))
            {
                var authorizationHeader = authorizationHeaders.FirstOrDefault();
                if (authorizationHeader.Contains(Headers.Bearer))
                {
                    var authorizationHeaderParts = authorizationHeader.Split(Headers.Bearer);
                    if (authorizationHeaderParts.Length > 1)
                        baseResponse.BearerToken = authorizationHeaderParts[1].Trim();
                }
            }
        }

        private static void GetMessageHeader(HttpResponseMessage response, BaseResponse baseResponse)
        {
            if (response.Headers.TryGetValues(Headers.MessageHeader, out var messageHeaders))
                baseResponse.Message = HttpUtility.HtmlDecode(messageHeaders.FirstOrDefault());
        }

        private static async Task GetContent<TResponseData>(HttpResponseMessage response, BaseResponse<TResponseData> baseResponse)
        {
            if (baseResponse.Success
                && response.Content != null)
            {
                try
                {
                    var jsonContent = await response.Content.ReadAsStringAsync();
                    baseResponse.Data = JsonConvert.DeserializeObject<TResponseData>(jsonContent);
                }
                catch (JsonSerializationException)
                {
                    baseResponse.ResponseCode = 422;
                    baseResponse.Success = false;
                    baseResponse.Message = LocalizedStrings.IncorrectFormatServiceResponse;
                }
            }
        }
        #endregion Helper methods
    }
}
