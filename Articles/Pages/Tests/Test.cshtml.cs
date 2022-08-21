using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Articles.Models;
using Articles.Models.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Articles.Pages.Tests
{
    [IgnoreAntiforgeryToken]
    public class TestModel : PageModel
    {
        readonly ArticlesContext context;
        readonly UserManager<User> userManager;
        public TestModel(ArticlesContext context, UserManager<User> userManager)
            => (this.context, this.userManager) = (context, userManager);

        public Models.DbModels.Test? Test { get; set; }
        public bool HasValueTest => Test is not null;

        async Task<bool> SetTestAsync(int? id)
        {
            if (id is int _id)
            {
                var exercises = context.Tests.Include(t => t.Results).Include(t => t.Exercises);
                if (exercises.Any())
                    Test = await exercises?.ThenInclude(e => e.Answers).FirstOrDefaultAsync(t => t.Id == id);
                else
                    Test = await exercises?.FirstOrDefaultAsync(t => t.Id == id);

                return Test is not null;
            }
            return false;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
            => await SetTestAsync(id) ? Page() : RedirectToPage("Tests");

        public async Task<IActionResult> OnPostAsync(int? id, bool[] answers)
        {
            if (User.Identity?.IsAuthenticated ?? false && await SetTestAsync(id))
            {
                int count = answers.Where(a => a).Count();
                string userId = userManager.GetUserId(User);
                ResultOfTest result = new() { TestId = (int)id!, UserId = userId, CountOfCorrectAnswers = count, DateTime = DateTime.Now };

                if (!context.ResultsOfTests.Any(r => r.TestId == result.TestId && r.UserId == result.UserId))
                    context.ResultsOfTests.Add(result);
                else
                    context.ResultsOfTests.Update(result);
                await context.SaveChangesAsync();
            }
            return RedirectToPage("Tests");
        }
    }
}
