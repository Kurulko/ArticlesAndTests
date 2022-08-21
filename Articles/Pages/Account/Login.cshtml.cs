using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Articles.Models.AccountModels;
using Articles.Models.DbModels;
using Microsoft.AspNetCore.Identity;
using System.Text.Encodings.Web;

namespace Articles.Pages.Account
{
    public class LoginModel : PageModel
    {
        readonly UserManager<User> userManager;
        readonly SignInManager<User> signInManager;
        public LoginModel(UserManager<User> userManager, SignInManager<User> signInManager)
            => (this.userManager, this.signInManager) = (userManager, signInManager);

        [BindProperty]
        public Login Login { get; set; } = new();

        [BindProperty]
        public string? ReturnUrl { get; set; }

        public void OnGet(string ReturnUrl)
        {
            if (ReturnUrl is not null)
                this.ReturnUrl = UrlEncoder.Default.Encode(ReturnUrl);
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(Login.Name, Login.Password, Login.RememberMe, false);
                if (result.Succeeded)
                    return Redirect(ReturnUrl ?? "/Index");
                else
                    ModelState.AddModelError(string.Empty, "Wrong password or/and email");
            }
            return Page();
        }
    }
}
