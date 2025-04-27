using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
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
      

        builder.Entity<Order>().HasData(
            new Order {
            Id = 1,
            UserId = 1, // Use real user Id
            OrderDate = new DateTime(2024, 04, 22, 12, 00, 00),
            TotalAmount = 150.75m
        },
            new Order
            {
                Id = 2,
                UserId = 1, // Use real user Id
                OrderDate = new DateTime(2024, 04, 22, 12, 00, 00),
                TotalAmount = 150.75m
            }, new Order
            {
                Id = 3,
                UserId = 2, // Use real user Id
                OrderDate = new DateTime(2024, 04, 22, 12, 00, 00),
                TotalAmount = 150.75m
            }, new Order
            {
                Id = 4,
                UserId = 2, // Use real user Id
                OrderDate =new DateTime(2024, 04, 22, 12, 00, 00),
                TotalAmount = 150.75m
            }, new Order
            {
                Id = 5,
                UserId = 2, // Use real user Id
                OrderDate =new DateTime(2024,04,22,12,00,00),
                TotalAmount = 150.75m
            });

        // Seeding example for OrderItem
        builder.Entity<OrderItem>().HasData(new OrderItem
        {
            Id = 1,
            OrderId = 1,
            ProductId = 1,
            Quantity = 2,
            UnitPrice = 50.25m
        }, new OrderItem
        {
            Id = 2,
            OrderId = 1,
            ProductId = 1,
            Quantity = 2,
            UnitPrice = 50.25m
        }, new OrderItem
        {
            Id = 3,
            OrderId = 1,
            ProductId = 1,
            Quantity = 2,
            UnitPrice = 50.25m
        }, new OrderItem
        {
            Id = 4,
            OrderId = 1,
            ProductId = 1,
            Quantity = 2,
            UnitPrice = 50.25m
        });

        // Seeding example for Cart
        builder.Entity<Cart>().HasData(new Cart
        {
            Id = 1,
            UserId = 1, // Use real user Id
            ProductId = 2,
            Quantity = 3
        });
    }

}
