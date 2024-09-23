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
    public class EquipoController : ControllerBase
    {
        private readonly InventarioContext _context;
        public EquipoController(InventarioContext context)
        {
            _context = context;
        }

        [ValidateJWT]
        [HttpGet]
        [Route("getEquipos")]
        public async Task<ActionResult<IEnumerable<Equipo>>> GetEquipos(){
            try
            {
                var equipos = await _context.Equipos.Where(eq => eq.estado == 1).ToListAsync();
                if (equipos.Count() == 0)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "No se encontraron registros");
                }
                return Ok(equipos);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener los registros");
            }
        }

        [ValidateJWT]
        [HttpGet]
        [Route("getEquipoById")]
        public async Task<ActionResult<Equipo>> GetEquipoById(int id)
        {
            try
            {
                var equipo = await _context.Equipos.FindAsync(id);
                if (equipo == null || equipo.estado == 0)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "No se encontró el registro");
                }
                return Ok(equipo);
            }
            catch (System.Exception e)
            {
                Console.Error.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener el registro");
            }
        }

        [ValidateJWT]
        [HttpGet]
        [Route("getEquipoBySerialNumber")]
        public async Task<ActionResult<Equipo>> GetEquipoBySerialNumber(string serialNumber)
        {
            try
            {
                var equipo = await _context.Equipos.FirstOrDefaultAsync(eq => eq.estado == 1 && eq.numeroDeSerie == serialNumber);
                if (equipo == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "No se encontró el registro");
                }
                return Ok(equipo);
            }
            catch (System.Exception e)
            {
                Console.Error.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener el registro");
            }
        }

        [ValidateJWT]
        [HttpPost]
        [Route("createEquipo")]
        public async Task<ActionResult<Equipo>> CreateEquipo([FromBody] EquipoDTO equipoDTO)
        {
            try
            {
                var equipoExistente = await _context.Equipos.FirstOrDefaultAsync(eq => eq.estado == 1 && eq.numeroDeSerie == equipoDTO.numeroDeSerie);
                if (equipoExistente!= null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "El equipo ya existe");
                }

                var tipoDeEquipoExistente = await _context.TiposDeEquipos.FirstOrDefaultAsync(te => te.estado == 1 && te.idTipoDeEquipo == equipoDTO.idTipoDeEquipo);
                if (tipoDeEquipoExistente == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Tipo de equipo no encontrado");
                }

                var equipo = new Equipo{
                    numeroDeSerie = equipoDTO.numeroDeSerie,
                    estadoEquipo = equipoDTO.estadoEquipo,
                    fechaDeIngreso = equipoDTO.fechaDeIngreso,
                    idTipoDeEquipo = equipoDTO.idTipoDeEquipo,
                    estado = 1
                };
                await _context.Equipos.AddAsync(equipo);
                await _context.SaveChangesAsync();
                return Ok("Equipo creado correctamente");
            }
            catch (System.Exception e)
            {
                Console.Error.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al crear el registro");
            }
        }

        [ValidateJWT]
        [HttpPut]
        [Route("updateEquipo")]
        public async Task<ActionResult<Equipo>> UpdateEquipo(int id,[FromBody] EquipoUpdateDTO equipoUpdateDTO)
        {
            try
            {
                var equipoExistente = await _context.Equipos.FirstOrDefaultAsync(eq => eq.estado == 1 && eq.idEquipo == id);
                if (equipoExistente == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Registro no encontrado");
                }

                if (!string.IsNullOrEmpty(equipoUpdateDTO.numeroDeSerie))
                {
                    equipoExistente.numeroDeSerie = equipoUpdateDTO.numeroDeSerie;
                }

                if (!string.IsNullOrEmpty(equipoUpdateDTO.estadoEquipo))
                {
                    equipoExistente.estadoEquipo = equipoUpdateDTO.estadoEquipo;
                }

                if (equipoUpdateDTO.fechaDeIngreso.HasValue)
                {
                    equipoExistente!.fechaDeIngreso = equipoUpdateDTO.fechaDeIngreso.Value;
                }

                await _context.SaveChangesAsync();
                return Ok("Equipo actualizado correctamente");
            }
            catch (System.Exception e) 
            {
                Console.Error.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al actualizar el registro");
            }
        }

        [ValidateJWT]
        [HttpPut]
        [Route("deleteEquipo")]
        public async Task<ActionResult> DeleteEquipo(int id)
        {
            try
            {
                var equipoExistente = await _context.Equipos.FirstOrDefaultAsync(eq => eq.estado == 1 && eq.idEquipo == id);
                if (equipoExistente == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Registro no encontrado");
                }
                equipoExistente.estado = 0;
                await _context.SaveChangesAsync();
                return Ok("Equipo eliminado correctamente");
            }
            catch (System.Exception e)
            {
                Console.Error.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al eliminar el registro");
            }
        }
    }
}