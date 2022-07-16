using System.ComponentModel.DataAnnotations;

namespace eCommerceApp.ViewModels
{
    public class RegisterVM
    {
        [Display(Name = "Full name")]
        [Required(ErrorMessage = "Full name is requiered")]
        public string FullName { get; set; } = string.Empty;

        [Display(Name = "Email address")]
        [Required(ErrorMessage = "Email address is requiered")]
        public string EmailAddress { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Display(Name = "Confirm password")]
        [Required(ErrorMessage = "Confirm password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password fields do not match")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}