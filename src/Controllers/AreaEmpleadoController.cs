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
        public async Task<ActionResult<IEnumerable<AreaEmpleado>>> GetAreasEmpleados(){
            try
            {
                var areasEmpleados = await _context.AreasEmpleados.Where(ae => ae.estado == 1).ToListAsync();
                if (areasEmpleados.Count() == 0)
                {
                    return NotFound("No se encontraron registros");
                }
                return Ok(areasEmpleados);
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
    }
}