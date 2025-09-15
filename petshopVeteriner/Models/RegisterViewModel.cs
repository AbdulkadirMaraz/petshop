using System.ComponentModel.DataAnnotations;

namespace CustomerPetshop.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Kullanıcı adı gerekli")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email gerekli")]
        [EmailAddress]
        public string Mail { get; set; }

        [Required(ErrorMessage = "Şifre gerekli")]

        public string Password { get; set; }

        [Required(ErrorMessage = "Şifre tekrar gerekli")]

        [Compare("Password", ErrorMessage = "Şifreler uyuşmuyor")]
        public string PasswordConfirm { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }


    }
}
