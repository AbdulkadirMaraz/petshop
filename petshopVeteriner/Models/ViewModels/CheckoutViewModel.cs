using System.ComponentModel.DataAnnotations;

namespace CustomerPetshop.Models.ViewModels
{
    public class CheckoutViewModel
    {
        [Required(ErrorMessage = "Ad Soyad gereklidir.")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "E-posta gereklidir.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Adres gereklidir.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Şehir gereklidir.")]
        public string City { get; set; }

        [Required(ErrorMessage = "Posta kodu gereklidir.")]
        public string PostalCode { get; set; }
    }
}
