using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Region{
    [Key]
    [Required(ErrorMessage = "El parametro {0} es obligatorio")]
    [Column("idRegion", TypeName = "int")]
    public int idRegion { get; set; }

    [Required(ErrorMessage = "El parametro {0} es obligatorio")]
    [StringLength(10, ErrorMessage = "El parametro {0} solo puede contener {1} carateres")]
    [Column("nombreRegion", TypeName = "varchar(10)")]
    public string nombreRegion { get; set; } = null!;

    [Required(ErrorMessage = "El parametro {0} es obligatorio")]
    [Column("idRegion", TypeName = "int")]
    public int idPais { get; set; }

    [ForeignKey("idPais")]
    public Pais Pais { get; set; } = null!;
}