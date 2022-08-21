using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Articles.Models.AccountModels;
using Articles.Models.DbModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Articles.Pages.Account
{
    [Authorize]
    public class LogoutModel : PageModel
    {
        readonly SignInManager<User> signInManager;
        public LogoutModel (SignInManager<User> signInManager)
            => this.signInManager = signInManager;

        public async Task<IActionResult> OnGet()
        {
            await signInManager.SignOutAsync();
            return Redirect("/Index");
        }
    }
}
