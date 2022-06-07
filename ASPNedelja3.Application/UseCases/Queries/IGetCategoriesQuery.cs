using ASPNedelja3.Application.UseCases.DTO;
using ASPNedelja3.Application.UseCases.DTO.Searches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNedelja3.Application.UseCases.Queries
{
    public interface IGetCategoriesQuery : IQuery<BasePagedSearch, PagedResponse<CategoryDto>>
    {
    }

    //41 ->

    // /api/categories?perPage=10&page=2

    /* Sadrzaj http response gde se nalazi paginacija
        PagesCount -> int
        Data -> IEnumerable<OrderDto>
        CurrentPage -> int
        ItemsPerPage -> int
        TotalCount -> int
    */
}
