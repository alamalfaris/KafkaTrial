using KafkaProducer.Api.Interfaces;
using KafkaProducer.Api.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KafkaProducer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<ActionResult> SendOrderRequest ([FromBody] OrderRequest orderRequest)
        {
            return Ok(await _orderService.SendOrderRequest(orderRequest));
        }
    }
}
