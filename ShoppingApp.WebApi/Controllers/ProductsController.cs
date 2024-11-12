using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingApp.Business.Operations.Product.Dtos;
using ShoppingApp.WebApi.Filters;
using ShoppingApp.WebApi.Models;

namespace ShoppingApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase 
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;

        }

        [HttpGet ("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _productService.GetProducts(id);

            if (product is null) 
                return NotFound();
                else
                    return Ok(product);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productService.GetAllProducts();
            return Ok(products);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddProduct(AddProductRequest request)
        {

            var addProductDto = new AddProductDto {
               
                ProductName = request.ProductName,
                Price = request.Price,
                StockQuantity = request.StockQuantity,
            };

           var result =  await _productService.AddProduct(addProductDto);

            if (!result.IsSucceed)
            {
                return BadRequest(result.Message);
            }
            else
            {
                return Ok();
            }

          
        }

        [HttpPatch("{id}")]
        [TimeControlFilter]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> AdjustProductPrice(int id, int changeTo)
        {
            var result = await _productService.AdjustProductPrice(id, changeTo);

            if (!result.IsSucceed)
                return NotFound(result.Message);
            else
                return Ok(result.Message);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result = await _productService.DeleteProduct(id);

            if (!result.IsSucceed)
                return NotFound(result.Message);
            else
                return Ok();
        }


        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]


        public async Task<IActionResult> UpdateProduct(int id, UpdateProductRequest request)
        {
            var updateProductDto = new UpdateProductDto
            {
                Id = id,
                ProductName = request.ProductName,
                Price = request.Price,
                StockQuantity = request.StockQuantity,
            };

            var result = await _productService.UpdateProduct(updateProductDto);

            if (!result.IsSucceed)
            {  return NotFound(result.Message); }
            else
            {
                return await GetProduct(id);
            }
        }

        

    }
}
