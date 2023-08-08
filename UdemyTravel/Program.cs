
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using UdemyTravel.Database;
using UdemyTravel.Services;

namespace UdemyTravel
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers(setupAction =>
            {
                // setup need correct media type in header
                setupAction.ReturnHttpNotAcceptable = true;
                
            }).AddNewtonsoftJson(setupAction =>
            {
                setupAction.SerializerSettings.ContractResolver= new CamelCasePropertyNamesContractResolver();
            })
            // add only output serializer
            //setupAction.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
            .AddXmlDataContractSerializerFormatters()
            .ConfigureApiBehaviorOptions(setupAction =>
            {
                setupAction.InvalidModelStateResponseFactory = context =>
                {
                    var validationProblemDetails = new ValidationProblemDetails(context.ModelState)
                    {
                        Title = "Data validate Error",
                        Status = StatusCodes.Status422UnprocessableEntity,
                        Detail = "plz inspect details",
                        Instance = context.HttpContext.Request.Path
                    };
                    validationProblemDetails.Extensions.Add("traceId", context.HttpContext.TraceIdentifier);
                    return new UnprocessableEntityObjectResult(validationProblemDetails)
                    {
                        ContentTypes = { "applications/problem+json" }
                    };
                };
            });

            //builder.Services.AddTransient<ITouristRouteRepository, MockTouristRouteRepository>();
            builder.Services.AddTransient<ITouristRouteRepository, TouristRouteRepository>();
            builder.Services.AddDbContext<AppDbContext>(option =>
            {
                option.UseSqlServer(builder.Configuration["DbContext:ConnectionString"]);
            });

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}