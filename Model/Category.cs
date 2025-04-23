﻿using System.ComponentModel.DataAnnotations;


namespace ZadGroceryAppApi.Model
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]  
        [StringLength(50)]
        public string Name { get; set; }
        [MaxLength(250)]

        public string Description { get; set; }
      
        public ICollection<Product> Products { get; set; }

    }
}
