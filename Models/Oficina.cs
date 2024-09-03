using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Oficina
{
    [Key]
    [Required(ErrorMessage = "El parámetro {0} es obligatorio")]
    [Column("idOficina", TypeName = "int")]
    public int idOficina { get; set; }

    [Required(ErrorMessage = "El parámetro {0} es obligatorio")]
    [MaxLength(100, ErrorMessage = "El parámetro {0} solo puede tener {1} carácteres")]
    [Column("nombreOficina", TypeName = "varchar(100)")]
    public string nombreOficina { get; set; } = null!;

    [Required(ErrorMessage = "El parámetro {0} es obligatorio")]
    [DataType(DataType.MultilineText)]
    [MaxLength(100, ErrorMessage = "El parámetro {0} solo puede tener {1} carácteres")]
    [Column("descripcionOficina", TypeName = "varchar(100)")]
    public string descripcionOficina { get; set; } = null!;

    [Required(ErrorMessage = "El parámetro {0} es obligatorio")]
    [MaxLength(10, ErrorMessage = "El parámetro {0} solo puede tener {1} carácteres")]
    [Column("niveloficina", TypeName = "varchar(10)")]
    public string niveloficina {get; set;} = null!;

    [Required(ErrorMessage = "El parámetro {0} es obligatorio")]
    [Column("idEdificio", TypeName = "int")]
    public int idEdificio { get; set; }

    [ForeignKey("idEdificio")]
    public Edificio Edificio { get; set; } = null!;
}