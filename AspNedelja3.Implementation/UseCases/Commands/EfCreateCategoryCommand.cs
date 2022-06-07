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
    public class EfCreateCategoryCommand : EfUseCase, ICreateCategoryCommand
    {
        private CreateCategoryValidator _validator;
        public EfCreateCategoryCommand(
            VezbeDbContext context, 
            CreateCategoryValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 3;

        public string Name => "Create Category (EF)";

        public string Description => "Create category using entity framework.";

        public void Execute(CreateCategoryDto request)
        {
            _validator.ValidateAndThrow(request);

            var category = new Category
            {
                Name = request.Name,
                ParentId = request.ParentCategoryId
            };

            if(!string.IsNullOrEmpty(request.ImageFileName))
            {
                var image = new Image
                {
                    Path = request.ImageFileName
                };
                category.Image = image;
            }

            Context.Categories.Add(category);

            Context.SaveChanges();
        }
    }
}
