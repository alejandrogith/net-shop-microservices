using InventoryApi_Service.Configurations;
using InventoryApi_Service.Data;
using InventoryApi_Service.Middleware;
using InventoryApi_Service.Service;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddConsulServicesConfiguration(builder.Configuration);
builder.Services.AddTransient<IInventoryService, InventoryService>();
builder.Services.AddHttpClient();

builder.Services.AddDbContext<InventoryDbContext>(opciones =>
opciones.UseSqlServer(builder.Configuration.GetConnectionString("ConexionSQL")));

var app = builder.Build();
app.UseCustomErrorMiddleware();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<InventoryDbContext>();
    await context.Database.MigrateAsync();
}



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
