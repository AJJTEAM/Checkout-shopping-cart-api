using CheckoutShopping.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace CheckoutShopping.Infrastructure
{
    public class CheckoutContext:DbContext
    {
        public CheckoutContext(DbContextOptions<CheckoutContext> options)
            : base(options)
        {

        }
        public DbSet<ShoppingItem> ShoppingItems { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
