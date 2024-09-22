using GestorInventario.src.Middlewares;
using GestorInventario.src.Models.Contexts;
using GestorInventario.src.Models.DTOUpdate;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestorInventario.src.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarcaController : ControllerBase
    {
        private readonly InventarioContext _context;
        public MarcaController(InventarioContext context)
        {
            _context = context;
        }

        [ValidateJWT]
        [HttpGet]
        [Route("getMarcas")]
        public async Task<ActionResult<IEnumerable<Marca>>> GetMarcas(){
            try
            {
                var marcas = await _context.Marcas.Where(m => m.estado == 1).ToListAsync();
                if (marcas.Count() == 0)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "No se encontraron registros");
                }
                return Ok(marcas);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener los registros");
            }
        }

        [ValidateJWT]
        [HttpGet]
        [Route("getMarcaById")]
        public async Task<ActionResult<Marca>> GetMarcaById(int id)
        {
            try
            {
                var marca = await _context.Marcas.FindAsync(id);
                if (marca == null || marca.estado == 0)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "No se encontró el registro");
                }
                return Ok(marca);
            }
            catch (System.Exception e)
            {
                Console.Error.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener el registro");
            }
        }

        [ValidateJWT]
        [HttpGet]
        [Route("getMarcaByName")]
        public async Task<ActionResult<Marca>> GetMarcaByName(string nombre)
        {
            try
            {
                var marca = await _context.Marcas.Where(m => m.nombreMarca.Contains(nombre) && m.estado == 1).ToListAsync();
                if (marca == null || marca.Count == 0)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "No se encontraron registros");
                }
                return Ok(marca);
            }
            catch (System.Exception e)
            {
                Console.Error.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener el registro");
            }
        }

        [ValidateJWT]
        [HttpPost]
        [Route("createMarca")]
        public async Task<ActionResult<Marca>> CreateMarca([FromBody] Marca marca)
        {
            try
            {
                if (!ModelState.IsValid!)
                {
                    return BadRequest(ModelState);
                }

                var marcaExistente = await _context.Marcas.FirstOrDefaultAsync(m => m.estado == 1 && m.nombreMarca == marca.nombreMarca);
                if (marcaExistente!= null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Marca ya existente");
                }

                var nuevaMarca = new Marca{
                    nombreMarca = marca.nombreMarca,
                    descripcionMarca = marca.descripcionMarca,
                    estado = 1
                };
                await _context.Marcas.AddAsync(nuevaMarca);
                await _context.SaveChangesAsync();
                return Ok("Marca creada correctamente");
            }
            catch (System.Exception e)
            {
                Console.Error.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al crear el registro");
            }
        }

        [ValidateJWT]
        [HttpPut]
        [Route("updateMarca")]
        public async Task<ActionResult<Marca>> UpdateMarca(int id,[FromBody] MarcaUpdateDTO marca)
        {
            try
            {
                var marcaExistente = await _context.Marcas.FirstOrDefaultAsync(m => m.idMarca == id && m.estado == 1);
                if (marcaExistente == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Registro no encontrado");
                }

                if (!string.IsNullOrEmpty(marca.nombreMarca))
                {
                    marcaExistente!.nombreMarca = marca.nombreMarca;
                }
                if (!string.IsNullOrEmpty(marca.descripcionMarca))
                {
                    marcaExistente!.descripcionMarca = marca.descripcionMarca;
                }
                await _context.SaveChangesAsync();
                return Ok("Marca actualizada correctamente");
            }
            catch (System.Exception e)
            {
                Console.Error.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al crear el registro");
            }
        }

        [ValidateJWT]
        [HttpPut]
        [Route("deleteMarca")]
        public async Task<ActionResult<Marca>> DeleteMarca(int id)
        {
            try
            {
                var marcaExistente = await _context.Marcas.FirstOrDefaultAsync(m => m.idMarca == id && m.estado == 1);
                if (marcaExistente == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Registro no encontrado");
                }
                marcaExistente.estado = 0;
                await _context.SaveChangesAsync();
                return Ok("Marca eliminada correctamente");
            }
            catch (System.Exception e)
            {
                Console.Error.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al crear el registro");
            }
        }
    }
}