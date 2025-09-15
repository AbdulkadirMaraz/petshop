using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PetShopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatagoriesController : ControllerBase
    {
        private readonly PetShopContext _context;

        public CatagoriesController(PetShopContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetList()
        {
            var values = _context.Categoryies.ToList();
            return Ok(values);
        }
   

        [HttpPost]
        public IActionResult CreateCategory(Category category)
        {
            _context.Categoryies.Add(category);
            _context.SaveChanges();
            return Ok("Kategori Ekleme işlemi başarılı");
        }
        [HttpDelete]
         
        public IActionResult DeleteCategory(int id)
        {
            var value = _context.Categoryies.Find(id);
            _context.Categoryies.Remove(value);
            _context.SaveChanges();
            return Ok("Kategori Silme İşlemi Başarılı");
        }
        [HttpGet("GetCategory")]
        public IActionResult GetCategory(int id)
        {
            var value = _context.Categoryies.Find(id);
            return Ok(value);
        }
        [HttpPut]
        public IActionResult UpdateCategory(Category category)
        {
            _context.Categoryies.Update(category);
            _context.SaveChanges();
            return Ok("Kategori güncelleme işlemi başarılı");
        }
    }
}
