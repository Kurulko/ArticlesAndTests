using Newtonsoft.Json;

namespace Articles.Models.ArticleModels
{
    public class News
    {
        [JsonProperty("status")]
        public string? Status { get; set; }

        [JsonProperty("totalResults")]
        public int? TotalResults { get; set; }

        [JsonProperty("results")]
        public List<Article>? Articles { get; set; }

        [JsonProperty("nextPage")]
        public int? NextPage { get; set; }
    }
}
