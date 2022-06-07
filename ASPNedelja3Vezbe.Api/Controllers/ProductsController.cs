using AspNedelja3Vezbe.DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ASPNedelja3Vezbe.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        // GET: api/<ProductsController>
        [HttpGet]
        public IActionResult Get([FromServices] VezbeDbContext context, [FromQuery] string keyword)
        {
            var productsQuery = context.Products.AsQueryable();

            if (keyword != null)
            {
                productsQuery = productsQuery.Where(x => x.Name.Contains(keyword));
            }

            return Ok(productsQuery.Select(x => new
            {
                x.Name,
                x.Description,
                x.Id,
                x.Price,
                Images = x.Images.Select(x => x.Path)
            }).ToList());
        } 
    }
}

   
