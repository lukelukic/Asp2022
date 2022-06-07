using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNedelja3.Application.UseCases.DTO
{
    public class SpecificationDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<SpecificationValueDto> SpecificationValues { get; set; }
    }

    public class SpecificationValueDto
    {
        public int Id { get; set; }
        public string Value { get; set; }
    }

    public class CreateSpecificationDto
    {
        public string Name { get; set; }
        public IEnumerable<string> Values { get; set; }
    }
}
