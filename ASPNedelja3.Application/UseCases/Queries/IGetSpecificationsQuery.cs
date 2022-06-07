using ASPNedelja3.Application.UseCases.DTO;
using ASPNedelja3.Application.UseCases.DTO.Searches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNedelja3.Application.UseCases.Queries
{
    public interface IGetSpecificationsQuery : IQuery<BaseSearch, IEnumerable<SpecificationDto>>
    {
    }
}
