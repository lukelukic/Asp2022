using AspNedelja3.Implementation.Validators;
using ASPNedelja3.Application.Emails;
using ASPNedelja3.Application.UseCases.Commands;
using ASPNedelja3.Application.UseCases.DTO;
using AspNedelja3Vezbe.DataAccess;
using ASPNedelja3Vezbe.Domain;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNedelja3.Implementation.UseCases.Commands
{
    public class EfRegisterUserCommand : EfUseCase, IRegisterUserCommand
    {
        private readonly RegisterUserValidator _validator;
        private readonly IEmailSender _sender;

        public EfRegisterUserCommand(VezbeDbContext context, RegisterUserValidator validator, IEmailSender sender) : base(context)
        {
            _validator = validator;
            _sender = sender;
        }

        public void Execute(RegisterDto request)
        {
            _validator.ValidateAndThrow(request);

            var hash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var user = new User
            {
                Username = request.Username,
                Email = request.Email,
                Password = hash,
                FirstName = request.FirstName,
                LastName = request.LastName
            };

            Context.Users.Add(user);
            Context.SaveChanges();

            //slanje email-a za verifikaciju

            _sender.Send(new MessageDto
            {
                To = request.Email,
                Title = "Successfull registration!",
                Body = "Dear " + request.Username + "\n Please activate your account...."
            });
        }

        public int Id => 4;

        public string Name => "User reigstration (Using EF)";

        public string Description => "";
    }
}
