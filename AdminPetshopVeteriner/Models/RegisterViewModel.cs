using System.ComponentModel.DataAnnotations;

namespace AdminPetshop.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Lütfen Kullanıcı Adını Giriniz")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Lütfen Mail Giriniz")]
        public string Mail { get; set; }
        [Required(ErrorMessage = "Lütfen Şifreyi Giriniz")]
        public string Password { get; set; }


        [Required(ErrorMessage = "Lütfen Şifreyi Tekrar Giriniz")]
        [Compare("Password", ErrorMessage = "Şifreler Uyumlu Değil")]
        public string PasswordConfirm { get; set; }
        public string Address { get; set; }
        public bool IsAdmin { get; set; }
        public string PhoneNumber { get; set; }
    }
}
