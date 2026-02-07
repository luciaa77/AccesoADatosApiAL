using System.ComponentModel.DataAnnotations.Schema;

namespace AccesoADatosApi2.Models;

[Table("Arqueros")]
public class Arquero : Personaje
{
    public double Precision { get; set; }
    public bool TieneMascota { get; set; }
}
