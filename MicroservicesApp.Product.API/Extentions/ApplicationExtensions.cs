using MicroservicesApp.Product.API.Data;
using Microsoft.EntityFrameworkCore;

namespace MicroservicesApp.Product.API.Extentions
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
