using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using System.Reflection;

const string CORS = "CORS";
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var env = Environment.GetEnvironmentVariable("ENV");
env ??= builder.Environment.EnvironmentName;

builder.Configuration
    .AddJsonFile($"appsettings.json", false, true)
    .AddJsonFile($"appsettings.{env}.json", false, true);

builder.Services.AddCors(opt =>
{
    opt.AddPolicy(CORS, policy => policy.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());
});

builder.Services.AddApiVersioning(config =>
{
    config.DefaultApiVersion = new ApiVersion(1, 0);
    config.AssumeDefaultVersionWhenUnspecified = true;
    config.ReportApiVersions = true;
    config.ApiVersionReader = new UrlSegmentApiVersionReader();
});

builder.Services.AddVersionedApiExplorer(config =>
{
    config.GroupNameFormat = "'v'VVV";
    config.SubstituteApiVersionInUrl = true;
});

//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen(opt =>
//{
//    var filename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
//    opt.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, filename));
//});

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    //var apiProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
    //app.UseSwagger();
    //app.UseSwaggerUI(opt =>
    //{
    //    foreach (var groupName in apiProvider.ApiVersionDescriptions.Select(x => x.GroupName))
    //    {
    //        opt.SwaggerEndpoint($"/swagger/{groupName}/swagger.json", groupName.ToUpperInvariant());
    //    }
    //});
}

app.UseCors(CORS);
app.UseHttpsRedirection();

app.MapControllers();
app.Run();
