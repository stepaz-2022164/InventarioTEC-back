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
    public class SedeController : ControllerBase
    {
        private readonly InventarioContext _context;
        public SedeController(InventarioContext context)
        {
            _context = context;
        }

        [ValidateJWT]
        [HttpGet]
        [Route("getSedes")]
        public async Task<ActionResult<IEnumerable<Sede>>> GetSedes([FromQuery] int pagina = 1, [FromQuery] int numeroPaginas = 10)
        {
            try
            {
                var totalRecords = await _context.Sedes.CountAsync(s => s.estado == 1);
                var sedes = await _context.Sedes
                .Where(s => s.estado == 1)
                .Include(s => s.Pais)
                .Include(s => s.Region)
                .Include(s => s.HUB)
                .Skip((pagina - 1) * numeroPaginas)
                .Take(numeroPaginas)
                .Select(s => new {
                    id = s.idSede,
                    nombre = s.nombreSede,
                    s.direccionSede,
                    nombrePais = s.Pais.nombrePais,
                    nombreRegion = s.Region.nombreRegion,
                    nombreHUB = s.HUB.nombreHUB
                })
                .ToListAsync();
                if (sedes.Count() == 0)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "No se encontraron registros");
                }
                return Ok(new {data = sedes, totalRecords});
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener los registros");
            }
        }

        [ValidateJWT]
        [HttpGet]
        [Route("getSedeById")]
        public async Task<ActionResult<Sede>> GetSede(int id)
        {
            try
            {
                var sede = await _context.Sedes.FindAsync(id);
                if (sede == null || sede.estado == 0)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "No se encontro el registro");
                }
                return Ok(sede);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener los registros");
            }
        }

        [ValidateJWT]
        [HttpGet]
        [Route("getSedeByName")]
        public async Task<ActionResult<Sede>> GetSedeByName(string name)
        {
            try
            {
                var sede = await _context.Sedes
                .Where(s => s.estado == 1 && s.nombreSede.Contains(name))
                .Include(s => s.Pais)
                .Include(s => s.Region)
                .Include(s => s.HUB)
                .Select(s => new {
                    id = s.idSede,
                    nombre = s.nombreSede,
                    s.direccionSede,
                    nombrePais = s.Pais.nombrePais,
                    nombreRegion = s.Region.nombreRegion,
                    nombreHUB = s.HUB.nombreHUB
                })
                .ToListAsync();
                if (sede == null || sede.Count == 0)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "No se encontraron registros");
                }
                return Ok(sede);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener los registros");
            }
        }

        [ValidateJWT]
        [HttpGet]
        [Route("getSedesAll")]
        public async Task<ActionResult<IEnumerable<Sede>>> GetSedesAll()
        {
            try
            {
                var sedes = await _context.Sedes
                .Where(s => s.estado == 1 )
                .Include(s => s.Pais)
                .Include(s => s.Region)
                .Include(s => s.HUB)
                .Select(s => new {
                    id = s.idSede,
                    nombre = s.nombreSede,
                    s.direccionSede,
                    nombrePais = s.Pais.nombrePais,
                    nombreRegion = s.Region.nombreRegion,
                    nombreHUB = s.HUB.nombreHUB
                })
                .ToListAsync();

                if (sedes.Count() == 0)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "No se encontraron registros");
                }

                return Ok(new{data = sedes});
            }
            catch (System.Exception e)
            {
                Console.Error.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener los registros");
            }
        }

        [ValidateJWT]
        [HttpPost]
        [Route("createSede")]
        public async Task<ActionResult<Sede>> CreateSede([FromBody] SedeDTO sedeDTO)
        {
            try
            {
                if (!ModelState.IsValid!)
                {
                    return BadRequest(ModelState);
                }

                var sedeExistente = await _context.Sedes.FirstOrDefaultAsync(s => s.nombreSede == sedeDTO.nombreSede);
                if (sedeExistente != null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Sede ya existente");
                }

                var pais = await _context.Paises.FindAsync(sedeDTO.idPais);
                var region = await _context.Regiones.FindAsync(sedeDTO.idRegion);
                var hub = await _context.HUB.FindAsync(sedeDTO.idHUB);

                if (pais == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "No se encontro el pais");
                }
                else if (region == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "No se encontro la región");
                }
                else if (hub == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "No se encontro el HUB");
                }

                var sede = new Sede
                {
                    nombreSede = sedeDTO.nombreSede,
                    direccionSede = sedeDTO.direccionSede,
                    idPais = sedeDTO.idPais,
                    idRegion = sedeDTO.idRegion,
                    idHUB = sedeDTO.idHUB
                };

                await _context.Sedes.AddAsync(sede);
                await _context.SaveChangesAsync();
                return Ok();

            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al crear el registro");
            }
        }

        [ValidateJWT]
        [HttpPut]
        [Route("updateSede")]
        public async Task<ActionResult<Sede>> UpdateSede(int id, [FromBody] SedeUpdateDTO sedeDTO)
        {
            try
            {
                var sedeExistente = await _context.Sedes.FindAsync(id);
                if (sedeExistente == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Registro no encontrado");
                }

                if (!string.IsNullOrEmpty(sedeDTO.nombre))
                {
                    sedeExistente!.nombreSede = sedeDTO.nombre;
                }

                if (!string.IsNullOrEmpty(sedeDTO.direccionSede))
                {
                    sedeExistente!.direccionSede = sedeDTO.direccionSede;
                }
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al actualizar el registro");
            }
        }

        [ValidateJWT]
        [HttpPut]
        [Route("deleteSede")]
        public async Task<ActionResult<Sede>> DeleteSede(int id)
        {
            try
            {
                var sedeExistente = await _context.Sedes.FirstOrDefaultAsync(s => s.idSede == id && s.estado == 1);
                if (sedeExistente == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Registro no encontrado");
                }

                var empleados = await _context.Empleados.Where(e => e.idSede == sedeExistente.idSede && e.estado == 1).ToListAsync();
                if (empleados.Count() != 0)
                {
                    return StatusCode(StatusCodes.Status409Conflict, "No se puede eliminar la sede porque hay registros dependientes.");
                }
                sedeExistente.estado = 0;
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al eliminar el registro");
            }
        }
    }
}