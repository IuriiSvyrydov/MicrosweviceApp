using MicroservicesApp.Product.API.Dtos;

namespace MicroservicesApp.Product.API.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductDto>> GetProducts();
        Task<ProductDto> GetById(int id);
        Task<ProductDto> CreateUpdate(ProductDto productDto);
        Task<bool> DeleteProduct(int id);
    }
}
