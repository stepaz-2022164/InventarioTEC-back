using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class TipoDeEquipo
{
    [Key]
    [Required(ErrorMessage = "El parámetro {0} es obligatorio")]
    [Column("idTipoDeEquipo", TypeName = "int")]
    public int idTipoDeEquipo {get; set; }

    [Required(ErrorMessage = "El parámetro {0} es obligatorio")]
    [MaxLength(100, ErrorMessage = "El parámetro {0} solo puede tener {1} carácteres")]
    [Column("nombreTipoDeEquipo", TypeName = "varchar(100)")]
    public string nombreTipoDeEquipo { get; set; } = null!;

    [Required(ErrorMessage = "El parámetro {0} es obligatorio")]
    [DataType(DataType.MultilineText)]
    [MaxLength(100, ErrorMessage = "El parámetro {0} solo puede tener {1} carácteres")]
    [Column("descripcionTipoDeEquipo", TypeName = "text")]
    public string descripcionTipoDeEquipo { get; set; } = null!;

    [Required(ErrorMessage = "El parámetro {0} es obligatorio")]
    [Column("stock", TypeName = "int")]
    public int stock { get; set; }

    [Required(ErrorMessage = "El parámetro {0} es obligatorio")]
    [Column("idMarca", TypeName = "int")]
    public int idMarca { get; set; }

    [ForeignKey("idMarca")]
    public Marca Marca { get; set; } = null!;
}