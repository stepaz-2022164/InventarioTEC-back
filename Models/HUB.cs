using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class HUB
{
    [Key]
    [Required(ErrorMessage = "El parametro {0} es obligatorio")]
    [Column("idHUB", TypeName = "int")]
    public int idHUB { get; set; }

    [Required(ErrorMessage = "El parametro {0} es obligatorio")]
    [MaxLength(100, ErrorMessage = "El paramtetro {0} solo puede tener {1} caracteres")]
    [Column("nombreHUB", TypeName = "varchar(100)")]
    public string nombreHUB { get; set; } = null!;

    [Required(ErrorMessage = "El parametro {0} es obligatorio")]
    [Column("idRegion", TypeName = "int")]
    public int idRegion { get; set; }
    
    [ForeignKey("idRegion")]
    public Region Region { get; set; } = null!;

    [Required(ErrorMessage = "El parametro {0} es obligatorio")]
    [Column("idPais", TypeName = "int")]
    public int idPais { get; set; }

    [ForeignKey("idPais")]
    public Pais Pais { get; set; } = null!;
}