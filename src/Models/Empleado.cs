using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Empleado
{
    [Key]
    [Required(ErrorMessage = "El parámetro {0} es obligatorio")]
    [Column("idEmpleado", TypeName = "int")]
    public int idEmpleado { get; set; }

    [Required(ErrorMessage = "El campo {0} es obligatorio")]
    [Column("numeroDeFicha", TypeName = "int")]
    public int numeroDeFicha { get; set; }

    [Required(ErrorMessage = "El campo {0} es obligatorio")]
    [StringLength(100, ErrorMessage = "El campo {0} no puede taner mas de {1} caracteres")]
    [Column("nombreEmpleado", TypeName = "varchar(100)")]
    public string nombreEmpleado{ get; set; } = null!;

    [Required(ErrorMessage = "El campo {0} es obligatorio")]
    [StringLength(100, ErrorMessage = "El campo {0} no puede tener mas de {1} caracteres")]
    [DataType(DataType.PhoneNumber)]
    [Column("telefonoEmpleado", TypeName ="varchar(20)")]
    public string telefonoEmpleado{ get; set; } = null!;

    [Required(ErrorMessage = "El campo {0} es obligatorio")]
    [EmailAddress(ErrorMessage = "El campo {0} no es un email valido")]
    [DataType(DataType.EmailAddress)]
    [Column("correoEmpleado",TypeName = "varchar(50)")]
    public string correoEmpleado{ get; set; } = null!;

    [Required(ErrorMessage = "El campo {0} es obligatorio")]
    [Column("idDepartamentoEmpleado", TypeName = "int")]
    public int idDepartamentoEmpleado{ get; set; }

    [Required(ErrorMessage = "El campo {0} es obligatorio")]
    [Column("idAreaEmpleado", TypeName = "int")]
    public int idAreaEmpleado{ get; set; }

    [Required(ErrorMessage = "El campo {0} es obligatorio")]
    [Column("idPuestoEmpleado", TypeName = "int")]
    public int idPuestoEmpleado {get; set; }

    [Required(ErrorMessage = "El campo {0} es obligatorio")]
    [Column("idSede", TypeName = "int")]
    public int idSede{ get; set; }

    [Column("estado", TypeName = "int")]
    public int estado {get; set;} = 1;

    [ForeignKey("idPuestoEmpleado")]
    public PuestoEmpleado PuestoEmpleado {get; set;} = null!;

    [ForeignKey("idDepartamentoEmpleado")]
    public DepartamentoEmpleado DepartamentoEmpleado {get; set;} = null!;

    [ForeignKey("idAreaEmpleado")]
    public AreaEmpleado AreaEmpleado {get; set;} = null!;

    [ForeignKey("idSede")]
    public Sede Sede {get; set;} = null!;
}   