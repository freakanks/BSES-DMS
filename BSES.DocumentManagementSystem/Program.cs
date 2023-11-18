using BSES.DocumentManagementSystem.Business;
using BSES.DocumentManagementSystem.Data;
using BSES.DocumentManagementSystem.Data.FileSystem;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//Add Swagger Services 
builder.Services.AddSwaggerGen(options =>
{
    options.UseInlineDefinitionsForEnums();
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo()
    {
        Title = "DMS API",
        Description = "This is the DMS application which will serve as a common Document Storage point for all the internal applications by the BSES team.",
        Version = "v1"
    });
});

///DMS Services Registration.
builder.Services.AddDataServicesFileSystem()
                .AddDataServicesDB()
                .AddBusinessServices();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

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
