using Articles.Models;
using Articles.Models.DbModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace Articles.Pages.Tests.Test.Exercises
{
    [Authorize(Roles = "Admin")]
    public class AddModel : PageModel
    {
        readonly ArticlesContext context;
        public AddModel(ArticlesContext context)
            => this.context = context;

        [BindProperty]
        public Exercise Exercise { get; set; }
        public Models.DbModels.Test Test { get; set; }

        async Task<bool> SetTestAsync(int? testId)
        {
            if (testId is int _testId)
            {
                Test = await context.Tests.FirstOrDefaultAsync(t => t.Id == _testId);
                return Test is not null;
            }
            return false;
        }

        public async Task<IActionResult> OnGetAsync(int? testId)
        {
            if (!await SetTestAsync(testId))
                return Redirect("/Tests");
            Exercise = new();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? testId)
        {
            if (await SetTestAsync(testId))
            {
                if (ModelState.IsValid && Exercise is not null)
                {
                    Exercise.TestId = (int)testId!;
                    Exercise.Id = 0;
                    await context.Exercises.AddAsync(Exercise);
                    await context.SaveChangesAsync();
                }

                return Page();
            }
            return Redirect("/Tests");
        }
    }
}
