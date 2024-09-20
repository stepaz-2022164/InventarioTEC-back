namespace GestorInventario.src.Models.DTO
{
    public class AreaEmpleadoDTO
    {
        public string nombreAreaEmpleado {get; set;} = null!;
        public string descripcionAreaEmpleado {get; set;} = null!;
        public int idDepartamentoEmpleado {get; set;}
    }
}