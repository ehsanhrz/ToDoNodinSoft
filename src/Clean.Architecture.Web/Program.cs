using Ardalis.ListStartupServices;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Clean.Architecture.Core;
using Clean.Architecture.Infrastructure;
using Clean.Architecture.Infrastructure.Data;
using FastEndpoints.Swagger.Swashbuckle;
using Microsoft.OpenApi.Models;
using Serilog;
using Clean.Architecture.Core.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;



using Clean.Architecture.Web;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Host.UseSerilog((_, config) => config.ReadFrom.Configuration(builder.Configuration));

builder.Services.Configure<CookiePolicyOptions>(options =>
{
  options.CheckConsentNeeded = context => true;
  options.MinimumSameSitePolicy = SameSiteMode.None;
});

builder.Services.AddCors(options =>
{
  options.AddPolicy(name: MyAllowSpecificOrigins,
    policy  =>
    {
      policy.WithOrigins("http://localhost:4200","http://localhost:57679").AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
    });
});

string? connectionString = builder.Configuration.GetConnectionString("SqliteConnection");  //Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext(connectionString!);

// add list services for diagnostic purposes - see https://github.com/ardalis/AspNetCoreStartupServices
builder.Services.Configure<ServiceConfig>(config =>
{
  config.Services = new List<ServiceDescriptor>(builder.Services);
  // optional - default path to view services is /listallservices - recommended to choose your own path
  config.Path = "/listservices";
});

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JwtOptions"));
// JwtBearerOptionsSetup
builder.Services.ConfigureOptions<JwtBearerOptionsSetup>();
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
  containerBuilder.RegisterModule(new DefaultCoreModule());
  containerBuilder.RegisterModule(new DefaultInfrastructureModule(builder.Environment.EnvironmentName == "Development"));
});

builder.Services.AddSwaggerGen(c =>
{
  c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
  c.EnableAnnotations();
  c.OperationFilter<FastEndpointsOperationFilter>();
});


builder.Services.AddControllers();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{

  c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
  {
    In = ParameterLocation.Header,
    Description = "Please enter token",
    Name = "Authorization",
    Type = SecuritySchemeType.Http,
    BearerFormat = "JWT",
    Scheme = "bearer"
  });
  c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"); c.RoutePrefix = ""; });
}


using (var scope = app.Services.CreateScope())
{
  var services = scope.ServiceProvider;

  try
  {
    var context = services.GetRequiredService<AppDbContext>();
    //context.Database.Migrate();

    context.Database.EnsureCreated();
    //SeedData.Initialize(services);
  }
  catch (Exception ex)
  {
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occurred seeding the DB. {exceptionMessage}", ex.Message);
  }
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
app.UseAuthorization();

app.MapControllers();

app.Run();
public partial class Program { }
