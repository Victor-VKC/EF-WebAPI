using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Data.Transport
{
    public class ApiAdapter<T> : IDisposable where T : class 
    {
        public string BaseUrl = "http://localhost:33000/api/";

        private readonly HttpClient _client;

        private static ApiAdapter<T> _instance = null;

        public static ApiAdapter<T> Instance
        {
            get { return _instance ?? (_instance = new ApiAdapter<T>()); }
        }

        public ApiAdapter()
        {
            _client = new HttpClient() {BaseAddress = new Uri(BaseUrl) };
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public T Get(string relativeUri)
        {
            if (String.IsNullOrWhiteSpace(relativeUri)) return null;
            T result = null;

            using (var response = _client.GetAsync(relativeUri).Result)
            {
                if (response.IsSuccessStatusCode)
                    result = response.Content.ReadAsAsync<T>().Result;
            }
            return result;
        }

        public T Post(T value, string relativeUri)
        {
            if (String.IsNullOrWhiteSpace(relativeUri) || value == null) return null;
            T result = null;
            using (var response = _client.PostAsJsonAsync(relativeUri, value).Result)
            {
                if (response.IsSuccessStatusCode)
                    result = response.Content.ReadAsAsync<T>().Result;
            }
            return result;
        }

        public bool Put(T value, string relativeUri)
        {
            if (String.IsNullOrWhiteSpace(relativeUri) || value == null) return false;
            var result = false;
            using (var response = _client.PutAsJsonAsync(relativeUri, value).Result)
            {
                if (response.IsSuccessStatusCode)
                    result = response.Content.ReadAsAsync<bool>().Result;
            }
            return result;
        }

        public void Delete(string relativeUri)
        {
            if (String.IsNullOrWhiteSpace(relativeUri)) return;
            _client.DeleteAsync(relativeUri);
        }

        public void Dispose()
        {
            if (_client != null) _client.Dispose();
        }
    }
}
