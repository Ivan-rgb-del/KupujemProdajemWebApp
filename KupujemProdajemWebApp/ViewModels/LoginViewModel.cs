using System.ComponentModel.DataAnnotations;

namespace KupujemProdajemWebApp.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email address is required!")]
        public string EmailAddress { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
