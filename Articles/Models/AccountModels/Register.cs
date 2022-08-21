using System.ComponentModel.DataAnnotations;

namespace Articles.Models.AccountModels
{
    public class Register : Account
    {
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [Display(Name = "Confirm password*")]
        [Required(ErrorMessage = "Repeat password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords don't match")]
        public string PasswordConfirm { get; set; }
    }
}
