using System.ComponentModel.DataAnnotations;

namespace MicroserviceApp.Web.Models
{
    public class ProductDto
    {
#pragma warning disable
        public int Id { get; set; }
        
        public string Name { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
        public string ImageUrl { get; set; }
        
        public decimal Price { get; set; }
        [Range(1,100)]
        public int Count { get; set; } = 1;
    }
}
