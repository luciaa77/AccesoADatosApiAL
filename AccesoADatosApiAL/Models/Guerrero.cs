// Autor: Lucia
using System.ComponentModel.DataAnnotations.Schema;

namespace AccesoDatosApiAL.Models
{
    [Table("Guerreros")]
    public class Guerrero : Personaje
    {
        public string? ArmaPrincipal { get; set; }
        public int Furia { get; set; }
    }
}
