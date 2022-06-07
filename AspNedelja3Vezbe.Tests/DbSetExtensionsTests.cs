using AspNedelja3Vezbe.DataAccess;
using AspNedelja3Vezbe.DataAccess.Exceptions;
using AspNedelja3Vezbe.DataAccess.Extensions;
using ASPNedelja3Vezbe.Domain;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Xunit;

namespace AspNedelja3Vezbe.Tests
{
    //public class DbSetExtensionsTests
    //{
    //    [Fact]
    //    public void NonGenericDeactivate_ChangesIsActiveToFalse()
    //    {
    //        var category = new Category
    //        {
    //            Name = "Test 1",
    //            IsActive = true,
    //        };

    //        var context = new VezbeDbContext(new TestUser());

    //        context.Entry(category).State.Should().Be(EntityState.Detached);

    //        context.Deactivate(category);

    //        context.Entry(category).State.Should().Be(EntityState.Modified);
    //        category.IsActive.Should().BeFalse();
    //    }

    //    [Fact]
    //    public void GenericDeactivateThrows_WhenEntityDoesntExist()
    //    {
    //        var context = new VezbeDbContext(new TestUser());

    //        Action a = () => context.Deactivate<Category>(-500);

    //        a.Should().ThrowExactly<EntityNotFoundException>();
    //    }

    //    [Fact]
    //    public void GenericDeactivate_ChangesIsActiveToFalse()
    //    {
    //        var context = new VezbeDbContext(new TestUser());

    //        var category = context.Set<Category>().Find(1);

    //        category.IsActive.Should().BeTrue();

    //        context.Deactivate<Category>(1);

    //        context.Entry(category).State.Should().Be(EntityState.Modified);
    //        category.IsActive.Should().BeFalse();
    //    }
    //}

    public class TestUser : IApplicationUser
    {
        public string Identity => "Hardcoded User";

        public int Id => throw new NotImplementedException();

        public IEnumerable<int> UseCaseIds => throw new NotImplementedException();

        public string Email => throw new NotImplementedException();
    }
}
