using Articles.Models;
using Articles.Models.DbModels;
using Articles.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Articles.Pages.Users
{
    [Authorize]
    public class UserModel : PageModel
    {
        readonly ArticlesContext context;
        readonly UserManager<User> userManager;
        public UserModel(ArticlesContext context, UserManager<User> userManager)
            => (this.context, this.userManager) = (context, userManager);

        public UserAndRole UserAndRole { get; set; }

        async Task<User?> GetUserAsync(string? userId)
        {
            User? user = null;
            if (userId is not null)
            {
                var results = context.Users.Include(u => u.ResultsOfTests);
                if (results.Any())
                {
                    var test = results?.ThenInclude(r => r.Test);
                    if(test is not null)
                        user = await test?.ThenInclude(t => t.Exercises).FirstOrDefaultAsync(u => u.Id == userId)!;
                    else
                        user = await test?.FirstOrDefaultAsync(u => u.Id == userId)!;
                }
                else
                    user = await results?.FirstOrDefaultAsync(u => u.Id == userId)!;
            }
            return user;
        }

        public async Task OnGetAsync()
        {
            string userId = userManager.GetUserId(base.User);
            User? user = await GetUserAsync(userId);

            string? role = User.Identity?.Name;

            UserAndRole = new(user!, role);
        }

        public async Task<IActionResult> OnGetByIdAsync(string userId)
        {
            if (User.Identity?.Name?.Equals("Admin") ?? false)
            {
                User? user = await GetUserAsync(userId);

                string? roleId = (await context.UserRoles.FirstOrDefaultAsync(u => u.UserId == userId))?.RoleId;
                string? role = roleId is not null ? (await context.Roles.FirstOrDefaultAsync(r => r.Id == roleId))?.Name : null;

                if (user is not null)
                {
                    UserAndRole = new(user, role);
                    return Page();
                }
            }
            return RedirectToPage("Users");
        }
    }
}
