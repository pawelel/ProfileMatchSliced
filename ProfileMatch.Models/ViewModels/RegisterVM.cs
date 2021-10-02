using System.ComponentModel.DataAnnotations;

namespace ProfileMatch.Models.ViewModels
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Email confirmation is required.")]
        [Compare("Email", ErrorMessage = "The Email and confirmation Email do not match.")]
        public string ConfirmEmail { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}