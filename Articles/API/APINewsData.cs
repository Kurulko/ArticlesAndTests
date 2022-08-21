using Articles.Models.ArticleModels;
using System.Text;

namespace Articles.API
{
    public record APINewsData(string Url, string APIKey, string? Theme, List<Language> Languages) : APIRequest(Url)
    {
        public override string GetFullUrl()
        {
            StringBuilder builder = new();

            builder.Append(Url);
            builder.Append($"?apikey={APIKey}");

            if (!string.IsNullOrEmpty(Theme))
                builder.Append($"&q={Theme}");

            if (Languages.Any())
            {
                builder.Append($"&language=");
                int i = 0;
                foreach (Language language in Languages)
                {
                    builder.Append(language.ToString().ToLower());
                    if (i != Languages.Count - 1)
                        builder.Append(",");
                    ++i;
                }
            }

            return builder.ToString();
        }
    }

}
