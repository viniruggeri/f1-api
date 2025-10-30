using F1.Api.Domain.Interfaces;
using F1.Api.Infrastructure.Data;
using F1.Api.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddDbContext<F1DbContext>(options =>
    options.UseOracle(
        builder.Configuration.GetConnectionString("OracleConnection"),
        oracleOptions =>
        {
            oracleOptions.UseOracleSQLCompatibility(OracleSQLCompatibility.DatabaseVersion19);
        }
    ));

builder.Services.AddScoped<IEquipeRepository, EquipeRepository>();
builder.Services.AddScoped<IPilotoRepository, PilotoRepository>();
builder.Services.AddScoped<ICorridaRepository, CorridaRepository>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "F1 API - CP2 Advanced Business Development with .NET",
        Version = "v1",
        Description = "API RESTful para gerenciamento de dados da Fórmula 1",
        Contact = new OpenApiContact
        {
            Name = "FIAP - Turma 2TDSPR",
        }
    });

    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", corsBuilder =>
    {
        corsBuilder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "F1 API v1");
    c.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<F1DbContext>();
    context.Database.Migrate();
}

app.Run();
