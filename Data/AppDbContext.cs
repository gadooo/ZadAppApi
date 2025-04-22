using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ZadGroceryAppApi.Model;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Cart> CartItems { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }

    //protected override void OnModelCreating(ModelBuilder builder)
    //{
    //    base.OnModelCreating(builder);

    //    // Configure relationships if needed (e.g. cascading deletes, composite keys)
    //}

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Seed Categories
        builder.Entity<Category>().HasData(
     new Category { Id = 1, Name = "Fruits", Description = "Fresh and seasonal fruits" },
     new Category { Id = 2, Name = "Vegetables", Description = "Green and healthy vegetables" },
     new Category { Id = 3, Name = "Dairy", Description = "Milk, cheese, and more" },
     new Category { Id = 4, Name = "Meat", Description = "sheep,caow and chiken " }

 );


        // Seed Products
        builder.Entity<Product>().HasData(
            new Product
            {
                Id = 1,
                Name = "Banana",
                Description = "Fresh organic bananas",
                Price = 3.99m,
                Stock = 50,
                CategoryId = 1,
                ImageUrl = "images/banana.jpg"
            },
            new Product
            {
                Id = 2,
                Name = "Milk",
                Description = "Full cream milk",
                Price = 2.50m,
                Stock = 30,
                CategoryId = 3,
                ImageUrl = "images/milk.jpg"
            }
        );
    }

}
