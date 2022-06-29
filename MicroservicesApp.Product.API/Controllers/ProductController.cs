using MicroservicesApp.Product.API.Dtos;
using MicroservicesApp.Product.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace MicroservicesApp.Product.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private  readonly ResponseDto _responseDto;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
            _responseDto = new ResponseDto();
        }

        [HttpGet]
        
        public async Task<object> GetAllProducts()
        {
            try
            {
                IEnumerable<ProductDto> products = await _productRepository.GetProducts();
                _responseDto.Result = products;
            }
            catch (Exception e)
            {
                _responseDto.IsSuccess = false;
                _responseDto.ErrorMessages = new List<string>
                {
                    e.ToString()
                };
            }
            return _responseDto;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            var product = await _productRepository.GetById(id);
            if (product is null)
                return NotFound();
            return Ok(product);

        }

        [HttpPost]
        public async Task<object> Create ([FromBody]ProductDto productDto)
        {
            try
            {
                var product = await _productRepository.CreateUpdate(productDto);
                _responseDto.Result = product;
            }
            catch (Exception e)
            {
                _responseDto.IsSuccess = false;
                _responseDto.ErrorMessages = new List<string>
                {
                    e.ToString()
                };
            }

            return _responseDto;
        }
        [HttpPut]
        public async Task<object> Put([FromBody] ProductDto productDto)
        {
            try
            {
                var product = await _productRepository.CreateUpdate(productDto);
                _responseDto.Result = product;
            }
            catch (Exception e)
            {
                _responseDto.IsSuccess = false;
                _responseDto.ErrorMessages = new List<string>
                {
                    e.ToString()
                };
            }
           
            return _responseDto;
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<object> Delete(int id)
        {
            try
            {
                var productToDelete = await _productRepository.DeleteProduct(id);
                _responseDto.Result = productToDelete;
            }
            catch (Exception e)
            {
                _responseDto.IsSuccess = false;
                _responseDto.ErrorMessages = new List<string>
                {
                    e.ToString()
                };
            }

            return _responseDto;
        }

    }
}
