using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Everton._123Vendas.Infrastructure.Data.Repository;
using Everton._123Vendas.Infrastructure.IoC;
using Everton._123Vendas.API.Filters;
using Everton._123Vendas.API.Mappers;
using Everton._123Vendas.Domain.Services.Notification;
using Everton._123Vendas.Domain.Interfaces.Notification;

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

builder.Services.AddControllers(opt =>
{
    opt.Filters.Add<NotificationFilter>();
});

//var dataSourceBuilder = new NpgsqlDataSourceBuilder(builder.Configuration.GetConnectionString("DefaultConnection"));
////dataSourceBuilder.MapEnum<Mood>();
////dataSourceBuilder.UseNodaTime();
//var dataSource = dataSourceBuilder.Build();

//builder.Services.AddDbContextPool<RepositoryContext>(opt => opt.UseNpgsql(dataSource));

builder.Services.AddHttpContextAccessor();
builder.Services.AddAutoMapper(typeof(CompraProfile));
builder.Services.AddDependencyInjection();

builder.Services.AddDbContextPool<RepositoryContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    ServiceLocator.Initialize(scope.ServiceProvider);
}


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
