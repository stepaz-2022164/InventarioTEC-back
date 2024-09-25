namespace GestorInventario.src.Models.DTO
{
    public class EquipoDTO
    {
        public string numeroDeSerie { get; set; } = null!;
        public string estadoEquipo { get; set; } = null!;
        public DateOnly fechaDeIngreso { get; set;}
        public int idTipoDeEquipo { get; set; }
    }
}