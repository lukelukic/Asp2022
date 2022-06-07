using ASPNedelja3.Application.UseCases.DTO;
using AspNedelja3Vezbe.DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNedelja3.Implementation.Validators
{
    public class CreateCategoryValidator : AbstractValidator<CreateCategoryDto>
    {
        private VezbeDbContext _context;
        public CreateCategoryValidator(VezbeDbContext context)
        {
            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Naziv je obavezan podatak.")
                .MinimumLength(3).WithMessage("Minimalan broj karaktera je 3.")
                .Must(CategoryNotInUse).WithMessage("Naziv {PropertyValue} je već u upotrebi.");
            _context = context;
            //Func<string,bool>


            RuleFor(x => x.ParentCategoryId)
                .Must(x => context.Categories.Any(c => c.Id == x && c.IsActive))
                .When(dto => dto.ParentCategoryId.HasValue).WithMessage("Prosleđena roditeljska kategorija ne postoji u sistemu.");
                
        }

        private bool CategoryNotInUse(string name)
        {
            var exists = _context.Categories.Any(x => x.Name == name);

            return !exists;
        }
    }
}
