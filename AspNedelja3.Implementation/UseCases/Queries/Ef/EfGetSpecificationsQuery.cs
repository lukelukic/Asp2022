using ASPNedelja3.Application.UseCases.DTO;
using ASPNedelja3.Application.UseCases.DTO.Searches;
using ASPNedelja3.Application.UseCases.Queries;
using AspNedelja3Vezbe.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNedelja3.Implementation.UseCases.Queries.Ef
{
    public class EfGetSpecificationsQuery : EfUseCase, IGetSpecificationsQuery
    {
        public EfGetSpecificationsQuery(VezbeDbContext context) : base(context)
        {
        }

        public int Id => 5;

        public string Name => "EF Search Specifications";

        public string Description => "";

        public IEnumerable<SpecificationDto> Execute(BaseSearch search)
        {
            var query = Context.Specifications.Where(x => x.IsActive);

            var kw = search.Keyword;

            if (!string.IsNullOrEmpty(kw))
            {
                query = query.Where(x => x.Name.Contains(kw) || x.SpecificationValues.Any(sv => sv.Value.Contains(kw)));
            }

            return query.Select(x => new SpecificationDto
            {
                Id = x.Id,
                Name = x.Name,
                SpecificationValues = x.SpecificationValues.Where(x => x.IsActive).Select(sv => new SpecificationValueDto
                {
                    Id = sv.Id,
                    Value = sv.Value
                })
            }).ToList();
        }
    }
}
