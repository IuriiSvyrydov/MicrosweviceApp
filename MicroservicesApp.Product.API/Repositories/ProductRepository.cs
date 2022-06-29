using AutoMapper;
using MicroservicesApp.Product.API.Data;
using MicroservicesApp.Product.API.Dtos;
using Microsoft.EntityFrameworkCore;

namespace MicroservicesApp.Product.API.Repositories
{
    public class ProductRepository: IProductRepository
    {
        private readonly ApplicationDbContext _context;
        private  IMapper _mapper;

        public ProductRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDto>> GetProducts()
        {
            var products = await _context.Products.ToListAsync();
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task<ProductDto> GetById(int id)
        {
            var product = await _context.Products.Where(x => x.Id == id).FirstOrDefaultAsync();
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<ProductDto> CreateUpdate(ProductDto productDto)
        {
            var product = _mapper.Map<ProductDto, Models.Product>(productDto);
            if (product.Id>0)
            {
                _context.Products.Update(product);
            }
            else
            {
                _context.Products.Add(product);
            }

            await _context.SaveChangesAsync();
            return _mapper.Map<Models.Product, ProductDto>(product);
        }

        public async Task<bool> DeleteProduct(int id)
        {
            try
            {
                var productToDelete = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
                if (productToDelete !=null)
                    _context.Products.Remove(productToDelete);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
