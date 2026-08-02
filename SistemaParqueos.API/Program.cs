using Microsoft.EntityFrameworkCore;
using SistemaParqueos.AccesoDatos.Contexto;
using SistemaParqueos.AccesoDatos.Implementaciones;
using SistemaParqueos.Dominio.InterfacesAD;
using SistemaParqueos.Dominio.InterfazLN;
using SistemaParqueos.LogicaNegocio.Implementaciones;
using SistemaParqueos.API.Middleware;


var builder = WebApplication.CreateBuilder(args);

string connectionString =
    builder.Configuration.GetConnectionString(
        "ParqueosConnection"
    )
    ?? throw new InvalidOperationException(
        "No se encontró la cadena de conexión ParqueosConnection."
    );

builder.Services.AddDbContext<ParqueosContext>(
    options =>
        options.UseSqlServer(connectionString)
);
// Inyección de dependencias
builder.Services.AddScoped<ITipoVehiculoAD, TipoVehiculoAD>();
builder.Services.AddScoped<ITipoVehiculoLN, TipoVehiculoLN>();
builder.Services.AddScoped<IClienteAD, ClienteAD>();
builder.Services.AddScoped<IClienteLN, ClienteLN>();
builder.Services.AddScoped<IParqueoAD, ParqueoAD>();
builder.Services.AddScoped<IParqueoLN, ParqueoLN>();
builder.Services.AddScoped<IVehiculoAD, VehiculoAD>();
builder.Services.AddScoped<IVehiculoLN, VehiculoLN>();
builder.Services.AddScoped<IEspacioParqueoAD, EspacioParqueoAD>();
builder.Services.AddScoped<IEspacioParqueoLN, EspacioParqueoLN>();
builder.Services.AddScoped<ITarifaAD, TarifaAD>();
builder.Services.AddScoped<ITarifaLN, TarifaLN>();
builder.Services.AddScoped<IFacturaAD, FacturaAD>();
builder.Services.AddScoped<IFacturaLN, FacturaLN>();
builder.Services.AddScoped<IIngresoVehiculoAD, IngresoVehiculoAD>();
builder.Services.AddScoped<IIngresoVehiculoLN, IngresoVehiculoLN>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("IonicApp", policy =>
    {
        policy
            .WithOrigins("http://localhost:8100")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});
builder.Services.AddControllers();
builder.Services.AddOpenApi();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();
app.UseMiddleware<ManejadorExcepcionesMiddleware>();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint(
            "/openapi/v1.json",
            "Sistema Parqueos API v1"
        );
    });
}

app.UseHttpsRedirection();

app.UseCors("IonicApp");

app.UseAuthorization();

app.MapControllers();

app.Run();
