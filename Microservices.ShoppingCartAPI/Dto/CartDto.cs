using Microservices.ShoppingCartAPI.Dto;
using Microservices.ShoppingCartAPI.Models;

namespace Microservices.ShoppingCartAPI.Dto
{
    public class CartDto
    {
        public CartHeader CartHeader { get; set; }
        public IEnumerable<CartDetails>CartDetails { get; set; }
    }
}
