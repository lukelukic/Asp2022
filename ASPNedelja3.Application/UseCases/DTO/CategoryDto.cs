using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNedelja3.Application.UseCases.DTO
{
    public class CategoryDto : BaseDto
    {
        public string Name { get; set; }
    }

    public class CreateCategoryDto
    {
        public string ImageFileName { get; set; }
        public string Name { get; set; }
        public int? ParentCategoryId { get; set; }
    }
}
