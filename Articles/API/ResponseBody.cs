using Articles.Models.ArticleModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Articles.API
{
    record ResponseBody(string Url)
    {
        public ResponseBody(APIRequest request) : this(request.GetFullUrl()) { }

        async Task<HttpResponseMessage> GetHttpResponseMessage()
            => (await new HttpClient().GetAsync(Url)).EnsureSuccessStatusCode();
        public async Task<string> GetResponseBodyAsStringAsync()
            => await (await GetHttpResponseMessage()).Content.ReadAsStringAsync();
        public async Task<T?> GetResponseBodyAsTAsync<T>()
            => JsonConvert.DeserializeObject<T>(await GetResponseBodyAsStringAsync());
    }
}
