using System.ComponentModel.DataAnnotations;

namespace AdminPetshop.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Lütfen Kullanıcı Adını Girin")]
        public string userName { get; set; }

        [Required(ErrorMessage = "Lütfen Şifreyi Girin")]
        public string password { get; set; }

        public bool admin { get; set; }
    }
}
