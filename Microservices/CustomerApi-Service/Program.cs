using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using UsersApi.Configurations;
using UsersApi.Data;
using UsersApi.Middleware;
using UsersApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<UserDbContext>(opciones =>
opciones.UseSqlServer(builder.Configuration.GetConnectionString("ConexionSQL")));

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddConsulServicesConfiguration(builder.Configuration);


builder.Services.AddIdentity<UserEntity, IdentityRole>()
  .AddEntityFrameworkStores<UserDbContext>().AddDefaultTokenProviders();


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
    var context = scope.ServiceProvider.GetRequiredService<UserDbContext>();
    await context.Database.MigrateAsync();
}






app.UseGlobalCustomErrorMiddleware();


app.UseAuthorization();

app.MapControllers();

app.Run();
