using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class AreaEmpleado
{
    [Key]
    [Required(ErrorMessage = "El parametro {0} es obligatorio")]
    [Column("idAreaEmpleado", TypeName = "int")]
    public int idAreaEmpleado { get; set; }

    [Required(ErrorMessage = "El parametro {0} es obligatorio")]
    [MaxLength(100 , ErrorMessage = "El parametro {0} solo puede tener {1} caracteres")]
    [Column("nombreAreaEmpleado", TypeName = "varchar(100)")]
    public string nombreAreaEmpleado { get; set; } = null!;

    [Required(ErrorMessage = "El parametro {0} es obligatorio")]
    [DataType(DataType.MultilineText)]
    [MaxLength(100 , ErrorMessage = "El parametro {0} solo puede tener {1} caracteres")]
    [Column("descripcionAreaEmpleado", TypeName = "varchar(100)")]
    public string descripcionAreaEmpleado { get; set; } = null!;

    [Required(ErrorMessage = "El parametro {0} es obligatorio")]
    [Column("idDepartamentoEmpleado", TypeName = "int")]
    public int idDepartamentoEmpleado { get; set; }

    [ForeignKey("idDepartamentoEmpleado")]
    public DepartamentoEmpleado DepartamentoEmpleado { get; set; } = null!;

    [Column("estado", TypeName = "int")]
    public int estado {get; set;} = 1;
}