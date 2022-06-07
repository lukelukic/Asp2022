using AspNedelja3.Implementation;
using AspNedelja3.Implementation.Emails;
using AspNedelja3.Implementation.Logging;
using AspNedelja3.Implementation.UseCases.UseCaseLogger;
using ASPNedelja3.Application.Emails;
using ASPNedelja3.Application.Logging;
using ASPNedelja3.Application.UseCases;
using AspNedelja3Vezbe.Api.Payment;
using ASPNedelja3Vezbe.Api.Core;
using ASPNedelja3Vezbe.Api.Emails;
using ASPNedelja3Vezbe.Api.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace ASPNedelja3Vezbe.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var settings = new AppSettings();
            
            Configuration.Bind(settings);

            services.AddSingleton(settings);
            services.AddApplicationUser();
            services.AddJwt(settings);
            services.AddVezbeDbContext();
            services.AddUseCases();
            services.AddSingleton<IPaymentMethod, BankPaymentMethod>();
            services.AddTransient<OrderProcessor>();
            services.AddTransient<IExceptionLogger, ConsoleExceptionLogger> ();
            services.AddTransient<IUseCaseLogger>(x => new SpUseCaseLogger(settings.ConnString));
            services.AddTransient<UseCaseHandler>();
            services.AddTransient<IEmailSender>(x => 
            new SmtpEmailSender(settings.EmailOptions.FromEmail,
                                settings.EmailOptions.Password,
                                settings.EmailOptions.Port,
                                settings.EmailOptions.Host));
            services.AddControllers();
            services.AddHttpContextAccessor();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ASPNedelja3Vezbe.Api", Version = "v1" });

                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ASPNedelja3Vezbe.Api v1"));
            }

            //var instance = app.ApplicationServices.GetService<IPaymentMethod>();

            app.UseRouting();
            app.UseMiddleware<GlobalExceptionHandler>();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
