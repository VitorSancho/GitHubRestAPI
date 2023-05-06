using AutoMapper;
using GitHub.Business;
using GitHub.Data;
using GitHub.Service;
using GitHubRestAPI.Profiles;
using System.Text.Json.Serialization;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers().AddJsonOptions(o => o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddAutoMapper(typeof(ListOfLanguagesProfile));


        builder.Services.AddSingleton<IGitHubBusiness, GitHubBusiness>();
        builder.Services.AddSingleton<IGitHubService, GitHubService>();
        builder.Services.AddSingleton<IGitHubRepository, GitHubRepository>();
        

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger(c => { c.SerializeAsV2 = true; });
            app.UseSwaggerUI(
                c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "V1");
                });
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}