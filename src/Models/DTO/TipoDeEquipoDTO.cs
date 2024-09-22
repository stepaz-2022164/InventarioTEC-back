namespace GestorInventario.src.Models.DTO
{
    public class TipoDeEquipoDTO
    {
        public string nombreTipoDeEquipo { get; set; } = null!;
        public string descripcionTipoDeEquipo { get; set; } = null!;
        public int stock { get; set; }
        public int idMarca { get; set; }
    }
}