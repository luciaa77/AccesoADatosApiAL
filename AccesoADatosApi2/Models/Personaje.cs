using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace AccesoADatosApi2.Models;

[Table("Personajes")]
public abstract class Personaje
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required, MaxLength(50)]
    public string Nombre { get; set; } = string.Empty;

    [Range(1, 100)]
    public int Nivel { get; set; }

    [Required]
    public DateTime FechaCreacion { get; set; }

    public string? Gremio { get; set; }

    // jsonb (dinámico)
    [Required]
    [Column(TypeName = "jsonb")]
    public JsonDocument Rasgos { get; set; } = JsonDocument.Parse("{}");
}
