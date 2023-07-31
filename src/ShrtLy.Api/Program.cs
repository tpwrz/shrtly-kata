using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using ShrtLy.Api.Extencions;
using ShrtLy.Api.Extentions;
using ShrtLy.Application.Features;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
    .MinimumLevel.Debug()
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;

// Add services to the container.
builder.Host.UseSerilog();

builder.AddControllersConfig(); // Setup Controllers and their configuration

builder.Services.AddEndpointsApiExplorer(); // Enable API endpoint exploration for documentation

builder.ConfigureDatabaseServices(); // Setup Identity Database and Repositories

builder.AddMediator(); // Setup Mediator

builder.AddServices(); // Setup Services

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
app.MigrateDatabase(); // Perform database migration on web application

app.UseHttpsRedirection();

// Enable middleware to serve generated Swagger as a JSON endpoint.
app.UseSwagger();

// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
// specifying the Swagger JSON endpoint.
app.UseSwaggerUI(
    c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
}
);

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.MapGet("/{url}", async (
    string url, IMediator mediator
) =>
{
    var command = new RedirectCommand(url);
    var result = await mediator.Send(command); ;
    if (result.Data == null || string.IsNullOrEmpty(result.Data))
        return Results.NotFound();

    return Results.Redirect(result.Data);
});

app.Run();