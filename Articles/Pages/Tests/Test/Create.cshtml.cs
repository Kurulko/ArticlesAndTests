using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Articles.Models;
using Articles.Models.DbModels;
using Microsoft.AspNetCore.Authorization;

namespace Articles.Pages.Tests
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        readonly ArticlesContext context;

        public CreateModel(ArticlesContext context)
            => this.context = context;

        [BindProperty]
        public Models.DbModels.Test Test { get; set; }

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || Test is null)
                return Page();

            context.Tests.Add(Test);
            await context.SaveChangesAsync();

            return Redirect("/Tests");
        }
    }
}
