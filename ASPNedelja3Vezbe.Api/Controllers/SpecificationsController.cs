using AspNedelja3.Implementation;
using ASPNedelja3.Application.Exceptions;
using ASPNedelja3.Application.Logging;
using ASPNedelja3.Application.UseCases.Commands;
using ASPNedelja3.Application.UseCases.DTO;
using ASPNedelja3.Application.UseCases.DTO.Searches;
using ASPNedelja3.Application.UseCases.Queries;
using AspNedelja3Vezbe.DataAccess;
using ASPNedelja3Vezbe.Api.Extensions;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ASPNedelja3Vezbe.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SpecificationsController : ControllerBase
    {
        private UseCaseHandler _handler;

        public SpecificationsController(UseCaseHandler handler)
        {
            _handler = handler;
        }

        // GET: api/<SpecificationsController>
        [HttpGet]
        public IActionResult Get([FromQuery] BaseSearch search, [FromServices] IGetSpecificationsQuery query)
        {
            return Ok(_handler.HandleQuery(query, search));
        }

        // GET api/<SpecificationsController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id, [FromServices] IFindSpecificationQuery query)
        {
            return Ok(_handler.HandleQuery(query, id));
        }

        // POST api/<SpecificationsController>
        [HttpPost]
        public IActionResult Post([FromBody] CreateSpecificationDto dto, [FromServices]ICreateSpecificationCommand command)
        {
            _handler.HandleCommand(command, dto);
            return NoContent();
        }

        // DELETE api/<SpecificationsController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteSpecificationCommand command)
        {
            _handler.HandleCommand(command, id);
            return NoContent();
        }
    }
}
