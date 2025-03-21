using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pizzashop.Models;
using Pizzashop.Services;

namespace Pizzashop.Controller
{
    [Route("api/Order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public IActionResult PlaceOrder(OrderRequest order)
        {
            return Ok(_orderService.PlaceOrder(order));
        }

//TODO: turn these into async
        [HttpPatch]
        public IActionResult UpdateOrder(string orderId)
        {
            return Ok(_orderService.CancelOrder(orderId));
        }
    }
}
