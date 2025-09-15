using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Identity.Client;

namespace CustomerPetshop.Models.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; } 
        public string Name { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public string ImageUrl2 { get; set; } = string.Empty;
        public string ImageUrl3 { get; set; } = string.Empty;
        public string ImageUrl4 { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Tags { get; set; } = string.Empty;
        public int CategoryId { get; set; }

        public string CategoryName { get; set; } = string.Empty; 

        public List<SelectListItem>? CategoryOptions { get; set; } 
    }
}
