using Articles.Models;
using Articles.Models.DbModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Articles.Pages.Tests
{
    public class TestsModel : PageModel
    {
        readonly ArticlesContext context;
        public TestsModel(ArticlesContext context)
            => this.context = context;
        public IEnumerable<Models.DbModels.Test> Tests { get; set; }
        public void OnGet()
            => Tests = context.Tests.ToList();
    }
}
