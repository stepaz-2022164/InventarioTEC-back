namespace GestorInventario.src.Models.DTO
{
    public class SedeDTO
    {
    public string nombreSede { get; set; } = null!;
    public string direccionSede { get; set; } = null!;
    public int idPais { get; set; }
    public int idRegion { get; set; }
    public int idHUB { get; set; }
    }
}