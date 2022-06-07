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
    public class RegisterUserValidator : AbstractValidator<RegisterDto>
    {
        public RegisterUserValidator(VezbeDbContext _context)
        {
            RuleFor(x => x.Email)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Email je obavezan podatak.")
                .EmailAddress().WithMessage("Email nije ispravnog formata.")
                .Must(x => !_context.Users.Any(u => u.Email == x)).WithMessage("Email adresa {PropertyValue} je već u upotrebi.");

            RuleFor(x => x.Username)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Korisničko ime je obavezan podatak.")
                .MinimumLength(3).WithMessage("Minimalan broj karaktera je 3.")
                .MaximumLength(12).WithMessage("Maksimalan broj karaktera je 12.")
                .Matches("^(?=[a-zA-Z0-9._]{3,12}$)(?!.*[_.]{2})[^_.].*[^_.]$")
                .WithMessage("Korisničko ime nije ispravnog formata.")
                .Must(x => !_context.Users.Any(u => u.Username == x)).WithMessage("Korisničko ime {PropertyValue} je već u upotrebi.");

            var imePrezimeRegex = @"^[A-Z][a-z]{2,}(\s[A-Z][a-z]{2,})?$";
            RuleFor(x => x.FirstName).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Ime je obavezan podatak.")
                .Matches(imePrezimeRegex).WithMessage("Ime nije u ispravnom formatu.");

            RuleFor(x => x.LastName).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Prezime je obavezan podatak.")
                .Matches(imePrezimeRegex).WithMessage("Prezime nije u ispravnom formatu.");

            RuleFor(x => x.Password).NotEmpty().WithMessage("Lozinka je obavezan podatak.")
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$").WithMessage("Lozinka mora da sadrži minimalno 8 karaktera, jedno veliko, jedno malo slovo, broj i specijalni karakter.");

        }
    }
}
