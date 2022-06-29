using System.ComponentModel.DataAnnotations;

namespace MicroservicesApp.Product.API.Models
{
    public class Product
    {
#pragma warning disable CS8618
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
        public string ImageUrl { get; set; }
        [Required]
        public decimal Price { get; set; }
    }
}
