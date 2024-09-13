using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class PropietarioEquipo
{
    [Key]
    [Required(ErrorMessage = "El parámetro {0} es obligatorio")]
    [Column("idPropietarioEquipo", TypeName = "int")]
    public int idPropietarioEquipopacion {get; set;}

    [Required(ErrorMessage = "El parámetro {0} es obligatorio")]
    [Column("idEmpleado", TypeName = "int")]
    public int idEmpleado { get; set; }

    [Required(ErrorMessage = "El parámetro {0} es obligatorio")]
    [Column("idEquipo", TypeName = "int")]
    public int idEquipo { get; set; }

    [Required(ErrorMessage = "El parámetro {0} es obligatorio")]
    [DataType(DataType.Date)]
    [Column("fechaDeEntrega", TypeName = "date")]
    public System.DateOnly fechaDeEntrega { get; set; }

    [ForeignKey("idEmpleado")]
    public Empleado Empleado { get; set;} = null!;

    [ForeignKey("idEquipo")]
    public Equipo Equipo { get; set;} = null!;
}