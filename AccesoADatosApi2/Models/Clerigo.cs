using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccesoADatosApi2.Models;

[Table("Clerigos")]
public class Clerigo : Personaje
{
    [Required]
    public string Deidad { get; set; } = string.Empty;

    public int PuntosSanacion { get; set; }
}
