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
    public class CreateSpecificationValidator : AbstractValidator<CreateSpecificationDto>
    {
        public CreateSpecificationValidator(VezbeDbContext context)
        {
            RuleFor(x => x.Name).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Name is required.")
                .MinimumLength(3).WithMessage("Minimal number of characters is 3.")
                .Must(name => !context.Specifications.Any(s => s.Name == name && s.IsActive))
                    .WithMessage("Specification {PropertyValue} is already in use.");

            //null -> OK
            // [] -> OK
            // [ "S1" ] -> Nije dozvoljena samo jedna vrednost
            // [ "S1", "S2", "S3" ] -> OK
            // [ "S1", "S2", "S2" ]
            RuleFor(x => x.Values).Cascade(CascadeMode.Stop)
                .Must(x => x.Count() > 1)
                .WithMessage("Minimum number of values is 2.").When(x => x.Values != null && x.Values.Any())
                .Must(values =>
                {
                    if (values == null)
                    {
                        return true;
                    }

                    return values.Distinct().Count() == values.Count();
                }).WithMessage("Duplicate values are not allowed.").DependentRules(() =>
                {
                    RuleForEach(x => x.Values).NotEmpty().WithMessage("Value should not be empty.");
                });

            
        }
    }
}
