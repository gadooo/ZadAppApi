using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZadGroceryAppApi.DTOs;
using ZadGroceryAppApi.Model;

namespace ZadGroceryAppApi.Controllers
{
    //[Authorize(Roles = "user")]

    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IImageService _imageService;

        public ProductsController(ApplicationDbContext context , IImageService imageService)
        {
            _context = context;
            _imageService = imageService;

        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
        {
            var products = await _context.Products.ToListAsync();

            var productDtos = products.Select(product => new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                CategoryId = product.CategoryId,
                ImageUrl = product.Image != null ? Convert.ToBase64String(product.Image) : null
            }).ToList();

            return Ok(productDtos);
        }


        // GET: api/Products/5
        [Authorize(Roles = "user")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult<ProductDto>> CreateProduct([FromForm] ProductCreateDto productDto)
        {
            var product = new Product
            {
                Name = productDto.Name,
                Description = productDto.Description,
                Price = productDto.Price,
                CategoryId = productDto.CategoryId
            };

            if (productDto.ImageFile != null)
            {
                if (!_imageService.IsImageValid(productDto.ImageFile))
                {
                    return BadRequest("Invalid image file");
                }

                // نحول الصورة من IFormFile إلى byte[]
                using (var memoryStream = new MemoryStream())
                {
                    await productDto.ImageFile.CopyToAsync(memoryStream);
                    product.Image = memoryStream.ToArray();
                }
            }

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return Created();
        }


        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }

        //[Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromForm] ProductCreateDto productDto)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            // نحدث بيانات المنتج
            product.Name = productDto.Name;
            product.Description = productDto.Description;
            product.Price = productDto.Price;
            product.CategoryId = productDto.CategoryId;

            // لو في صورة جديدة
            if (productDto.ImageFile != null)
            {
                if (!_imageService.IsImageValid(productDto.ImageFile))
                {
                    return BadRequest("Invalid image file");
                }

                using (var memoryStream = new MemoryStream())
                {
                    await productDto.ImageFile.CopyToAsync(memoryStream);
                    product.Image = memoryStream.ToArray();
                }
            }

            await _context.SaveChangesAsync();

            return NoContent();
        }
        //[Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
