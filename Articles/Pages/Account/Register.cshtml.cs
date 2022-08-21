using Articles.Models.AccountModels;
using Articles.Models.DbModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Encodings.Web;

namespace Articles.Pages.Account
{
    public class RegisterModel : PageModel
    {
        readonly UserManager<User> userManager;
        readonly SignInManager<User> signInManager;
        public RegisterModel(UserManager<User> userManager, SignInManager<User> signInManager)
            => (this.userManager, this.signInManager) = (userManager, signInManager);

        [BindProperty]
        public Register Register { get; set; } = new();

        [BindProperty]
        public string? ReturnUrl { get; set; }

        public void OnGet(string? ReturnUrl)
        {
            if(ReturnUrl is not null)
                this.ReturnUrl = UrlEncoder.Default.Encode(ReturnUrl);
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                User user = new() { Email = Register.Email, UserName = Register.Name, DateTime = DateTime.Now };

                IdentityResult result = await userManager.CreateAsync(user, Register.Password);
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, Register.RememberMe);
                    await userManager.AddToRolesAsync(user, new List<string>() { "User" });
                    return Redirect(ReturnUrl ?? "/Index");
                }
                else
                    foreach (IdentityError error in result.Errors)
                        ModelState.AddModelError(string.Empty, error.Description);
            }
            return Page();
        }
    }
}
