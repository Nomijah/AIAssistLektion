using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using ProductInventory.Api.Data;
using ProductInventory.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddProblemDetails();

var connectionString = builder.Configuration.GetConnectionString("ProductInventory")
    ?? throw new InvalidOperationException("Connection string 'ProductInventory' is not configured.");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendDevelopment", policy =>
        policy.WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod());
});

var app = builder.Build();

app.UseExceptionHandler(exceptionHandlerApp =>
{
    exceptionHandlerApp.Run(async context =>
    {
        var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;
        app.Logger.LogError(exception, "An unexpected error occurred while handling the request.");
        await Results.Problem(
            statusCode: StatusCodes.Status500InternalServerError,
            title: "An unexpected error occurred.")
            .ExecuteAsync(context);
    });
});

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseCors("FrontendDevelopment");
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();

public partial class Program;
