namespace GestorInventario.src.Models.DTOUpdate
{
    public class EquipoUpdateDTO
    {
        public string? nombre { get; set; }
        public string? estadoEquipo { get; set; }
        public DateOnly? fechaDeIngreso { get; set;}
    }
}