

using ApiGateway.Configurations;

using Microsoft.Extensions.Options;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Consul;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddEndpointsApiExplorer();


builder.Services.AddOcelot(builder.Configuration)
    .AddDelegatingHandler<AutenticationDelegatingHandler>()
    .AddConsul() ;
 builder.Configuration.AddJsonFile("ocelot.json", optional:false,reloadOnChange:true);


builder.Services.AddHttpClient();


// Configure the HTTP request pipeline.
var app = builder.Build();




app.UseOcelot().Wait();



app.Run();
