using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SBC_ESTORE.Models;
using SBC_ESTORE.Services.CartServices;
using SBC_ESTORE.Services.CategoryServices;
using SBC_ESTORE.Shared.DTO.Cart;
using System.Net;

namespace SBC_ESTORE.Controllers
{
    [Route("api/cart")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService cartService;

        public CartController(ICartService cartService)
        {
            this.cartService = cartService;
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult> GetItemsFromCart(int Id)
         {
            var response = await cartService.GetAllItemsFromCart(Id);

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

        [HttpPost("addtocart")]
        public async Task<ActionResult> AddItemToCart(CartItemDTO cartItem)
        {
            var response = await cartService.AddCartItemToCart(cartItem);

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

        [HttpPost("update-cart")]
        public async Task<ActionResult> UpdateCartItems(List<CartItemDTO> cartItems)
        {
            var response = await cartService.UpdateCartItems(cartItems);

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
    }
}
