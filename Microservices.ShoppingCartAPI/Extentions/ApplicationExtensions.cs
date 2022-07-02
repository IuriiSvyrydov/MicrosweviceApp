using Microservices.ShoppingCartAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace Microservices.ShoppingCartAPI.Extentions
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddDbConfiguration(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(opt => opt
                .UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            return services;
        }
    }
}
