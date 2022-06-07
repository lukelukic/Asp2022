using AspNedelja3Vezbe.Api.Payment;
using AspNedelja3Vezbe.DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ASPNedelja3Vezbe.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        OrderProcessor orderProcessor;

        public OrdersController(OrderProcessor processor)
        {
            orderProcessor = processor;
        }

        [HttpPost]
        public void Post([FromBody] Order o)
        {
            orderProcessor.ProcessOrder(o);
        }
    }
}
