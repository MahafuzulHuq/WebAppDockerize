using EFCoreCodeFirstDemo.Models;
using Microsoft.AspNetCore.Mvc;
using WebAPIPrime.Data;

namespace EFCoreCodeFirstDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductsController(AppDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public IActionResult GetAllProducts()
        {
            var products = _context.Products.ToList();
            return Ok(products);

        }

        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                return NotFound(new { message = "Product not found!" });
            }
            return Ok(product);
        }

        [HttpPost]
        public IActionResult CreateNewProduct([FromBody] Product product)
        {
            if (product == null)
            {
                return BadRequest(new { message = "Invalid product data" });
            }

            _context.Products.Add(product);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
        }


        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, [FromBody] Product product)
        {
            var existingProduct = _context.Products.Find(id);
            if (existingProduct == null)
            {
                return NotFound(new { message = "Product not found!" });
            }

            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;
            _context.SaveChanges();

            return Ok(new { message = "Product updated successfully" });
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                return NotFound(new { message = "Product not found!" });
            }

            _context.Products.Remove(product);
            _context.SaveChanges();
            return Ok(new { message = "Product deleted successfully" });
        }

    }
}
