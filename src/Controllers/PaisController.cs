using GestorInventario.src.Middlewares;
using GestorInventario.src.Models.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestorInventario.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaisController : ControllerBase
    {
        private readonly InventarioContext _context;

        public PaisController(InventarioContext context)
        {
            _context = context;
        }

        [ValidateJWT]
        [HttpGet]
        [Route("getPaises")]
        public async Task<ActionResult<IEnumerable<Pais>>> GetPaises([FromQuery] int pagina = 1,[FromQuery] int numeroPaginas = 10)
        {
            try
            {
                var totalRecords = await _context.Paises.CountAsync();
                var paises = await _context.Paises
                .Skip((pagina - 1) * numeroPaginas)
                .Take(numeroPaginas)
                .Select(p => new {
                    id = p.idPais,
                    nombre = p.nombrePais
                })
                .ToListAsync();

                if (paises.Count() == 0)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "No se encontraron registros");
                }
                return Ok(new {data = paises, totalRecords});
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener los registros");
            }
        }

        [ValidateJWT]
        [HttpGet]
        [Route("getPais")]
        public async Task<ActionResult<Pais>> GetPais(int id)
        {
            try
            {
                var pais = await _context.Paises.FindAsync(id);
                if (pais == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "No se encontro el registro");
                }
                return Ok(pais);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener los registros");
            }
        }
    }
}