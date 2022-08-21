using Articles.Models;
using Articles.Models.DbModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace Articles.Pages.Tests.Test.Exercises
{
    [Authorize(Roles = "Admin")]
    public class DeleteModel : PageModel
    {
        readonly ArticlesContext context;
        public DeleteModel(ArticlesContext context)
            => this.context = context;

        public async Task<IActionResult> OnGetAsync(int? testId, int? exerciseId)
        {
            if(testId is not null)
            {
                Models.DbModels.Test? test = await context.Tests.FirstOrDefaultAsync(t => t.Id == testId);
                if(test is not null)
                {
                    if(exerciseId is not null)
                    {
                        Exercise? exercise = await context.Exercises.FirstOrDefaultAsync(e => e.Id == exerciseId);
                        if(exercise is not null)
                        {
                            context.Exercises.Remove(exercise);
                            await context.SaveChangesAsync();
                        }
                    }
                    return RedirectToPage("Edit",new { testId = testId });
                    //return Redirect($"/Tests/Test/Exercises/Edit?id={testId}");
                }
            }
            return Redirect("/Tests");
        }
    }
}
