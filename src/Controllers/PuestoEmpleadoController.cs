using GestorInventario.src.Middlewares;
using GestorInventario.src.Models.Contexts;
using GestorInventario.src.Models.DTO;
using GestorInventario.src.Models.DTOUpdate;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestorInventario.src.Controllers
{
    [Route("api/[controller]")]
    public class PuestoEmpleadoController : ControllerBase
    {
        private readonly InventarioContext _context;
        public PuestoEmpleadoController(InventarioContext context)
        {
            _context = context;
        }

        [ValidateJWT]
        [HttpGet]
        [Route("getPuestosEmpleados")]
        public async Task<ActionResult<IEnumerable<PuestoEmpleado>>> GetPuestosEmpleados(){
            try
            {
                var puestosEmpleados = await _context.PuestosEmpleados.Where(pe => pe.estado == 1).ToListAsync();
                if (puestosEmpleados.Count() == 0)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "No se encontraron registros");
                }
                return Ok(puestosEmpleados);
            }
            catch (System.Exception e)
            {
                Console.Error.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener los registros");
            }
        }

        [ValidateJWT]
        [HttpGet]
        [Route("getPuestoEmpleadoById")]
        public async Task<ActionResult<PuestoEmpleado>> GetPuestoEmpleado(int id){
            try
            {
                var puestoEmpleado = await _context.PuestosEmpleados.FindAsync(id);
                if (puestoEmpleado == null || puestoEmpleado.estado == 0)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "No se encontró el registro");
                }
                return Ok(puestoEmpleado);
            }
            catch (System.Exception e)
            {
                Console.Error.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener los registros");
            }
        }

        [ValidateJWT]
        [HttpGet]
        [Route("getPuestoEmpleadoByName")]
        public async Task<ActionResult<PuestoEmpleado>> GetPuestoEmpleadoByName(string name){
            try
            {
                var puestoEmpleado = await _context.PuestosEmpleados.Where(pe => pe.nombrePuestoEmpleado.Contains(name) && pe.estado == 1).ToListAsync();
                if (puestoEmpleado == null || puestoEmpleado.Count == 0)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "No se encontraron registros");
                }
                return Ok(puestoEmpleado);
            }
            catch (System.Exception e)
            {
                Console.Error.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener los registros");
            }
        }

        [ValidateJWT]
        [HttpPost]
        [Route("createPuestoEmpleado")]
        public async Task<ActionResult<PuestoEmpleado>> CreatePuestoEmpleado([FromBody] PuestoEmpleadoDTO puestoEmpleadoDTO){
            try
            {
                if (!ModelState.IsValid!)
                {
                    return BadRequest(ModelState);
                }

                var puestoEmpledoExistente = await _context.PuestosEmpleados.FirstOrDefaultAsync(pe => pe.nombrePuestoEmpleado == puestoEmpleadoDTO.nombrePuestoEmpleado);
                if (puestoEmpledoExistente != null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Puesto de empleado ya existente");
                }

                var areaEmpleado = await _context.AreasEmpleados.FindAsync(puestoEmpleadoDTO.idAreaEmpleado);
                if (areaEmpleado == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "No se encontro el área del empleado");
                }

                var puestoEmpleado = new PuestoEmpleado{
                    nombrePuestoEmpleado = puestoEmpleadoDTO.nombrePuestoEmpleado,
                    descripcionPuestoEmpleado = puestoEmpleadoDTO.descripcionPuestoEmpleado,
                    idAreaEmpleado = puestoEmpleadoDTO.idAreaEmpleado,
                    estado = 1
                };

                await _context.AddAsync(puestoEmpleado);
                await _context.SaveChangesAsync();
                return Ok("Puesto de empleado creado correctamente");
            }
            catch (System.Exception e)
            {
                Console.Error.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al crear el registro");
            }
        }

        [ValidateJWT]
        [HttpPut]
        [Route("updatePuestoEmpleado")]
        public async Task<ActionResult<PuestoEmpleado>> UpdatePuestoEmpleado(int id,[FromBody] PuesEmpleadoUpdateDTO puesEmpleadoUpdateDTO){
            try
            {
                var puestoEmpledoExistente = await _context.PuestosEmpleados.FindAsync(id);
                if (puestoEmpledoExistente == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Registro no encontrado");
                }

                if (!string.IsNullOrEmpty(puesEmpleadoUpdateDTO.nombrePuestoEmpleado))
                {
                    puestoEmpledoExistente!.nombrePuestoEmpleado = puesEmpleadoUpdateDTO.nombrePuestoEmpleado;
                }

                if (!string.IsNullOrEmpty(puesEmpleadoUpdateDTO.descripcionPuestoEmpleado))
                {
                    puestoEmpledoExistente!.descripcionPuestoEmpleado = puesEmpleadoUpdateDTO.descripcionPuestoEmpleado;
                }

                await _context.SaveChangesAsync();
                return Ok("Puesto de empleado actualizado correctamente");
            }
            catch (System.Exception e)
            {
                Console.Error.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al actualizar el registro");
            }
        }

        [ValidateJWT]
        [HttpPut]
        [Route("deletePuestoEmpleado")]
        public async Task<ActionResult<PuestoEmpleado>> DeletePuestoEmpleado(int id){
            try
            {
                var puestoEmpleadoExistente = await _context.PuestosEmpleados.FindAsync(id);
                if (puestoEmpleadoExistente == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Registro no encontrado");
                }
                puestoEmpleadoExistente.estado = 0;
                await _context.SaveChangesAsync();
                return Ok("Puesto de empleado eliminado correctamente");
            }
            catch (System.Exception e)
            {
                Console.Error.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al eliminar el registro");
            }
        }
    }
}