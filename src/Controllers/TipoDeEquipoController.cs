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
    public class TipoDeEquipoController : ControllerBase
    {
        private readonly InventarioContext _context;
        public TipoDeEquipoController(InventarioContext context)
        {
            _context = context;
        }

        [ValidateJWT]
        [HttpGet]
        [Route("getTiposDeEquipos")]
        public async Task<ActionResult<IEnumerable<TipoDeEquipo>>> GetTiposDeEquipos(){
            try
            {
                var tiposDeEquipos = await _context.TiposDeEquipos.Where(te => te.estado == 1).ToListAsync();
                if (tiposDeEquipos.Count() == 0)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "No se encontraron registros");
                }
                return Ok(tiposDeEquipos);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener los registros");
            }
        }

        [ValidateJWT]
        [HttpGet]
        [Route("getTipoDeEquipoById")]
        public async Task<ActionResult<TipoDeEquipo>> GetTipoDeEquipoById(int id){
            try
            {
                var tipoDeEquipo = await _context.TiposDeEquipos.FindAsync(id);
                if (tipoDeEquipo == null || tipoDeEquipo.estado == 0)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "No se encontró el registro");
                }
                return Ok(tipoDeEquipo);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener los registros");
            }
        }

        [ValidateJWT]
        [HttpGet]
        [Route("getTipoDeEquipoByName")]
        public async Task<ActionResult<TipoDeEquipo>> GetTipoDeEquipoByName(string nombre){
            try
            {
                var tipoDeEquipo = await _context.TiposDeEquipos.Where(te => te.estado == 1 && te.nombreTipoDeEquipo.Contains(nombre)).ToListAsync();
                if (tipoDeEquipo == null || tipoDeEquipo.Count == 0)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "No se encontraron registros");
                }
                return Ok(tipoDeEquipo);
            }
            catch (System.Exception e) 
            {
                Console.Error.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener los registros");
            }
        }

        [ValidateJWT]
        [HttpPost]
        [Route("createTipoDeEquipo")]
        public async Task<ActionResult<TipoDeEquipo>> CreateTipoDeEquipo([FromBody] TipoDeEquipoDTO tipoDeEquipoDTO)
        {
            try
            {
                var tipoDeEquipoExistente = await _context.TiposDeEquipos.FirstOrDefaultAsync(te => te.nombreTipoDeEquipo == tipoDeEquipoDTO.nombreTipoDeEquipo && te.estado == 1);
                if (tipoDeEquipoExistente!= null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "El tipo de equipo ya existe");
                }
                var tipoDeEquipo = new TipoDeEquipo{
                    nombreTipoDeEquipo = tipoDeEquipoDTO.nombreTipoDeEquipo,
                    descripcionTipoDeEquipo = tipoDeEquipoDTO.descripcionTipoDeEquipo,
                    stock = tipoDeEquipoDTO.stock,
                    idMarca = tipoDeEquipoDTO.idMarca,
                    estado = 1
                };
                await _context.TiposDeEquipos.AddAsync(tipoDeEquipo);
                await _context.SaveChangesAsync();
                return Ok("Tipo de equipo creado correctamente");
            }
            catch (System.Exception e)
            {
                Console.Error.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al crear el registro");
            }
        }

        [ValidateJWT]
        [HttpPut]
        [Route("updateTipoDeEquipo")]
        public async Task<ActionResult> UpdateTipoDeEquipo(int id,[FromBody] TipoDeEquipoUpdateDTO tipoDeEquipoUpdateDTO)
        {
            try
            {
                var tipoDeEquipoExistente = await _context.TiposDeEquipos.FirstOrDefaultAsync(te => te.idTipoDeEquipo == id && te.estado == 1); 
                if (tipoDeEquipoExistente == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Registro no encontrado");
                }
                
                if (!string.IsNullOrEmpty(tipoDeEquipoUpdateDTO.nombreTipoDeEquipo))
                {
                    tipoDeEquipoExistente.nombreTipoDeEquipo = tipoDeEquipoUpdateDTO.nombreTipoDeEquipo;
                }

                if (!string.IsNullOrEmpty(tipoDeEquipoUpdateDTO.descripcionTipoDeEquipo))
                {
                    tipoDeEquipoExistente.descripcionTipoDeEquipo = tipoDeEquipoUpdateDTO.descripcionTipoDeEquipo;
                }

                if (tipoDeEquipoUpdateDTO.stock.HasValue)
                {
                    tipoDeEquipoExistente!.stock = tipoDeEquipoUpdateDTO.stock.Value;
                }

                await _context.SaveChangesAsync();
                return Ok("Tipo de equipo actualizado correctamente");
            }
            catch (System.Exception e)
            {
                Console.Error.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al actualizar el registro");
            }
        }

        [ValidateJWT]
        [HttpPut]
        [Route("deleteTipoDeEquipo")]
        public async Task<ActionResult> DeleteTipoDeEquipo(int id)
        {
            try
            {
                var tipoDeEquipoExistente = await _context.TiposDeEquipos.FirstOrDefaultAsync(te => te.idTipoDeEquipo == id && te.estado == 1);
                if (tipoDeEquipoExistente == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Registro no encontrado");
                }
                
                tipoDeEquipoExistente.estado = 0;
                await _context.SaveChangesAsync();
                return Ok("Tipo de equipo eliminado correctamente");
            }
            catch (System.Exception e)
            {
                Console.Error.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al actualizar el registro");
            }
        }
    }
}