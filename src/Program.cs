using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using WebApi.Authorization;
using WebApi.Helpers;
using WebApi.Repositories;
using WebApi.Services;

var builder = WebApplication.CreateBuilder(args);

// add services to DI container
{
    var services = builder.Services;
    var env = builder.Environment;
 
    services.AddDbContext<DataContext>();
    services.AddSwaggerGen();
    services.AddCors();
    services.AddControllers().AddJsonOptions(x => 
    {
        // serialize enums as strings in api responses (e.g. Role)
        x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
    services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    services.AddSwaggerGen();
    builder.Services.AddEndpointsApiExplorer();

    // configure strongly typed settings object
    services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
    

    // configure DI for application services
    services.AddScoped<IJwtUtils, JwtUtils>();
    services.AddScoped<IAccountService, AccountService>();
    services.AddScoped<IDeviceRepository, DeviceRepository>();
    services.AddScoped<IEmailService, EmailService>();
}

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle



var app = builder.Build();

// migrate any database changes on startup (includes initial db creation)
using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<DataContext>();    
    dataContext.Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
// generated swagger json and swagger ui middleware
    app.UseSwagger();
    app.UseSwaggerUI(x => x.SwaggerEndpoint("/swagger/v1/swagger.json", ".NET Sign-up and Verification API"));
}

app.UseHttpsRedirection();


// configure HTTP request pipeline
{
     // global cors policy
    app.UseCors(x => x
        .SetIsOriginAllowed(origin => true)
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials());

    // global error handler
    app.UseMiddleware<ErrorHandlerMiddleware>();

    // custom jwt auth middleware
    app.UseMiddleware<JwtMiddleware>();

    app.MapControllers();
}

app.Run("http://localhost:4000");
