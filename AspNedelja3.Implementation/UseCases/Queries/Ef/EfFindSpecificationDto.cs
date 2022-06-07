using ASPNedelja3.Application.Exceptions;
using ASPNedelja3.Application.UseCases.DTO;
using ASPNedelja3.Application.UseCases.Queries;
using AspNedelja3Vezbe.DataAccess;
using ASPNedelja3Vezbe.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNedelja3.Implementation.UseCases.Queries.Ef
{
    public class EfFindSpecificationQuery : EfUseCase, IFindSpecificationQuery
    {
        public EfFindSpecificationQuery(VezbeDbContext context) : base(context)
        {
        }

        public int Id => 6;

        public string Name => "Ef Find Specification";

        public string Description => "";

        public SpecificationDto Execute(int search)
        {
            var spec = Context.Specifications
                        .Include(x => x.SpecificationValues)
                        .FirstOrDefault(x => x.Id == search && x.IsActive);

            if(spec == null)
            {
                throw new EntityNotFoundException(nameof(Specification), search);
            }

            return new SpecificationDto
            {
                Id = spec.Id,
                Name = spec.Name,
                SpecificationValues = spec.SpecificationValues.Select(x => new SpecificationValueDto
                {
                    Id = x.Id,
                    Value = x.Value
                })
            };
        }
    }
}
