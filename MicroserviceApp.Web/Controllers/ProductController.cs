using MicroserviceApp.Web.Services.IServices;
using MicroservicesApp.Product.API.Dtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MicroserviceApp.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> ProductIndex()
        {
            List<ProductDto> list = new();
            var response = await _productService.GetAllProductsAsync<ResponseDto>();
            if (response!= null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(response.Result));
            }
            return View(list);
        }

        public  async Task<IActionResult> CreateProduct()
        {
            
            return View();
        }

        [HttpPost]
        
        public async Task<IActionResult> CreateProduct(Models.ProductDto model)
        {
            if (ModelState.IsValid)
            {
                var response = await _productService.CreateProductAsync<ResponseDto>(model);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(ProductIndex));
                }
            }

            return View(model);
        }
        public async Task<IActionResult> EditProduct(int id)
        {
            var response = await _productService.GetProductByIdAsync<ResponseDto>(id);
            if (response is not null && response.IsSuccess)
            {
                ProductDto model = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
                return View(model);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> EditProduct(Models.ProductDto model)
        {
            if (ModelState.IsValid)
            {
                var response = await _productService.UpdateProductAsync<ResponseDto>(model);
                if (response != null && response.IsSuccess)
                {
                  
                    return RedirectToAction(nameof(ProductIndex));
                }
            }

            return View(model);
        }
        [HttpPost]
        [Route("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var response = await _productService.GetProductByIdAsync<ResponseDto>(id);
            if (response is  not null && response.IsSuccess)
            {
                ProductDto model = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
                return View(model);
            }

            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteProduct(Models.ProductDto model)
        {
            if (ModelState.IsValid)
            {
                var response = await _productService.DeleteProductAsync<ResponseDto>(model.Id);
                if (response != null && response.IsSuccess)
                {

                    return RedirectToAction(nameof(ProductIndex));
                }
            }

            return View(model);
        }

    }
}
