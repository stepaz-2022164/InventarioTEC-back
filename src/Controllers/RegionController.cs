using GestorInventario.src.Middlewares;
using GestorInventario.src.Models.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestorInventario.src.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionController : ControllerBase
    {
        private readonly InventarioContext _context;
        public RegionController(InventarioContext context){
            _context = context;
        }

        [ValidateJWT]
        [HttpGet]
        [Route("getRegiones")]
        public async Task<ActionResult<IEnumerable<Region>>> GetRegiones([FromQuery] int pagina = 1, [FromQuery] int numeroPaginas = 10) {
            try
            {
                var totalRecords = await _context.Regiones.CountAsync();
                var regiones = await _context.Regiones
                .Include(re => re.Pais)
                .Skip((pagina - 1) * numeroPaginas)
                .Take(numeroPaginas)
                .Select(re => new {
                    id = re.idRegion,
                    nombre = re.nombreRegion,
                    nombrePais = re.Pais.nombrePais
                })
                .ToListAsync();
                if (regiones.Count() == 0)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "No se encontraron registros");
                }
                return Ok(new {data = regiones, totalRecords});
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener los registros");
            }
        }

        [ValidateJWT]
        [HttpGet]
        [Route("getRegion")]
        public async Task<ActionResult<Region>> GetRegion(int id) {
            try
            {
                var region = await _context.Regiones.FindAsync(id);
                if (region == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Registro no encontrado");
                }
                return Ok(region);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener los registros");
            }
        }
    }
}