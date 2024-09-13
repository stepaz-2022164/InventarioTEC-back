using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Edificio
{
    [Key]
    [Required(ErrorMessage = "El parámetro {0} es obligatorio")]
    [Column("idEdificio", TypeName = "int")]
    public int idEdificio { get; set; }

    [Required(ErrorMessage = "El parámetro {0} es obligatorio")]
    [Column("cantidadOficinas", TypeName = "int")]
    public int cantidadOficinas { get; set; }

    [Required(ErrorMessage = "El parámetro {0} es obligatorio")]
    [Column("cantidadNiveles", TypeName = "int")]
    public int cantidadNiveles { get; set; }

    [Required(ErrorMessage = "El parámetro {0} es obligatorio")]
    [MaxLength(100, ErrorMessage = "El parámetro {0} solo puede tener {1} carácteres")]
    [Column("nombreEdificio", TypeName = "varchar(100)")]
    public string nombreEdificio { get; set; } = null!;

    [Required(ErrorMessage = "El parámetro {0} es obligatorio")]
    [DataType(DataType.MultilineText)]
    [MaxLength(100, ErrorMessage = "El parámetro {0} solo puede tener {1} carácteres")]
    [Column("descripcionEdificio", TypeName = "varchar(100)")]
    public string descripcionEdificio { get; set; } = null!;

    [Required(ErrorMessage = "El parámetro {0} es obligatorio")]
    [Column("idSede", TypeName = "int")]
    public int idSede { get; set; }

    [ForeignKey("idSede")]
    public Sede Sede { get; set; } = null!;
}