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
    public class EmpleadoController : ControllerBase
    {
        private readonly InventarioContext _context;
        public EmpleadoController(InventarioContext context)
        {
            _context = context;
        }

        [ValidateJWT]
        [HttpGet]
        [Route("getEmpleados")]
        public async Task<ActionResult<IEnumerable<Empleado>>> GetEmpleados(){
            try
            {
                var empleados = await _context.Empleados.Where(e => e.estado == 1).ToListAsync();
                if (empleados.Count() == 0)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "No se encontraron registros");
                }
                return Ok(empleados);
            }
            catch (System.Exception e)
            {
                Console.Error.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener los registros");
            }
        }

        [ValidateJWT]
        [HttpGet]
        [Route("getEmpleadoById")]
        public async Task<ActionResult<Empleado>> GetEmpleadoById(int id){
            try
            {
                var empleado = await _context.Empleados.FindAsync(id);
                if (empleado == null || empleado.estado == 0)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "No se encontró el registro");
                }
                return Ok(empleado);
            }
            catch (System.Exception e)
            {
                Console.Error.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener el registro");
            }
        }

        [ValidateJWT]
        [HttpGet]
        [Route("getEmpleadoByName")]
        public async Task<ActionResult<Empleado>> GetEmpleadoByName(string name){
            try
            {
                var empleado = await _context.Empleados.Where(e => e.estado == 1 && e.nombreEmpleado.Contains(name)).ToListAsync();
                if (empleado == null || empleado.Count == 0)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "No se encontraron registros");
                }
                return Ok(empleado);
            }
            catch (System.Exception e)
            {
                Console.Error.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener los registros");
            }
        }

        [ValidateJWT]
        [HttpPost]
        [Route("createEmpleado")]
        public async Task<ActionResult<Empleado>> CreateEmpleado([FromBody] EmpleadoDTO empleadoDTO){
            try
            {
                if (!ModelState.IsValid!)
                {
                    return BadRequest(ModelState);
                }

                var empleadoExistente = await _context.Empleados.FirstOrDefaultAsync(e => e.estado == 1 && e.numeroDeFicha == empleadoDTO.numeroDeFicha);
                if (empleadoExistente != null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Empleado ya existente");
                }

                var departamentoEmpleado = await _context.DepartamentosEmpleados.FindAsync(empleadoDTO.idDepartamentoEmpleado);
                if (departamentoEmpleado == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Departamento no encontrado");
                }

                var areaEmpleado = await _context.AreasEmpleados.FindAsync(empleadoDTO.idAreaEmpleado);
                if (areaEmpleado == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Área no encontrada");
                }

                var puestoEmpleado = await _context.PuestosEmpleados.FindAsync(empleadoDTO.idPuestoEmpleado);
                if (puestoEmpleado == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Puesto no encontrado");
                }

                var sede = await _context.Sedes.FindAsync(empleadoDTO.idSede);
                if (sede == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Sede no encontrada");
                }

                var empleado = new Empleado
                {
                    numeroDeFicha = empleadoDTO.numeroDeFicha,
                    nombreEmpleado = empleadoDTO.nombreEmpleado,
                    telefonoEmpleado = empleadoDTO.telefonoEmpleado,
                    correoEmpleado = empleadoDTO.correoEmpleado,
                    idDepartamentoEmpleado = empleadoDTO.idDepartamentoEmpleado,
                    idAreaEmpleado = empleadoDTO.idAreaEmpleado,
                    idPuestoEmpleado = empleadoDTO.idPuestoEmpleado,
                    idSede = empleadoDTO.idSede,
                    estado = 1
                };

                await _context.Empleados.AddAsync(empleado);
                await _context.SaveChangesAsync();
                return Ok("Empleado creado correctamente");
            }
            catch (System.Exception e)
            {
                Console.Error.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al crear el registro");
            }
        }

        [ValidateJWT]
        [HttpPut]
        [Route("updateEmpleado")]
        public async Task<ActionResult<Empleado>> UpdateEmpleado(int id ,[FromBody] EmpleadoDTOUpdate empleadoDTOUpdate){
            try
            {
                var empleadoExistente = await _context.Empleados.FirstOrDefaultAsync(e => e.idEmpleado == id && e.estado == 1);
                if (empleadoExistente == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Empleado no encontrado");
                }

                if (!string.IsNullOrEmpty(empleadoDTOUpdate.nombreEmpleado))
                {
                    empleadoExistente!.nombreEmpleado = empleadoDTOUpdate.nombreEmpleado;
                }

                if (!string.IsNullOrEmpty(empleadoDTOUpdate.telefonoEmpleado))
                {
                    empleadoExistente!.telefonoEmpleado = empleadoDTOUpdate.telefonoEmpleado;
                }

                if (!string.IsNullOrEmpty(empleadoDTOUpdate.correoEmpleado))
                {
                    empleadoExistente!.correoEmpleado = empleadoDTOUpdate.correoEmpleado;
                }

                await _context.SaveChangesAsync();
                return Ok("Empleado actualizado correctamente");
            }
            catch (System.Exception e)
            {
                Console.Error.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al actualizar el empleado");
            }
        }

        [ValidateJWT]
        [HttpPut]
        [Route("deleteEmpleado")]
        public async Task<ActionResult<Empleado>> DeleteEmpleado(int id)
        {
            try
            {
                var empleadoExistente = await _context.Empleados.FirstOrDefaultAsync(e => e.idEmpleado == id && e.estado == 1);
                if (empleadoExistente == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Empleado no encontrado");
                }

                empleadoExistente.estado = 0;
                await _context.SaveChangesAsync();
                return Ok("Empleado eliminado correctamente");
            }
            catch (System.Exception e)
            {
                Console.Error.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al eliminar el empleado");
            }
        }
    }
}