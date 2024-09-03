using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Empleado
{
[Key]
    public int idEmpleado { get; set; }
    [Required(ErrorMessage = "El campo {0} es obligatorio")]
    public int numeroDeFichas { get; set; }
    [Required(ErrorMessage = "El campo {0} es obligatorio")]
    public int idPuestoEmpleado {get; set; }

    [Required(ErrorMessage = "El campo {0} es obligatorio")]
    [StringLength(100, ErrorMessage = "El campo {0} no puede taner mas de {1} caracteres")]
    [Column("nombreEmpleado", TypeName = "varchar(100)")]
    public string nombreEmpleado{ get; set; } = null!;

    [Required(ErrorMessage = "El campo {0} es obligatorio")]
    [StringLength(100, ErrorMessage = "El campo {0} no puede tener mas de {1} caracteres")]
    [Column("telefonoEmpleado", TypeName ="varchar(20)")]
    public string telefonoEmpleado{ get; set; } = null!;

    [Required(ErrorMessage = "El campo {0} es obligatorio")]
    [EmailAddress(ErrorMessage = "El campo {0} no es un email valido")]
    [DataType(DataType.EmailAddress)]
    [Column("telefonoEmpleado",TypeName = "varchar(50)")]
    public string correoEmpleado{ get; set; } = null!;

    [Required(ErrorMessage = "El campo {0} es obligatorio")]
    public int idDepartamento{ get; set; }
    [Required(ErrorMessage = "El campo {0} es obligatorio")]
    public int idAreaEmpleado{ get; set; }
    [Required(ErrorMessage = "El campo {0} es obligatorio")]
    public int idHUB{ get; set; }
}   