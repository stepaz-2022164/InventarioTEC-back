namespace GestorInventario.src.Models.DTO
{
    public class PuestoEmpleadoDTO
    {
        public string nombrePuestoEmpleado {get; set;} = null!;
        public string descripcionPuestoEmpleado {get; set;} = null!;
        public int idAreaEmpleado {get; set;}
    }
}