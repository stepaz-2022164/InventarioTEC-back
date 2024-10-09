using GestorInventario.src.Middlewares;
using GestorInventario.src.Models.Contexts;
using GestorInventario.src.Models.DTO;
using GestorInventario.src.Models.DTOUpdate;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestorInventario.src.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReporteEquipoController : ControllerBase
    {
        private readonly InventarioContext _context;
        public ReporteEquipoController(InventarioContext context)
        {
            _context = context;
        }

        [ValidateJWT]
        [HttpGet]
        [Route("getReporteEquipos")]
        public async Task<ActionResult<IEnumerable<ReporteEquipo>>> GetReporteEquipos([FromQuery] int pagina = 1, [FromQuery] int numeroPaginas = 10){
            try
            {
                var totalRecords = await _context.ReporteEquipos.CountAsync(r => r.estado == 1);
                var reportesEquipos = await _context.ReporteEquipos
                .Where(r => r.estado == 1)
                .Include(r => r.Equipo)
                .Skip((pagina - 1) * numeroPaginas)
                .Take(numeroPaginas)
                .Select(r => new {
                    r.idReporteEquipo,
                    r.fechaReporte,
                    r.descripcionReporteEquipo,
                    numeroDeSerie = r.Equipo.numeroDeSerie,
                })
                .ToListAsync();

                if (reportesEquipos.Count() == 0)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "No se encontraron registros");
                }
                return Ok(new {data = reportesEquipos, totalRecords});
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener los registros");
            }
        }

        [ValidateJWT]
        [HttpGet]
        [Route("getReporteEquipoById")]
        public async Task<ActionResult<ReporteEquipo>> GetReporteEquipoById(int id){
            try
            {
                var reporteEquipo = await _context.ReporteEquipos.FindAsync(id);
                if (reporteEquipo == null || reporteEquipo.estado == 0)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "No se encontró el registro");
                }
                return Ok(reporteEquipo);
            }
            catch (System.Exception e)
            {
                Console.Error.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener el registro");
            }
        }

        [ValidateJWT]
        [HttpGet]
        [Route("getReporteEquipoBySerialNumber")]
        public async Task<ActionResult<ReporteEquipo>> GetReporteEquipoBySerialNumber(string serialNumber){
            try
            {
                var reporteEquipo = await _context.ReporteEquipos
                .Include(r => r.Equipo)
                .Where(r => r.estado == 1 && r.Equipo.numeroDeSerie.Contains(serialNumber))
                .Select(r => new {
                    r.idReporteEquipo,
                    r.fechaReporte,
                    r.descripcionReporteEquipo,
                    numeroDeSerie = r.Equipo.numeroDeSerie})
                .ToListAsync();

                if (reporteEquipo.Count() == 0)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "No se encontró el registro");
                }
                return Ok(reporteEquipo);
            }
            catch (System.Exception e)
            {
                Console.Error.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener el registro");
            }
        }

        [ValidateJWT]
        [HttpPost]
        [Route("createReporteEquipo")]
        public async Task<ActionResult<ReporteEquipo>> CreateReporteEquipo([FromBody] ReporteEquipoDTO reporteEquipoDTO){
            try
            {
                if (!ModelState.IsValid!)
                {
                    return BadRequest(ModelState);
                }

                var equipoExistente = await _context.Equipos.FirstOrDefaultAsync(eq => eq.idEquipo == reporteEquipoDTO.idEquipo && eq.estado == 1);
                if (equipoExistente == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Equipo no encontrado");
                }

                var reporteEquipo = new ReporteEquipo{
                    fechaReporte = reporteEquipoDTO.fechaReporte,
                    descripcionReporteEquipo = reporteEquipoDTO.descripcionReporteEquipo,
                    idEquipo = reporteEquipoDTO.idEquipo,
                    estado = 1
                };
                
                await _context.ReporteEquipos.AddAsync(reporteEquipo);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (System.Exception e)
            {
                Console.Error.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al crear el registro");
            }
        }

        [ValidateJWT]
        [HttpPut]
        [Route("updateReporteEquipo")]
        public async Task<ActionResult<ReporteEquipo>> UpdateReporteEquipo(int id,[FromBody] ReporteEquipoUpdateDTO reporteEquipoUpdateDTO){
            try
            {
                var reporteEquipoExistente = await _context.ReporteEquipos.FirstOrDefaultAsync(r => r.idReporteEquipo == id && r.estado == 1);
                if (reporteEquipoExistente == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Registro no encontrado");
                }

                if (reporteEquipoUpdateDTO.fechaReporte.HasValue)
                {
                    reporteEquipoExistente.fechaReporte = reporteEquipoUpdateDTO.fechaReporte.Value;
                }
                
                if (!string.IsNullOrEmpty(reporteEquipoUpdateDTO.descripcionReporteEquipo))
                {
                    reporteEquipoExistente.descripcionReporteEquipo = reporteEquipoUpdateDTO.descripcionReporteEquipo;
                }
                await _context.SaveChangesAsync();
                return Ok("Reporte de equipo actualizado correctamente");
            }
            catch (System.Exception e)
            {
                Console.Error.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al actualizar el registro");
            }
        }

        [ValidateJWT]
        [HttpPut]
        [Route("deleteReporteEquipo")]
        public async Task<ActionResult<ReporteEquipo>> DeleteReporteEquipo(int id)
        {
            try
            {
                var reporteEquipoExistente = await _context.ReporteEquipos.FirstOrDefaultAsync(r => r.idReporteEquipo == id && r.estado == 1);
                if (reporteEquipoExistente == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Registro no encontrado");
                }
                reporteEquipoExistente.estado = 0;
                await _context.SaveChangesAsync();
                return StatusCode(StatusCodes.Status200OK);
            }
            catch (System.Exception e)
            {
                Console.Error.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al eliminar el registro");
            }
        }
    }
}