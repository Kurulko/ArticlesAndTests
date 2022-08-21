using Articles.Models;
using Articles.Models.DbModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace Articles.Pages.Tests.Test.Exercises.Answers
{
    [Authorize(Roles = "Admin")]
    public class DeleteModel : PageModel
    {
        readonly ArticlesContext context;
        public DeleteModel(ArticlesContext context)
            => this.context = context;

        public async Task<IActionResult> OnGetAsync(int? testId, int? answerId)
        {
            if (testId is not null)
            {
                Models.DbModels.Test? test = await context.Tests.FirstOrDefaultAsync(t => t.Id == testId);
                if (test is not null)
                {
                    if (answerId is not null)
                    {
                        Answer? answer = await context.Answers.FirstOrDefaultAsync(a => a.Id == answerId);
                        if (answer is not null)
                        {
                            context.Answers.Remove(answer);
                            await context.SaveChangesAsync();
                        }
                    }
                    return Redirect($"/Tests/Test/Exercises/Edit?testId={testId}");
                }
            }
            return Redirect("/Tests");
        }
    }
}
