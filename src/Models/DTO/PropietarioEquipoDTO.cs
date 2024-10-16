namespace GestorInventario.src.Models.DTO
{
    public class PropietarioEquipoDTO
    {
        public int idEmpleado {get; set;}
        public int idTipoDeEquipo {get; set;}
        public int idEquipo {get; set;}
        public int idHUB {get; set;}
        public DateOnly fechaDeEntrega {get; set;}
    }
}