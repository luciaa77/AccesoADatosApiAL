// Autor: Lucia
using System.ComponentModel.DataAnnotations.Schema;

namespace AccesoDatosApiAI.Models
{
    [Table("Clerigos")]
    public class Clerigo : Personaje
    {
        public string? Deidad { get; set; }
        public int PuntosSanacion { get; set; }
    }
}
