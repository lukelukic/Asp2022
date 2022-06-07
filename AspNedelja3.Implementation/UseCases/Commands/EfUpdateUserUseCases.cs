using AspNedelja3.Implementation.Validators;
using ASPNedelja3.Application.UseCases.Commands;
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
    public class EfUpdateUserUseCases : EfUseCase, IUpdateUserUseCasesCommand
    {
        private readonly UpdateUserUseCasesValidator _validator;
        public EfUpdateUserUseCases(
            VezbeDbContext context, 
            UpdateUserUseCasesValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 9;

        public string Name => "Update user actions";

        public string Description => "";

        public void Execute(UpdateUserUseCasesDto request)
        {
            _validator.ValidateAndThrow(request);

            var userUseCases = Context.UserUseCases
                                      .Where(x => x.UserId == request.UserId)
                                      .ToList();

            Context.UserUseCases.RemoveRange(userUseCases);

            var useCasesToAdd = request.UseCaseIds.Select(x => new UserUseCase
            {
                UseCaseId = x,
                UserId = request.UserId.Value
            });

            Context.AddRange(useCasesToAdd);

            Context.SaveChanges();
        }
    }
}
