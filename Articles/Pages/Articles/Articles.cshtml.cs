using Articles.API;
using Articles.Models.ArticleModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;

namespace Articles.Pages.Articles
{
    //https://newsdata.io/api/1/news?apikey=pub_10071d7a1aa644cab4123dc43f964ec49c74e&q=computer&language=en
    public class ArticlesModel : PageModel
    {
        public News? News { get; private set; }

        const string newsDataUrl = "https://newsdata.io/api/1/news";
        const string newsDataAPIKey = "pub_10071d7a1aa644cab4123dc43f964ec49c74e";
        public async Task OnGetAsync(string theme, List<Language> languages)
        {
            APIRequest api = new APINewsData(newsDataUrl, newsDataAPIKey, theme, languages);
            ResponseBody response = new(api);
            News = await response.GetResponseBodyAsTAsync<News>();
        }
    }
}
