using GestorInventario.src.Middlewares;
using GestorInventario.src.Models.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestorInventario.src.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HUBController : ControllerBase
    {
        private readonly InventarioContext _context;
        public HUBController(InventarioContext context){
            _context = context;
        }

        [ValidateJWT]
        [HttpGet]
        [Route("getHUBS")]
        public async Task<ActionResult<IEnumerable<HUB>>> GetHUBS([FromQuery] int pagina = 1, [FromQuery] int numeroPaginas = 10){
            try
            {
                var totalRecords = await _context.HUB.CountAsync();
                var hubs = await _context.HUB
                .Include(h => h.Pais)
                .Include(h => h.Region)
                .Skip((pagina - 1) * numeroPaginas)
                .Take(numeroPaginas)
                .Select(h => new {
                    id = h.idHUB,
                    nombre = h.nombreHUB,
                    nombrePais = h.Pais.nombrePais,
                    nombreRegion = h.Region.nombreRegion
                })
                .ToListAsync();
                if (hubs.Count() == 0)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "No se encontraron registros");
                }
                return Ok(new {data = hubs, totalRecords});
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener los registros");
            }
        }

        [ValidateJWT]
        [HttpGet]
        [Route("getHUB")]
        public async Task<ActionResult<HUB>> GetHUB(int id){
            try
            {
                var hub = await _context.HUB.FindAsync(id);
                if (hub == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Registro no encontrado");
                }
                return Ok(hub);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener los registros");
            }
        }

        [ValidateJWT]
        [HttpGet]
        [Route("getHUBSByName")]
        public async Task<ActionResult<HUB>> GeyHUBSByName(string name){
            try
            {
                var hubs = await _context.HUB
                .Where(h => h.nombreHUB.Contains(name))
                .Include(h => h.Pais)
                .Include(h => h.Region)
                .Select(h => new {
                    id = h.idHUB,
                    nombre = h.nombreHUB,
                    nombrePais = h.Pais.nombrePais,
                    nombreRegion = h.Region.nombreRegion
                })
                .ToListAsync();
                if (hubs.Count() == 0)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "No se encontraron registros");
                }
                return Ok(hubs);
            }
            catch (System.Exception e)
            {
                Console.Error.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener los registros");
            }
        }
    }
}