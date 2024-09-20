using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class PuestoEmpleado
{
    [Key]
    [Required(ErrorMessage = "El parámetro {0} es obligatorio")]
    [Column("idPuestoEmpleado", TypeName = "int")]
    public int idPuestoEmpleado {get; set;}

    [Required(ErrorMessage = "El parámetro {0} es obligatorio")]
    [MaxLength(100, ErrorMessage = "El parámetro {0} solo puede tener {1} carácteres")]
    [Column("nombrePuestoEmpleado", TypeName = "varchar(100)")]
    public string nombrePuestoEmpleado { get; set; } = null!;

    [Required(ErrorMessage = "El parámetro {0} es obligatorio")]
    [MaxLength(100, ErrorMessage = "El parámetro {0} solo puede tener {1} carácteres")]
    [Column("descripcionPuestoEmpleado", TypeName = "varchar(200)")]
    public string descripcionPuestoEmpleado { get; set; } = null!;

    [Required(ErrorMessage = "El parámetro {0} es obligatorio")]
    [Column("idAreaEmpleado", TypeName = "int")]
    public int idAreaEmpleado { get; set; }

    [ForeignKey("idAreaEmpleado")]
    public AreaEmpleado AreaEmpleado { get; set; } = null!;

    [Column("estado", TypeName = "int")]
    public int estado {get; set;} = 1;
}