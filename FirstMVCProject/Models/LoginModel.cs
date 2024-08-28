using System.ComponentModel.DataAnnotations;

namespace FirstMVCProject.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage="Enter Username")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Enter Password")]
        public string Password { get; set; }
    }
}
