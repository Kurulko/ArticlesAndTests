using System.ComponentModel.DataAnnotations;

namespace Articles.Models.AccountModels
{
    public class Account
    {
        [Display(Name = "Your name*")]
        [Required(ErrorMessage = "Enter your name!")]
        public string Name { get; set; }

        [Display(Name = "Your password*")]
        [Required(ErrorMessage = "Enter your password!")]
        [DataType(DataType.Password, ErrorMessage = "Not strong password!")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
