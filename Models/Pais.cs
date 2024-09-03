using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Pais
{
    [Key]
    [Required(ErrorMessage = "El campo {0} es obligatorio")]
    [Column("idPais", TypeName = "int")] 
    public int idPais { get; set; }

    [Required(ErrorMessage = "El campo {0} es obligatorio")]
    [StringLength(100, ErrorMessage = "El campo {0} debe tener un máximo de {1} caracteres")]
    [Column("nombrePais", TypeName = "varchar(100)")]
    public string nombrePais { get; set; } = null!;  
}