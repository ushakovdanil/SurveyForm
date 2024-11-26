using System;
using System.Collections;
using System.Text;
using Api.Models.Model;
using Api.Services.Abstract;
using Newtonsoft.Json;
using static System.Net.WebRequestMethods;

namespace Api.Services
{
	public class NetworkService : INetworkService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializer _serializer = new();

        public NetworkService()
        { 
            _httpClient = CreateHttpClient();
        }

        private HttpClient CreateHttpClient()
        {
            var innerHttpHandler = new HttpClientHandler();

            var httpClient = new HttpClient()
            {
                //Change this url to baseUrl depends of env dev/stage/prod
                BaseAddress = new Uri($"http://localhost/ServiceModel/SurveyFormService.svc/")
            };

            httpClient.Timeout = TimeSpan.FromSeconds(5000);
            return httpClient;
        }

        public async Task<ServiceResponse<TResult?>> MakeApiCall<TResult>(
          string path,
          HttpMethod method,
          CancellationToken cancellationToken = default,
          object? data = null)
          where TResult : class
        {
            var url = $"http://localhost/ServiceModel/SurveyFormService.svc/{path}";
            
            if (method == HttpMethod.Get && data != null)
            {
                if (data is not Dictionary<string, object> parameters)
                {
                    throw new Exception("Parameters for GET method should be in Dictionary<string, object>");
                }

                url += CreateQueryString(parameters);
            }

            using var request = new HttpRequestMessage
            {
                RequestUri = new Uri(url),
                Method = method,
            };

            if (method != HttpMethod.Get)
            {
                var json = JsonConvert.SerializeObject(data);

                request.Content = new StringContent(json, Encoding.UTF8, "application/json");
            }

            var response = new HttpResponseMessage();
            try
            {
                response = await _httpClient.SendAsync(request, cancellationToken)
                    .ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                if (ex is not TaskCanceledException and OperationCanceledException)
                {
                }
                return ServiceResponseBuilder.Failure<TResult>(new List<string> { "Connection Error" });
            }

            try
            {
                using var stream = await response.Content.ReadAsStreamAsync();
                using var reader = new StreamReader(stream);
                using var json = new JsonTextReader(reader);

                if (cancellationToken.IsCancellationRequested)
                {
                    return null;
                }

                if (response.IsSuccessStatusCode)
                {
                    if (stream.Length == 0)
                    {
                        return null;
                    }

                    var result = _serializer.Deserialize<TResult>(json);

                    return result;
                }
                else
                {
                    var error = _serializer.Deserialize<TResult>(json);

                    return error;
                }
            }
            catch (Exception exception)
            {
                return ServiceResponseBuilder.Failure<TResult>(new List<string> { "DeserializationError" });
            }
            finally
            {
                response.Dispose();
            }
        }

        private static string CreateQueryString(Dictionary<string, object> parameters)
        {
            var buider = new StringBuilder("?");

            foreach (var pair in parameters)
            {
                if (pair.Value is IList list)
                {
                    foreach (var item in list)
                    {
                        buider.Append($"{pair.Key}={item}&");
                    }
                }
                else
                {
                    buider.Append($"{pair.Key}={pair.Value}&");
                }
            }

            buider.Length--;

            return buider.ToString();
        }
    }
}

