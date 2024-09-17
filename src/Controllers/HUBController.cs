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
        public async Task<ActionResult<IEnumerable<HUB>>> GetHUBS(){
            try
            {
                var hubs = await _context.HUB.ToListAsync();
                if (hubs.Count() == 0)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "No se encontraron registros");
                }
                return Ok(hubs);
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
    }
}