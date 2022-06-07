using ASPNedelja3Vezbe.Domain;
using Bogus;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Faker;
using AspNedelja3Vezbe.DataAccess;
using System.Diagnostics;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ASPNedelja3Vezbe.Api.Controllers
{
    [Route("api/[controller]")]    //nassajt.com/api/resurs
    [ApiController]
    public class TestController : ControllerBase
    {
        // GET: http://localhost:5000/api/test
        [HttpGet]
        public IActionResult Get([FromServices] VezbeDbContext context)
        {
            var categories = new List<Category>();

            for (int i = 0; i < 50; i++)
            {
                categories.Add(new Category { Name = "Parent " + i + 1 });
            }

            var categoriesFaker = new Faker<Category>()
                                  .RuleFor(x => x.Name, x => x.Commerce.Categories(1).First())
                                  .RuleFor(x => x.ParentCategory, x => x.PickRandom(categories));

            var fakedCategories = categoriesFaker.Generate(100);

            categories.AddRange(fakedCategories);

            var words = Lorem.Words(1000);

            var imageFaker = new Faker<Image>()
                                  .RuleFor(x => x.Path, x => x.Image.PicsumUrl());



            var productsFaker = new Faker<Product>()
                                .RuleFor(x => x.Name, x => x.Lorem.Word())
                                .RuleFor(x => x.Price, x => x.Random.Decimal(1, 10000))
                                .RuleFor(x => x.Description, x => x.Lorem.Sentence())
                                .RuleFor(x => x.Category, x => x.PickRandom(categories));
                                //.RuleFor(x => x.Images, x => imageFaker.Generate(3));

            var stopWatch = new Stopwatch();
            stopWatch.Start();

            var products = productsFaker.Generate(100);

            stopWatch.Stop();
            Console.WriteLine(stopWatch.ElapsedMilliseconds);

            context.Categories.AddRange(categories);
            context.Products.AddRange(products);

            context.SaveChanges();

            return Ok(new { msg = "OK" });
        }
    }
}
