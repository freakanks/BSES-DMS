using BSES.DocumentManagementSystem;
using BSES.DocumentManagementSystem.Business;
using BSES.DocumentManagementSystem.Common;
using BSES.DocumentManagementSystem.Data;
using BSES.DocumentManagementSystem.Data.FileSystem;
using BSES.DocumentManagementSystem.Encryption.Data;
using Microsoft.OpenApi.Models;
using Oracle.EntityFrameworkCore.Query.Internal;
using Serilog;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//Add Swagger Services 
builder.Services.AddSwaggerGen(options =>
{
    options.UseInlineDefinitionsForEnums();
    options.SwaggerDoc("v1", new OpenApiInfo()
    {
        Title = "DMS API",
        Description = "This is the DMS application which will serve as a common Document Storage point for all the internal applications by the BSES team.",
        Version = "v1"
    });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization Header using the Bearer scheme. \r\n\r\n",

    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            new string[] {}
        }
    });
});
var sessionTimeOut = builder.Configuration.GetValue<int>(DMSConstants.SESSION_TIMEOUT_MINUTES);
sessionTimeOut = sessionTimeOut != 0 ? sessionTimeOut : 240;

///DMS Services Registration.
builder.Services.AddDistributedMemoryCache()
                .AddHttpContextAccessor()
                .ConfigureApplicationCookie(options => options.Cookie.Expiration = TimeSpan.FromMinutes(sessionTimeOut))
                .AddSession(options => options.IdleTimeout = TimeSpan.FromMinutes(sessionTimeOut))
                .AddJWT(builder.Configuration)
                .AddDataServicesFileSystem()
                .AddDataServicesDB(builder.Configuration)
                //.AddEncryptionDataServices(builder.Configuration)
                .AddBusinessServices();

builder.Host.UseSerilog((context, serviceProvider, loggerConfiguration) =>
{
    loggerConfiguration
    .ReadFrom.Configuration(context.Configuration)
    .ReadFrom.Services(serviceProvider)
    .Enrich.FromLogContext()
    .WriteTo.Debug();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<HandleExceptionMiddleware>();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();
app.MapControllers();
app.UseSwagger(options =>
{
    options.RouteTemplate = "DocumentManagementSystemAPI/swagger/{documentname}/swagger.json";
});
app.UseSwaggerUI(options =>
{

    options.SwaggerEndpoint("/DocumentManagementSystemAPI/swagger/v1/swagger.json", "DocumentManagementSystem API V1");
    options.RoutePrefix = "DocumentManagementSystemAPI/swagger";
});

app.Run();
