using SistemaParqueos.Dominio.DTO;

namespace SistemaParqueos.Dominio.InterfazLN;

public interface IAuthLN
{
    Task CrearAdministradorInicialAsync(
        CrearAdministradorInicialDTO administradorDTO
    );

    Task<RespuestaLoginDTO> IniciarSesionAsync(
        LoginDTO loginDTO
    );
}