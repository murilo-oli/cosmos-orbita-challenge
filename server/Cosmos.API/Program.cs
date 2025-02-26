using Microsoft.EntityFrameworkCore;
using Cosmos.Infrastructure.Context;
using Cosmos.IOC;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json.Serialization;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

DotNetEnv.Env.Load("../");

builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();

ConfigurationManager config = builder.Configuration;
builder.Services.AddSingleton(d => config);

builder.Services.AddDbContext<CosmosDbContext>(options =>
    options.UseNpgsql(Environment.GetEnvironmentVariable("CONNECTION_STRING")),
    ServiceLifetime.Scoped
);

builder.Services.Inject();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.Configure<KestrelServerOptions>(options =>
{
    options.Limits.MaxRequestBodySize = int.MaxValue;
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(s =>
    {
        s.SwaggerEndpoint("/swagger/v1/swagger.json", "Cosmos v1");
    });
}

app.UseCors();
app.UseRouting();
app.UseAuthentication(); 
app.UseAuthorization();
app.MapControllers();
app.UseHttpsRedirection();

app.Run();
