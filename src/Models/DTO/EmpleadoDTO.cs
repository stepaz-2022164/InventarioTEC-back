namespace GestorInventario.src.Models.DTO
{
    public class EmpleadoDTO
    {
        public int numeroDeFicha { get; set; }
        public string nombreEmpleado { get; set; } = null!;
        public string telefonoEmpleado { get; set; } = null!;
        public string correoEmpleado { get; set; } = null!;
        public int idDepartamentoEmpleado {get; set;}
        public int idAreaEmpleado { get; set; }
        public int idPuestoEmpleado { get; set; }
        public int idSede { get; set; }
    }
}