using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class DepartamentoEmpleado
{
    [Key]
    [Required(ErrorMessage = "El parametro {0} es obligatorio")]
    [Column("idDepartamentoEmpleado", TypeName = "int")]
    public int idDepartamentoEmpleado { get; set; }

    [Required(ErrorMessage = "El parametro {0} es obligatorio")]
    [MaxLength(100 , ErrorMessage = "El parametro {0} solo puede tener {1} caracteres")]
    [Column("nombreDepartamentoEmpleado", TypeName = "varchar(100)")]
    public string nombreDepartamentoEmpleado { get; set; } = null!;

    [Required(ErrorMessage = "El parametro {0} es obligatorio")]
    [DataType(DataType.MultilineText)]
    [MaxLength(100 , ErrorMessage = "El parametro {0} solo puede tener {1} caracteres")]
    [Column("descripcionAreaEmpleado", TypeName = "varchar(200)")]
    public string descripcionAreaEmpleado { get; set; } = null!;
}