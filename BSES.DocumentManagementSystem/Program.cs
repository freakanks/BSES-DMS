using BSES.DocumentManagementSystem.Business;
using BSES.DocumentManagementSystem.Data;
using BSES.DocumentManagementSystem.Data.FileSystem;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

///DMS Services Registration.
builder.Services.AddDataServicesFileSystem()
                .AddDataServicesDB()
                .AddBusinessServices();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
