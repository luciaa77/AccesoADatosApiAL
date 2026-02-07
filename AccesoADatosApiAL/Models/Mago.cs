// Autor: Lucia
using System.ComponentModel.DataAnnotations.Schema;

namespace AccesoDatosApiAL.Models
{
    [Table("Magos")]
    public class Mago : Personaje
    {
        public int Mana { get; set; }
        public string? ElementoPrincipal { get; set; }
    }
}
