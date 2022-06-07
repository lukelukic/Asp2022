using ASPNedelja3.Application.Exceptions;
using ASPNedelja3.Application.UseCases.Commands;
using AspNedelja3Vezbe.DataAccess;
using ASPNedelja3Vezbe.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNedelja3.Implementation.UseCases.Commands
{
    public class EfDeleteSpecificationCommand : EfUseCase, IDeleteSpecificationCommand
    {
        public EfDeleteSpecificationCommand(VezbeDbContext context) : base(context)
        {
        }

        public int Id => 8;

        public string Name => "Delete specification";

        public string Description => "";

        public void Execute(int request)
        {
            var spec = Context.Specifications
                        .Include(x => x.SpecificationValues)
                        .FirstOrDefault(x => x.Id == request && x.IsActive);

            if (spec == null)
            {
                throw new EntityNotFoundException(nameof(Specification), request);
            }

            if (spec.CategorySpecifications.Any())
            {
                throw new UseCaseConflictException("Can't delete speficiation because of it's link to categories: " 
                                                   + string.Join(", ", spec.CategorySpecifications.Select(x => x.Category.Name)));
            }

            Context.SpecificationValues.RemoveRange(spec.SpecificationValues);
            Context.Specifications.Remove(spec);

            Context.SaveChanges();
        }
    }
}
