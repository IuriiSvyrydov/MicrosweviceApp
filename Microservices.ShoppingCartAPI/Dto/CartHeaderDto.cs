
namespace Microservices.ShoppingCartAPI.Dto
{
    public class CartHeaderDto
    {
        public int CartHeaderId { get; set; }

        public int UserId { get; set; }
        public string CouponCode { get; set; }
    }
}
