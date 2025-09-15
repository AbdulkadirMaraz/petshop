using DataAccessLayer.Concrete;
using EntityLayer.Concrete;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PetShopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly PetShopContext _context;

        public ProductController(PetShopContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetList()
        {
            var values = _context.Products.ToList();
            return Ok(values);
        }
   

        [HttpPost]
        public IActionResult CreateProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return Ok("Ürün Ekleme işlemi başarılı");
        }
        [HttpDelete]
         
        public IActionResult DeleteProduct(int id)
        {
            var value = _context.Products.Find(id);
            _context.Products.Remove(value);
            _context.SaveChanges();
            return Ok("Ürün Silme İşlemi Başarılı");
        }
        [HttpGet("GetProduct")]
        public IActionResult GetProduct(int id)
        {
            var value = _context.Products.Find(id);
            return Ok(value);
        }
        [HttpPut]
        public IActionResult UpdateProduct(Product product)
        {
            _context.Products.Update(product);
            _context.SaveChanges();
            return Ok("Ürün güncelleme işlemi başarılı");
        }
    }
}
