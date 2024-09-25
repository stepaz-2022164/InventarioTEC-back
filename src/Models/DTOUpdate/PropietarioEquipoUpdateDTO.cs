namespace GestorInventario.src.Models.DTOUpdate
{
    public class PropietarioEquipoUpdateDTO
    {
        public int? idEmpleado {get; set;}
        public int? idEquipo {get; set;}
        public DateOnly? fechaDeEntrega {get; set;}
    }
}