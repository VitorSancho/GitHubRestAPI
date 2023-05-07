using AutoMapper;
using GitHub.Business;
using GitHub.Data;
using GitHub.Service;
using GitHubRestAPI.Profiles;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text.Json.Serialization;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers().AddNewtonsoftJson();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddApiVersioning( options =>
        {
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new ApiVersion(1, 0);
        });

        builder.Services.AddSwaggerGen(options =>
        {
            var contact = new OpenApiContact
            {
                Name = "Vitor Sancho Cardoso",
                Email = "vitor.sancho07@gmail.com"
            };
            var apiDescription = "An ASP.NET Core Web API to consult the most famous programming languages repositories";
            var apiTitle = "GitHubReastAPI";
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = apiTitle,
                Description = apiDescription,
                Contact = contact
            }); ;

            options.SwaggerDoc("v2", new OpenApiInfo
            {
                Version = "v2",
                Title = apiTitle,
                Description = apiDescription,
                Contact = contact
            });
            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });
        builder.Services.AddAndConfigureApiVersioning();
        builder.Services.AddAutoMapper(typeof(ListOfLanguagesProfile));


        builder.Services.AddSingleton<IGitHubBusiness, GitHubBusiness>();
        builder.Services.AddSingleton<IGitHubService, GitHubService>();
        builder.Services.AddSingleton<IGitHubRepository, GitHubRepository>();

        builder.Services.AddCors();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger(c => { c.SerializeAsV2 = true; });
            app.UseSwaggerUI(
                c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "V1");
                    c.SwaggerEndpoint("/swagger/v2/swagger.json", "V2");
                });
        }

        app.UseCors(c =>
        {
            c.AllowAnyHeader();
            c.AllowAnyMethod();
            c.AllowAnyOrigin();
        }
        );

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}