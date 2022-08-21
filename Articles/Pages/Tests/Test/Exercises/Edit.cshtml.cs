using Articles.Models;
using Articles.Models.DbModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace Articles.Pages.Tests.Test.Exercises
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        readonly ArticlesContext context;
        public EditModel(ArticlesContext context)
            => this.context = context;

        [BindProperty]
        public List<Exercise> Exercises { get; set; }
        public Models.DbModels.Test Test { get; set; }

        async Task<bool> SetTestAsync(int? testId)
        {
            if (testId is int _testId)
            {
                var exercises = context.Tests.Include(t => t.Exercises);
                if(exercises.Any())
                    Test = await exercises?.ThenInclude(e => e.Answers).FirstOrDefaultAsync(t => t.Id == _testId);
                else
                    Test = await exercises?.FirstOrDefaultAsync(t => t.Id == _testId);

                return Test is not null;
            }
            return false;
        }

        public async Task<IActionResult> OnGetAsync(int? testId)
        {
            if(await SetTestAsync(testId))
            {
                Exercises = Test?.Exercises.ToList();
                return Page();
            }

            return NotFound();
        }

        public async Task<IActionResult> OnPostAsync(int? testId)
        {
            if (await SetTestAsync(testId))
            {
                if (!ModelState.IsValid)
                {
                    if(!Exercises.Any())
                        Exercises = Test?.Exercises?.ToList();
                    return Page();
                }

                context.Exercises.UpdateRange(Exercises);
                context.SaveChanges();
                return Redirect($"/Tests/Test/{testId}");
            }

            return NotFound();
        }

    }
}
