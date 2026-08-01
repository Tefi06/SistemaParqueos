using System.Net;
using System.Text.Json;
using SistemaParqueos.Utilitarios;

namespace SistemaParqueos.API.Middleware;

public class ManejadorExcepcionesMiddleware
{
    private readonly RequestDelegate _next;

    private readonly ILogger<ManejadorExcepcionesMiddleware> _logger;

    public ManejadorExcepcionesMiddleware(
        RequestDelegate next,
        ILogger<ManejadorExcepcionesMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            await ManejarExcepcionAsync(context, exception);
        }
    }

    private async Task ManejarExcepcionAsync(
        HttpContext context,
        Exception exception)
    {
        _logger.LogError(
            exception,
            "Ocurrió un error durante la solicitud."
        );

        int codigoEstado;
        string mensaje;
        List<string> errores;

        switch (exception)
        {
            case InvalidOperationException:
                codigoEstado = (int)HttpStatusCode.Conflict;
                mensaje = "No se pudo completar la operación.";
                errores = [exception.Message];
                break;

            case KeyNotFoundException:
                codigoEstado = (int)HttpStatusCode.NotFound;
                mensaje = "No se encontró el recurso solicitado.";
                errores = [exception.Message];
                break;

            case ArgumentException:
                codigoEstado = (int)HttpStatusCode.BadRequest;
                mensaje = "Los datos enviados no son válidos.";
                errores = [exception.Message];
                break;

            default:
                codigoEstado =
                    (int)HttpStatusCode.InternalServerError;

                mensaje =
                    "Ocurrió un error interno en el servidor.";

                errores = [];
                break;
        }

        Respuesta<object?> respuesta =
            Respuesta<object?>.Fallida(
                mensaje,
                errores
            );

        context.Response.StatusCode = codigoEstado;
        context.Response.ContentType = "application/json";

        JsonSerializerOptions opciones = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        string resultado = JsonSerializer.Serialize(
            respuesta,
            opciones
        );

        await context.Response.WriteAsync(resultado);
    }
}