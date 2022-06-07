using AspNedelja3.Implementation.UseCases.Commands;
using AspNedelja3.Implementation.UseCases.Queries.Ef;
using AspNedelja3.Implementation.UseCases.Queries.SP;
using AspNedelja3.Implementation.Validators;
using ASPNedelja3.Application.UseCases.Commands;
using ASPNedelja3.Application.UseCases.Queries;
using AspNedelja3Vezbe.DataAccess;
using ASPNedelja3Vezbe.Api.Core;
using ASPNedelja3Vezbe.Domain;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASPNedelja3Vezbe.Api.Extensions
{
    public static class ContainerExtensions
    {
        public static void AddJwt(this IServiceCollection services, AppSettings settings)
        {
            services.AddTransient(x =>
            {
                var context = x.GetService<VezbeDbContext>();
                var settings = x.GetService<AppSettings>();

                return new JwtManager(context, settings.JwtSettings);
            });


            services.AddAuthentication(options =>
            {
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = settings.JwtSettings.Issuer,
                    ValidateIssuer = true,
                    ValidAudience = "Any",
                    ValidateAudience = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.JwtSettings.SecretKey)),
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });
        }

        public static void AddUseCases(this IServiceCollection services)
        {
            services.AddTransient<IGetCategoriesQuery, EfGetCategoriesQuery>();
            services.AddTransient<IGetUseCaseLogsQuery, GetUseCaseLogsQuery>();
            services.AddTransient<ICreateCategoryCommand, EfCreateCategoryCommand>();
            services.AddTransient<IRegisterUserCommand, EfRegisterUserCommand>();
            services.AddTransient<IGetSpecificationsQuery, EfGetSpecificationsQuery>();
            services.AddTransient<IFindSpecificationQuery, EfFindSpecificationQuery>();
            services.AddTransient<ICreateSpecificationCommand, EfCreateSpecificationCommand>();
            services.AddTransient<IDeleteSpecificationCommand, EfDeleteSpecificationCommand>();
            services.AddTransient<IUpdateUserUseCasesCommand, EfUpdateUserUseCases>();
            #region Validators
            services.AddTransient<CreateCategoryValidator>();
            services.AddTransient<RegisterUserValidator>();
            services.AddTransient<CreateSpecificationValidator>();
            services.AddTransient<UpdateUserUseCasesValidator>();
            services.AddTransient<SearchUseCaseLogsValidator>();
            #endregion
        }

        public static void AddApplicationUser(this IServiceCollection services)
        {
            services.AddTransient<IApplicationUser>(x =>
            {
                var accessor = x.GetService<IHttpContextAccessor>();
                var header = accessor.HttpContext.Request.Headers["Authorization"];

                //Pristup payload-u
                var claims = accessor.HttpContext.User;

                if(claims == null || claims.FindFirst("UserId") == null)
                {
                    return new AnonimousUser();
                }

                var actor = new JwtUser
                {
                    Email = claims.FindFirst("Email").Value,
                    Id = Int32.Parse(claims.FindFirst("UserId").Value),
                    Identity = claims.FindFirst("Email").Value,
                    // "[1, 2, 3, 4, 5]"
                    UseCaseIds = JsonConvert.DeserializeObject<List<int>>(claims.FindFirst("UseCases").Value)
                };

                return actor;
            });
        }

        public static void AddVezbeDbContext(this IServiceCollection services)
        {
            services.AddTransient(x =>
            {
                var optionsBuilder = new DbContextOptionsBuilder();

                var conString = x.GetService<AppSettings>().ConnString;

                optionsBuilder.UseSqlServer(conString).UseLazyLoadingProxies();

                var options = optionsBuilder.Options;

                return new VezbeDbContext(options);
            });
        }
    }
}
