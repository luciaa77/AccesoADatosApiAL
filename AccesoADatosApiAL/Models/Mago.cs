// Autor: (Tu nombre) - D&DSoft
using System.ComponentModel.DataAnnotations.Schema;

namespace AccesoDatosApiAI.Models
{
    [Table("Magos")]
    public class Mago : Personaje
    {
        public int Mana { get; set; }
        public string? ElementoPrincipal { get; set; }
    }
}
