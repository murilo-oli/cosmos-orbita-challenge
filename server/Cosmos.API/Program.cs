using Microsoft.EntityFrameworkCore;
using Cosmos.Infrastructure.Context;
using Cosmos.IOC;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Server.Kestrel.Core;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
ConfigurationManager config = builder.Configuration;
builder.Services.AddSingleton(d => config);
#region Database
builder.Services.AddDbContext<CosmosDbContext>(
    options => options.UseNpgsql(config.GetConnectionString("Local")),
    ServiceLifetime.Scoped
);

builder.Services.AddHttpContextAccessor();
#endregion

#region IOC
builder.Services.Inject();
#endregion

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
app.UseAuthorization();
app.UseRouting();
app.UseCors();

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
