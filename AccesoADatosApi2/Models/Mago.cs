using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccesoADatosApi2.Models;

[Table("Magos")]
public class Mago : Personaje
{
    public int Mana { get; set; }

    [Required]
    public string ElementoPrincipal { get; set; } = string.Empty;
}
