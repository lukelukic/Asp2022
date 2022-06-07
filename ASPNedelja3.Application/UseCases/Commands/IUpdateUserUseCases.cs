using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNedelja3.Application.UseCases.Commands
{
    public interface IUpdateUserUseCasesCommand : ICommand<UpdateUserUseCasesDto>
    {
    }

    public class UpdateUserUseCasesDto
    {
        public int? UserId { get; set; }
        public IEnumerable<int> UseCaseIds { get; set; }
    }
}
