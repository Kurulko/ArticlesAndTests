using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Articles.Models;
using Articles.Models.DbModels;
using Microsoft.AspNetCore.Authorization;

namespace Articles.Pages.Tests
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        readonly ArticlesContext context;

        public EditModel(ArticlesContext context)
            => this.context = context;

        [BindProperty]
        public Models.DbModels.Test Test { get; set; }

        public async Task<IActionResult> OnGetAsync(int? testId)
        {
            if (testId is null || !context.Tests.Any())
                return NotFound();

            Models.DbModels.Test? test =  await context.Tests.FirstOrDefaultAsync(m => m.Id == testId);

            if (test is null)
                return NotFound();
            Test = test;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            context.Tests.Update(Test);
            await context.SaveChangesAsync();

            return Redirect("/Tests");
        }
    }
}
