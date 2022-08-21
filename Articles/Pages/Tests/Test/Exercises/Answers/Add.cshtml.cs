using Articles.Models;
using Articles.Models.DbModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace Articles.Pages.Tests.Test.Exercises.Answers
{
    [Authorize(Roles = "Admin")]
    public class AddModel : PageModel
    {
        readonly ArticlesContext context;
        public AddModel(ArticlesContext context)
            => this.context = context;

        [BindProperty]
        public Answer Answer { get; set; }
        public Exercise Exercise { get; set; }
        public Models.DbModels.Test Test { get; set; }

        async Task<bool> SetTestAndExerciseAsync(int? testId, int? exerciseId)
        {
            if (testId is int _testId && exerciseId is int _exerciseId)
            {
                Test = await context.Tests.FirstOrDefaultAsync(t => t.Id == _testId)!;
                Exercise = await context.Exercises.FirstOrDefaultAsync(t => t.Id == _exerciseId)!;
                return Test is not null && Exercise is not null;
            }

            return false;
        }

        public async Task<IActionResult> OnGetAsync(int? testId, int? exerciseId)
            => await SetTestAndExerciseAsync(testId, exerciseId) ? Page() : Redirect("/Tests");
        
        public async Task<IActionResult> OnPostAsync(int? testId, int? exerciseId)
        {
            if(await SetTestAndExerciseAsync(testId, exerciseId))
            {
                if (ModelState.IsValid && Answer is not null)
                {
                    Answer.ExerciseId = (int)exerciseId!;
                    await context.Answers.AddAsync(Answer);
                    await context.SaveChangesAsync();
                    return Redirect($"/Tests/Test/Exercises/Edit?testId={testId}");
                }

                return Page();
            }
            return Redirect("/Tests");
        }

    }
}
