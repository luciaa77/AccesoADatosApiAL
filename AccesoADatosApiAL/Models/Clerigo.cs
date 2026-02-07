// Autor: Lucia
using System.ComponentModel.DataAnnotations.Schema;

namespace AccesoDatosApiAL.Models
{
    [Table("Clerigos")]
    public class Clerigo : Personaje
    {
        public string? Deidad { get; set; }
        public int PuntosSanacion { get; set; }
    }
}
