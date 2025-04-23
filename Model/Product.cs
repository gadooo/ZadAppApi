using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ZadGroceryAppApi.Model
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }=string.Empty;
        [MaxLength(200)]
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }



        [ForeignKey(nameof(category))]
        public int CategoryId { get; set; }
        [JsonIgnore]
        public Category? category { get; set; }
    }
}
