using Microsoft.EntityFrameworkCore;

namespace MicroservicesApp.Product.API.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {


        }
        public DbSet<Models.Product>Products { get; set; }
    }
}
