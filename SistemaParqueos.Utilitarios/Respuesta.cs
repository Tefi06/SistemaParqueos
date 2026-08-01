namespace SistemaParqueos.Utilitarios;

public class Respuesta<T>
{
    public bool Exito { get; set; }

    public string Mensaje { get; set; } = string.Empty;

    public T? Datos { get; set; }

    public List<string> Errores { get; set; } = [];

    public static Respuesta<T> Exitosa(
        T datos,
        string mensaje = "Operación realizada correctamente.")
    {
        return new Respuesta<T>
        {
            Exito = true,
            Mensaje = mensaje,
            Datos = datos
        };
    }

    public static Respuesta<T> Fallida(
        string mensaje,
        List<string>? errores = null)
    {
        return new Respuesta<T>
        {
            Exito = false,
            Mensaje = mensaje,
            Errores = errores ?? []
        };
    }
}