using AspNedelja3.Implementation.Validators;
using ASPNedelja3.Application.UseCases.DTO;
using AspNedelja3Vezbe.DataAccess;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AspNedelja3Vezbe.Tests.Validation
{
    public class CreateCategoryValidatorTests
    {
        [Fact]
        public void ReturnsError_WhenNameIsNotProvided()
        {
            var validator = new CreateCategoryValidator(Context);

            var dto = new CreateCategoryDto
            {
                Name = ""
            };

            var result = validator.Validate(dto);

            result.IsValid.Should().BeFalse();

            result.Errors.Where(x => x.PropertyName == "Name").Should().HaveCount(1);

            result.Errors.Where(x => x.PropertyName == "Name")
                .First().ErrorMessage.Should()
                .Be("Naziv je obavezan podatak.");
        }

        [Fact]
        public void ReturnsError_WhenNameHasLessThen3Characters()
        {
            var validator = new CreateCategoryValidator(Context);

            var dto = new CreateCategoryDto
            {
                Name = "A"
            };

            var result = validator.Validate(dto);

            result.IsValid.Should().BeFalse();

            result.Errors.Where(x => x.PropertyName == "Name").Should().HaveCount(1);

            result.Errors.Where(x => x.PropertyName == "Name")
                .First().ErrorMessage.Should()
                .Be("Minimalan broj karaktera je 3.");
        }

        private VezbeDbContext Context
        {
            get
            {
                var optionsBuilder = new DbContextOptionsBuilder();

                var conString = "Data Source = localhost; Initial Catalog = AspVezbe_2; Integrated Security = True";

                optionsBuilder.UseSqlServer(conString).UseLazyLoadingProxies();

                var options = optionsBuilder.Options;

                return new VezbeDbContext(options);
            }
        }
    }
}
