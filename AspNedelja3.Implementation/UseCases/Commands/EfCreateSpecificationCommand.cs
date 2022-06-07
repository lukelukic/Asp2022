using AspNedelja3.Implementation.Validators;
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
    public class EfCreateSpecificationCommand : EfUseCase, ICreateSpecificationCommand
    {
        private readonly CreateSpecificationValidator _validator;
        public EfCreateSpecificationCommand(
            VezbeDbContext context, 
            CreateSpecificationValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 7;

        public string Name => "Ef Create Command";

        public string Description => "";

        public void Execute(CreateSpecificationDto request)
        {
            _validator.ValidateAndThrow(request);

            var spec = new Specification
            {
                Name = request.Name
            };

            var values = request.Values.Select(x => new SpecificationValue
            {
                Value = x,
                Specification = spec
            }).ToList();

            Context.Specifications.Add(spec);

            Context.SaveChanges();
        }
    }
}
