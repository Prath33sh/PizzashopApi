using Microsoft.EntityFrameworkCore;
using Pizzashop.Data.Entities;

namespace Pizzashop.Data
{
    public class PizzaShopContext : DbContext
    {
        public PizzaShopContext(DbContextOptions<PizzaShopContext> options) : base(options){}

        public required DbSet<Order> Orders { get; set;}
    }
}