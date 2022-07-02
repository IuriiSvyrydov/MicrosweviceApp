
namespace Microservices.ShoppingCartAPI.Dto
{
    public class ProductDto
    {
#pragma warning disable CS8618
        public int Id { get; set; }
        
        public string Name { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
        public string ImageUrl { get; set; }
        
        public decimal Price { get; set; }
    }
}
