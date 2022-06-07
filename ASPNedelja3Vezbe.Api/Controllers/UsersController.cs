using AspNedelja3.Implementation;
using ASPNedelja3.Application.UseCases.Commands;
using ASPNedelja3Vezbe.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASPNedelja3Vezbe.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserUseCasesController : ControllerBase
    {
        private readonly UseCaseHandler _handler;

        public UserUseCasesController(UseCaseHandler handler)
        {
            _handler = handler;
        }

        [HttpPut]
        public IActionResult Put(
            [FromBody] UpdateUserUseCasesDto request,
            [FromServices] IUpdateUserUseCasesCommand command)
        {
            _handler.HandleCommand(command, request);
            return NoContent();
        }
    }
}
