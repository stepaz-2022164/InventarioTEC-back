using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Usuario
{
    [Key]
    [Required(ErrorMessage = "El parámetro {0} es obligatorio")]
    [Column("idUsuario", TypeName = "int")]
    public int idUsuario { get; set; }

    [Required(ErrorMessage = "El parámetro {0} es obligatorio")]
    [StringLength(10, MinimumLength = 5, ErrorMessage = "El parámetro {0} debe tener entre {2} y {1} carácteres")]
    [Column("usuario", TypeName = "varchar(10)")]
    public string usuario { get; set; } = null!;

    [Required(ErrorMessage = "El parámetro {0} es obligatorio")]
    [StringLength(10, MinimumLength = 5, ErrorMessage = "El parámetro {0} debe tener entre {2} y {1} carácteres")]
    [Column("pass", TypeName = "varchar(10)")]
    public string pass { get; set; } = null!;

}