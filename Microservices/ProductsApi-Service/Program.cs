using Microsoft.EntityFrameworkCore;
using ProductsApi.Data;
using ProductsApi.Service;
using Consul;
using ProductsApi.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

builder.Services.AddDbContext<ProductDbContext>(opciones =>
opciones.UseSqlServer(builder.Configuration.GetConnectionString("ConexionSQL")));

builder.Services.AddScoped<IProductService, ProductService>();


builder.Services.AddConsulServicesConfiguration(builder.Configuration);


var app = builder.Build();


using (var scope = app.Services.CreateScope()) {

    var res = builder.Configuration.GetConnectionString("ConexionSQL");
    var context = scope.ServiceProvider.GetRequiredService<ProductDbContext>();
    await context.Database.MigrateAsync();
}











// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{



    app.UseSwagger();
    app.UseSwaggerUI();
}



app.MapControllers();

app.Run();
