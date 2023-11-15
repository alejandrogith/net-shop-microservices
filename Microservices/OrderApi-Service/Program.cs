using Microsoft.EntityFrameworkCore;
using OrderApi_Service.Middleware;
using OrdersApi.Configurations;
using OrdersApi.Data;
using OrdersApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddConsulServicesConfiguration(builder.Configuration);



builder.Services.AddDbContext<OrderDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConexionSQL"));
}, ServiceLifetime.Singleton);



builder.Services.AddScoped<IOrderService,OrderService>();
builder.Services.AddHttpClient();


var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{

    var res = builder.Configuration.GetConnectionString("ConexionSQL");
    var context = scope.ServiceProvider.GetRequiredService<OrderDbContext>();
    await context.Database.MigrateAsync();
}

app.UseGlobalCustomErrorMiddleware();



app.MapControllers();

app.Run();
