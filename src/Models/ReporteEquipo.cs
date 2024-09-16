using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class ReporteEquipo
{
    [Key]
    [Required(ErrorMessage = "El parámetro {0} es obligatorio")]
    [Column("idReporteEquipo", TypeName = "int")]
    public int idReporteEquipo { get; set; }

    [Required(ErrorMessage = "El parámetro {0} es obligatorio")]
    [DataType(DataType.Date)]
    [Column("fechaReporte", TypeName = "date")]
    public System.DateOnly fechaReporte {get; set;}

    [Required(ErrorMessage = "El parámetro {0} es obligatorio")]
    [DataType(DataType.MultilineText)]
    [MaxLength(100, ErrorMessage = "El parámetro {0} solo puede tener {1} carácteres")]
    [Column("descripcionReporteEquipo", TypeName = "varchar(100)")]
    public string descripcionReporteEquipo { get; set; } = null!;

    [Required(ErrorMessage = "El parámetro {0} es obligatorio")]
    [Column("idEquipo", TypeName = "int")]
    public int idEquipo { get; set; }

    [ForeignKey("idEquipo")]
    public Equipo Equipo { get; set; } = null!;
}