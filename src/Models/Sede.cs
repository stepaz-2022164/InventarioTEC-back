using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Sede
{
    [Key]
    [Required(ErrorMessage = "El parametro {0} es obligatorio")]
    [Column("idSede", TypeName = "int")]
    public int idSede { get; set; }

    [Required(ErrorMessage = "El parametro {0} es obligatorio")]
    [MaxLength(100 , ErrorMessage = "El parametro {0} solo puede tener {1} caracteres")]
    [Column("nombreSede", TypeName = "varchar(100)")]
    public string nombreSede { get; set; } = null!;

    [Required(ErrorMessage = "El parametro {0} es obligatorio")]
    [MaxLength(100 , ErrorMessage = "El parametro {0} solo puede tener {1} caracteres")]
    [Column("direccionSede", TypeName = "varchar(100)")]
    public string direccionSede { get; set; } = null!;

    [Required(ErrorMessage = "El parametro {0} es obligatorio")]
    [Column("idPais", TypeName = "int")]
    public int idPais { get; set; }

    [ForeignKey("idPais")]
    public Pais Pais { get; set; } = null!;

    [Required(ErrorMessage = "El parametro {0} es obligatorio")]
    [Column("idRegion", TypeName = "int")]
    public int idRegion { get; set; }

    [ForeignKey("idRegion")]
    public Region Region { get; set; } = null!;

    [Required(ErrorMessage = "El parametro {0} es obligatorio")]
    [Column("idHUB", TypeName = "int")]
    public int idHUB { get; set; }

    [ForeignKey("idHUB")]
    public HUB HUB { get; set; } = null!;
}