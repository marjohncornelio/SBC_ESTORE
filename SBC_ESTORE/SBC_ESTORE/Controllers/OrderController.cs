using Microsoft.AspNetCore.Mvc;
using SBC_ESTORE.Services.OrderServices;
using SBC_ESTORE.Shared.DTO.Order;
using System.Net;

namespace SBC_ESTORE.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService orderService;

        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        [HttpPost]
        public async Task<ActionResult> AddOrderFromCartController(OrderDTO order)
        {
            var response = await orderService.AddOrderFromCart(order);

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


        [HttpGet("{id}")]
        public async Task<ActionResult<List<OrderDTO>>> GetAllUserOrderController(int id)
        {
            var response = await orderService.GetAllUserOrder(id);

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


        //Admin
        [HttpGet("admin")]
        public async Task<ActionResult<List<OrderDTO>>> GetAllCustomerOrdersController()
        {
            var response = await orderService.GetAllCustomerOrder();

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
