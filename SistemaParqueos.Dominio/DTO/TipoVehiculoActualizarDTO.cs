using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace SistemaParqueos.Dominio.DTO;

public class TipoVehiculoActualizarDTO
{
    [Required(ErrorMessage = "La descripción es obligatoria.")]
    [StringLength(
        100,
        MinimumLength = 2,
        ErrorMessage = "La descripción debe tener entre 2 y 100 caracteres."
    )]
    public string Descripcion { get; set; } = string.Empty;

    public bool Activo { get; set; }
}