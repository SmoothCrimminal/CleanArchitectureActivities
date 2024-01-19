using Microsoft.EntityFrameworkCore;
using Persistence;
using WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices(builder.Configuration);

var app = builder.Build();
app.UseSwaggerUI();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

try
{
    var context = services.GetRequiredService<DataContext>();
    await context.Database.MigrateAsync();
    await Seed.SeedData(context);
}
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "Error occured during migration");
}

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.MapControllers();
app.Run();