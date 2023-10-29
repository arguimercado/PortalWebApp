using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Portal.Support.Sessions;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using WebApp.Service.Commons;
using WebApp.Service.Sessions;

namespace WebApp.Service.Http
{

    public class HttpService
    {
        private readonly HttpClient _client;
        private readonly IStorage _storage;

        public HttpService(HttpClient client, IStorage storage)
        {
            _storage = storage;
            _client = client;
        }

        private async Task AddToken()
        {
            var token = await _storage.ReadAsync<UserSession>("UserSession") ?? new();
            if (!string.IsNullOrEmpty(token.Token)) {
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Token);
            }
        }

        public Uri? GetUrlBase()
        {
            return _client.BaseAddress;
        }


        public async Task<string> GetScalarAsync(string url, Dictionary<string, object>? queryParameters = null)
        {
            if (queryParameters is not null)
            {
                string strParam = "";
                foreach (var item in queryParameters)
                {
                    strParam += $"{item.Key}={item.Value}&";
                }
                url += $"?{strParam.Substring(0, strParam.Length - 1)}";
            }

            await AddToken();
            var result = await _client.GetAsync(url);
            if (!result.IsSuccessStatusCode)
            {
                if (result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    var error = await result.Content.ReadFromJsonAsync<ErrorModel>();
                    throw new ErrorResultException(error ?? new ErrorModel());
                }
            }

            return await result.Content.ReadAsStringAsync();
        }

        public async Task<T?> GetAsync<T>(string url, Dictionary<string, object>? queryParameters = null)
        {
            try
            {
                if (queryParameters is not null)
                {
                    string strParam = "";
                    foreach (var item in queryParameters)
                    {
                        strParam += $"{item.Key}={item.Value}&";
                    }
                    url += $"?{strParam.Substring(0, strParam.Length - 1)}";
                }

                await AddToken();
                var result = await _client.GetAsync(url);
                if (!result.IsSuccessStatusCode)
                {
                    if (result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        var error = await result.Content.ReadFromJsonAsync<ErrorModel>();
                        throw new ErrorResultException(error ?? new ErrorModel());
                    }
                }

                return await result.Content.ReadFromJsonAsync<T>();

            }
            catch (ErrorResultException error)
            {
                throw new Exception(error.Error.Detail);
            }
            catch (HttpRequestException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public HttpClient ToHttp => _client;

        public async Task<HttpResponseMessage?> PostFormData(string url, MultipartFormDataContent formData)
        {
            await AddToken();
            var result = await _client.PostAsync(url, formData);
            return result;
        }



        //will be deprecated
        public async Task<bool> Post<TParam>(string url, TParam value, CancellationToken cancellationToken = default)
        {
            try
            {
                await AddToken();
                var response = await _client.PostAsJsonAsync(url, value, cancellationToken);
                if (!response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        var error = await response.Content.ReadFromJsonAsync<ErrorModel>();
                        throw new ErrorResultException(error ?? new ErrorModel());
                    }
                }

                return true;

            }
            catch (ErrorResultException error)
            {
                throw new Exception(error.Error.Detail);
            }
            catch (HttpRequestException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<HttpResponseMessage> PostScalarAsync(string url, Dictionary<string, string> value, CancellationToken cancellationToken = default)
        {
            try
            {
                await AddToken();
                var json = JsonConvert.SerializeObject(value);
                var ids = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _client.PostAsync(url, ids, cancellationToken);

                return response;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<TResult> PostScalarAsync<TResult>(string url, Dictionary<string, string> value, CancellationToken cancellationToken = default)
        {
            try
            {
                await AddToken();
                var json = JsonConvert.SerializeObject(value);
                var ids = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _client.PostAsync(url, ids, cancellationToken);
                if (!response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        var error = await response.Content.ReadFromJsonAsync<ErrorModel>();
                        throw new ErrorResultException(error ?? new ErrorModel());
                    }
                }

                var returnValue = await response.Content.ReadFromJsonAsync<TResult>();

                return returnValue;
            }
            catch (ErrorResultException error)
            {
                throw new Exception(error.Error.Detail);
            }
            catch (HttpRequestException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<HttpResponseMessage> PostAsync<TParam>(string url, TParam value, CancellationToken cancellationToken = default)
        {
            try
            {
                await AddToken();
                var response = await _client.PostAsJsonAsync(url, value, cancellationToken);
                if (!response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        var error = await response.Content.ReadFromJsonAsync<ErrorModel>();
                        throw new ErrorResultException(error ?? new ErrorModel());
                    }
                }

                return response;
            }
            catch (ErrorResultException error)
            {
                throw new Exception(error.Error.Detail);
            }
            catch (HttpRequestException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<TResult> PostAsync<TParam, TResult>(string url, TParam value, CancellationToken cancellationToken = default)
        {
            try
            {
                await AddToken();
                var response = await _client.PostAsJsonAsync(url, value, cancellationToken);
                if (!response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        var error = await response.Content.ReadFromJsonAsync<ErrorModel>();
                        throw new ErrorResultException(error ?? new ErrorModel());
                    }
                }

                var returnValue = await response.Content.ReadFromJsonAsync<TResult>();

                return returnValue;

            }
            catch (ErrorResultException error)
            {
                throw new Exception(error.Error.Detail);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteAsync(string url)
        {
            await _client.DeleteAsync(url);
        }

    }
}
