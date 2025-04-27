using System.ComponentModel.DataAnnotations;

namespace ZadGroceryAppApi.DTOs
{
    public class ProductCreateDto
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }
        public IFormFile ImageFile { get; set; }
        public int CategoryId { get; set; }

    }
}
