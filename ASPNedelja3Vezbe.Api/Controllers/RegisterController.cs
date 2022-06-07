using AspNedelja3.Implementation;
using ASPNedelja3.Application.Logging;
using ASPNedelja3.Application.UseCases.Commands;
using ASPNedelja3.Application.UseCases.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASPNedelja3Vezbe.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly UseCaseHandler _handler;
        private readonly IRegisterUserCommand _command;

        public RegisterController(
            UseCaseHandler handler, 
            IRegisterUserCommand command)
        {
            _handler = handler;
            _command = command;
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Post([FromBody] RegisterDto dto)
        {
            _handler.HandleCommand(_command, dto);

            return StatusCode(StatusCodes.Status201Created);
        }
    }
}
