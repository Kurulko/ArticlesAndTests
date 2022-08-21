using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Articles.Models;
using Articles.Models.DbModels;
using Microsoft.EntityFrameworkCore;
using Articles.Models.ViewModels;

namespace Articles.Pages.Users
{
    [Authorize(Roles = "Admin")]
    public class UsersModel : PageModel
    {
        readonly ArticlesContext context;
        public UsersModel(ArticlesContext context)
            => this.context = context;

        public List<UserAndRole> UsersAndRoles { get; set; } = new();
        public async Task OnGetAsync()
        {
            foreach (User user in context.Users.ToList())
            {
                string? roleId =  (await context.UserRoles.FirstOrDefaultAsync(u => u.UserId == user.Id))?.RoleId;
                string? role = roleId is not null ? (await context.Roles.FirstOrDefaultAsync(r  => r.Id == roleId))?.Name : null;
                UserAndRole userAndRole = new(user, role);
                UsersAndRoles.Add(userAndRole);
            }
        }
    }
}
