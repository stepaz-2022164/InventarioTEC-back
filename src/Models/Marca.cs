using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Marca
{
    [Key]
    [Required(ErrorMessage = "El parámetro {0} es obligatorio")]
    [Column("idMarca", TypeName = "int")]
    public int idMarca { get; set; }

    [Required(ErrorMessage = "El parámetro {0} es obligatorio")]
    [MaxLength(100, ErrorMessage = "El parámetro {0} solo puede tener {1} carácteres")]
    [Column("nombreMarca", TypeName = "varchar(100)")]
    public string nombreMarca { get; set; } = null!;

    [Required(ErrorMessage = "El parámetro {0} es obligatorio")]
    [DataType(DataType.MultilineText)]
    [MaxLength(100, ErrorMessage = "El parámetro {0} solo puede tener {1} carácteres")]
    [Column("descripcionMarca", TypeName = "varchar(100)")]
    public string descripcionMarca { get; set; } = null!;

    [Column("estado", TypeName = "int")]
    public int estado {get; set;} = 1;
}