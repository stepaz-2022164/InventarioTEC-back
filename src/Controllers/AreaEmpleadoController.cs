using GestorInventario.src.Middlewares;
using GestorInventario.src.Models.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestorInventario.src.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AreaEmpleadoController : ControllerBase
    {
        private readonly InventarioContext _context;
        public AreaEmpleadoController (InventarioContext context){
            _context = context;
        }

        [ValidateJWT]
        [HttpGet]
        [Route("getAreasEmpleados")]
        public async Task<ActionResult<IEnumerable<AreaEmpleado>>> GetAreasEmpleados([FromQuery] int pagina = 1,[FromQuery] int numeroPaginas = 10){
            try
            {
                var totalRecords = await _context.AreasEmpleados.CountAsync(ae => ae.estado == 1);
                var areasEmpleados = await _context.AreasEmpleados
                .Where(ae => ae.estado == 1)
                .Include(ae => ae.DepartamentoEmpleado)
                .Skip((pagina - 1) * numeroPaginas)
                .Take(numeroPaginas)
                .Select( ae => new {
                    id = ae.idAreaEmpleado,
                    nombre = ae.nombreAreaEmpleado,
                    ae.descripcionAreaEmpleado,
                    nombreDepartamentoEmpleado = ae.DepartamentoEmpleado.nombreDepartamentoEmpleado
                })
                .ToListAsync();

                if (areasEmpleados.Count() == 0)
                {
                    return NotFound("No se encontraron registros");
                }
                return Ok(new {data = areasEmpleados, totalRecords});
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener los registros");
            }
        }

        [ValidateJWT]
        [HttpGet]
        [Route("getAreaEmpleadoById")]
        public async Task<ActionResult<AreaEmpleado>> GetAreaEmpleadoById(int id){
            try
            {
                var areaEmpleado = await _context.AreasEmpleados.FindAsync(id);
                if (areaEmpleado == null || areaEmpleado.estado == 0)
                {
                    return NotFound("No se encontró el registro");
                }
                return Ok(areaEmpleado);
            }
            catch (System.Exception e)
            {
                Console.Error.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener los registros");
            }
        }

        [ValidateJWT]
        [HttpGet]
        [Route("getAreaEmpleadoByName")]
        public async Task<ActionResult<AreaEmpleado>> GetAreaEmpleadoByName(string name){
            try
            {
                var areasEmpleados = await _context.AreasEmpleados
                .Where(ae => ae.estado == 1 && ae.nombreAreaEmpleado
                .Contains(name))
                .Include(ae => ae.DepartamentoEmpleado)
                .Select(ae => new {
                    id = ae.idAreaEmpleado,
                    nombre = ae.nombreAreaEmpleado,
                    ae.descripcionAreaEmpleado,
                    nombreDepartamentoEmpleado = ae.DepartamentoEmpleado.nombreDepartamentoEmpleado
                })
                .ToListAsync();
                if (areasEmpleados == null || areasEmpleados.Count == 0)
                {
                    return NotFound("No se encontraron registros");
                }
                return Ok(areasEmpleados);
            }
            catch (System.Exception e)
            {
                Console.Error.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener los registros");
            }
        }
    }
}