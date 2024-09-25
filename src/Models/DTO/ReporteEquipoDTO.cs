namespace GestorInventario.src.Models.DTO
{
    public class ReporteEquipoDTO
    {
        public DateOnly fechaReporte {get; set;}
        public string descripcionReporteEquipo {get; set;} = null!;
        public int idEquipo {get; set;}
    }
}