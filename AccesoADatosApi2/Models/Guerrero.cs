using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccesoADatosApi2.Models;

[Table("Guerreros")]
public class Guerrero : Personaje
{
    [Required]
    public string ArmaPrincipal { get; set; } = string.Empty;

    public int Furia { get; set; }
}
