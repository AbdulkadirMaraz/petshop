namespace CustomerPetshop.Models.ViewModels
{
    public class CategoryWithProductsViewModel
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string CategoryImageUrl { get; set; } = string.Empty;
        public List<ProductViewModel> Products { get; set; } = new();
    }
}
