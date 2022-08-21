using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Articles.Models;
using Articles.Models.DbModels;
using Microsoft.AspNetCore.Authorization;

namespace Articles.Pages.Tests
{
    [Authorize(Roles = "Admin")]
    public class DeleteModel : PageModel
    {
        readonly ArticlesContext context;

        public DeleteModel(ArticlesContext context)
            => this.context = context;

        public async Task<IActionResult> OnGetAsync(int? testId)
        {
            if (context.Tests.Any() && testId is int _testId)
            {
                Models.DbModels.Test? test = await context.Tests.FirstOrDefaultAsync(t => t.Id == _testId);
                if (test is not null)
                {
                    context.Tests.Remove(test);
                    await context.SaveChangesAsync();
                }
            }

            return Redirect("/Tests");
        }
    }
}
