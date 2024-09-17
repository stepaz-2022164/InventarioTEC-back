using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Equipo
{
    [Key]
    [Required(ErrorMessage = "El parámetro {0} es obligatorio")]
    [Column("idEquipo", TypeName = "int")]
    public int idEquipo { get; set; }

    [Required(ErrorMessage = "El parámetro {0} es obligatorio")]
    [MaxLength(50, ErrorMessage = "El parámetro {0} solo puede tener {1} carácteres")]
    [Column("numeroDeSerie", TypeName = "varchar(50)")]
    public string numeroDeSerie {get; set;} = null!;

    [Required(ErrorMessage = "El parámetro {0} es obligatorio")]
    [MaxLength(10, ErrorMessage = "El parámetro {0} solo puede tener {1} carácteres")]
    [Column("estado", TypeName = "varchar(10)")]
    public string estado { get; set; } = null!;

    [Required(ErrorMessage = "El parámetro {0} es obligatorio")]
    [DataType(DataType.Date)]
    [Column("fechaDeIngreso", TypeName = "date")]
    public System.DateOnly fechaDeIngreso { get; set; }

    [Required(ErrorMessage = "El parámetro {0} es obligatorio")]
    [Column("idTipoDeEquipo", TypeName = "int")]
    public int idTipoDeEquipo { get; set; }

    [ForeignKey("idTipoDeEquipo")]
    public TipoDeEquipo TipoDeEquipo {get; set; } = null!;
}