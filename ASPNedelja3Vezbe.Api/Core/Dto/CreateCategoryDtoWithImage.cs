using ASPNedelja3.Application.UseCases.DTO;
using Microsoft.AspNetCore.Http;

namespace ASPNedelja3Vezbe.Api.Core.Dto
{
    public class CreateCategoryDtoWithImage : CreateCategoryDto
    {
        public IFormFile Image { get; set; }
    }
}
