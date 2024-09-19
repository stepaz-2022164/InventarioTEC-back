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
        public async Task<ActionResult<IEnumerable<Sede>>> GetSedes()
        {
            try
            {
                var sedes = await _context.Sedes.ToListAsync();
                if (sedes.Count() == 0)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "No se encontraron registros");
                }
                return Ok(sedes);
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
                if (sede == null)
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
                var sede = await _context.Sedes.Where(s => s.nombreSede.Contains(name)).ToListAsync();
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
                return Ok("Sede creada correctamente");

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
                var sedeExistente = await _context.Sedes.FindAsync(1);
                if (sedeExistente == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Registro no encontrado");
                }

                if (!string.IsNullOrEmpty(sedeDTO.nombreSede))
                {
                    sedeExistente!.nombreSede = sedeDTO.nombreSede;
                }

                if (!string.IsNullOrEmpty(sedeDTO.direccionSede))
                {
                    sedeExistente!.direccionSede = sedeDTO.direccionSede;
                }
                sedeExistente!.nombreSede = sedeDTO.nombreSede;
                await _context.SaveChangesAsync();
                return Ok("Sede actualizada correctamente");
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al actualizar el registro");
            }
        }
    }
}