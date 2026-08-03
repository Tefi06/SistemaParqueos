namespace SistemaParqueos.Dominio.DTO;

public class DashboardDTO
{
    public int VehiculosIngresadosHoy { get; set; }

    public int IngresosRegistrados { get; set; }

    public int EspaciosDisponibles { get; set; }

    public decimal FacturacionDiaria { get; set; }

    public decimal FacturacionMensual { get; set; }
}