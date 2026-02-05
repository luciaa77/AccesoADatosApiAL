// Autor: (Tu nombre) - D&DSoft
using System.ComponentModel.DataAnnotations.Schema;

namespace AccesoDatosApiAI.Models
{
    [Table("Guerreros")]
    public class Guerrero : Personaje
    {
        public string? ArmaPrincipal { get; set; }
        public int Furia { get; set; }
    }
}
