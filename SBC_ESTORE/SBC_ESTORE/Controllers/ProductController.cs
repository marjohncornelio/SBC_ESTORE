using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SBC_ESTORE.Models;
using SBC_ESTORE.Services.CategoryServices;
using SBC_ESTORE.Services.ProductServices;
using SBC_ESTORE.Shared.DTO.Product;
using System.Net;

namespace SBC_ESTORE.Controllers
{
    [Route("api/admin/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpPost]
        public async Task<ActionResult> AddProductcontroller(ProductDTO product)
        {
            var response = await productService.AddProduct(product);

            switch (response.ResponseCode)
            {
                case HttpStatusCode.OK:
                    return Ok(response.Message);
                case HttpStatusCode.NotFound:
                    return NotFound(response.Message);
                case HttpStatusCode.BadRequest:
                    return BadRequest(response.Message);
                default:
                    return BadRequest("Error Occured, Try Again Later");
            }
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult> UpdateProductcontroller(ProductDTO product, int Id)
        {
            var response = await productService.UpdateProduct(product, Id);

            switch (response.ResponseCode)
            {
                case HttpStatusCode.OK:
                    return Ok(response.Message);
                case HttpStatusCode.NotFound:
                    return NotFound(response.Message);
                case HttpStatusCode.BadRequest:
                    return BadRequest(response.Message);
                default:
                    return BadRequest("Error Occured, Try Again Later");
            }
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteProductcontroller(int Id)
        {
            var response = await productService.DeleteProduct(Id);

            switch (response.ResponseCode)
            {
                case HttpStatusCode.OK:
                    return Ok(response.Message);
                case HttpStatusCode.NotFound:
                    return NotFound(response.Message);
                case HttpStatusCode.BadRequest:
                    return BadRequest(response.Message);
                default:
                    return BadRequest("Error Occured, Try Again Later");
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<ProductDTO>> GetProductByIdController(int Id)
        {
            var response = await productService.GetProductById(Id);

            switch (response.ResponseCode)
            {
                case HttpStatusCode.OK:
                    return Ok(response.Data);
                case HttpStatusCode.NotFound:
                    return NotFound(response.Message);
                case HttpStatusCode.BadRequest:
                    return BadRequest(response.Message);
                default:
                    return BadRequest("Error Occured, Try Again Later");
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductDTO>>> GetAllProductController()
        {
            var response = await productService.GetAllProducts();

            switch (response.ResponseCode)
            {
                case HttpStatusCode.OK:
                    return Ok(response.Data);
                case HttpStatusCode.NotFound:
                    return NotFound(response.Message);
                case HttpStatusCode.BadRequest:
                    return BadRequest(response.Message);
                default:
                    return BadRequest("Error Occured, Try Again Later");
            }
        }
    }
}
