using Microsoft.OpenApi;
using MotorSync.Application;
using MotorSync.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// 1. Capa Application: Casos de uso
builder.Services.AddApplicationServices();

// 2. Capa Infrastructure: EF Core DbContext, Repositorios, Servicios
builder.Services.AddInfrastructureServices(builder.Configuration);

// 3. Controladores y JSON
builder.Services.AddControllers();

// 4. Documentación con Swagger / OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "MotorSync API",
        Version = "v1",
        Description = "Sistema de Gestión de Talleres Automotrices - MotorSync. Diseñado bajo Clean Architecture y principios SOLID."
    });
});

var app = builder.Build();

// Pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "MotorSync API v1");
        c.RoutePrefix = string.Empty; // Permite abrir Swagger directamente en la raíz ("/")
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
