using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Articles.Models.ArticleModels
{
    public class Article
    {
        [JsonProperty("title")]
        public string? Title { get; set; }

        [JsonProperty("link")]
        public string? Link { get; set; }

        [JsonProperty("keywords")]
        public List<string> Keywords { get; set; } = new();

        [JsonProperty("creator")]
        public List<string> Creators { get; set; } = new();

        [JsonProperty("video_url")]
        public object? VideoUrl { get; set; }

        [JsonProperty("description")]
        public string? Description { get; set; }

        [JsonProperty("content")]
        public string? Content { get; set; }

        [JsonProperty("pubDate")]
        public string? Date { get; set; }

        [JsonProperty("image_url")]
        public string? ImageUrl { get; set; }

        [JsonProperty("source_id")]
        public string? SourceId { get; set; }

        [JsonProperty("country")]
        public List<string> Countries { get; set; } = new();

        [JsonProperty("category")]
        public List<string> Categories { get; set; } = new();

        [JsonProperty("language")]
        public string? Language { get; set; }
    }
}
