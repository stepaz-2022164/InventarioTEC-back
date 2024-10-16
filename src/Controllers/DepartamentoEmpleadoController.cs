using GestorInventario.src.Middlewares;
using GestorInventario.src.Models.Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestorInventario.src.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartamentoEmpleadoController : ControllerBase
    {
        private readonly InventarioContext _context;

        public DepartamentoEmpleadoController(InventarioContext context)
        {
            _context = context;
        }

        [ValidateJWT]
        [HttpGet]
        [Route("getDepartamentosEmpleados")]
        public async Task<ActionResult<IEnumerable<DepartamentoEmpleado>>> GetDepartamentosEmpleados([FromQuery] int pagina = 1, [FromQuery] int numeroPaginas = 10)
        {
            try
            {
                var totalRecords = await _context.DepartamentosEmpleados.CountAsync();
                var departamentosEmpleados = await _context.DepartamentosEmpleados
                .Skip((pagina - 1) * numeroPaginas)
                .Take(numeroPaginas)
                .Select(se => new {
                    id = se.idDepartamentoEmpleado,
                    nombre = se.nombreDepartamentoEmpleado,
                    se.descripcionAreaEmpleado
                })
                .ToListAsync();
                
                if (departamentosEmpleados.Count() == 0)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "No se encontraron registros");
                }
                return Ok(new {data = departamentosEmpleados, totalRecords});
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener los Departamentos");
            }
        }

        

        [ValidateJWT]
        [HttpGet]
        [Route("getDepartamentoEmpleadoById")]
        public async Task<ActionResult<DepartamentoEmpleado>> GetDepartamentoEmpleadoById(int id)
        {
            try
            {
                var departamentoEmpleado = await _context.DepartamentosEmpleados.FindAsync(id);
                if (departamentoEmpleado == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "No se encontro el registro");
                }
                return Ok(departamentoEmpleado);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener los registros");
            }
        }

        [ValidateJWT]
        [HttpGet]
        [Route("getDepartamentoEmpleadoByName")]
        public async Task<ActionResult<DepartamentoEmpleado>> GetDepartamentoEmpleadoByName(string name){
            try
            {
                var departamentoEmpleado = await _context.DepartamentosEmpleados.Where(de => de.nombreDepartamentoEmpleado.Contains(name))
                .Select(se => new {
                    id = se.idDepartamentoEmpleado,
                    nombre = se.nombreDepartamentoEmpleado,
                    se.descripcionAreaEmpleado
                })
                .ToListAsync();
                if (departamentoEmpleado == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "No se encontraron registros");
                }
                return Ok(departamentoEmpleado);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener los registros");
            }
        }

        [ValidateJWT]
        [HttpPost]
        [Route("createDepartamentoEmpleado")]
        public async Task<ActionResult<DepartamentoEmpleado>> CreateDepartamentoEmpleado([FromBody] DepartamentoEmpleado departamentoEmpleado)
        {
            try
            {
                if (!ModelState.IsValid!)
                {
                    return BadRequest(ModelState);
                }
                var departamentoExistente = await _context.DepartamentosEmpleados.FirstOrDefaultAsync(de => de.nombreDepartamentoEmpleado == departamentoEmpleado.nombreDepartamentoEmpleado);

                await _context.DepartamentosEmpleados.AddAsync(departamentoEmpleado);
                await _context.SaveChangesAsync();

                return Ok("Producto creado correctamente");
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al crear el registro");
            }
        }

        [ValidateJWT]
        [HttpPut]
        [Route("updateDepartamentoEmpleado")]
        public async Task<ActionResult<DepartamentoEmpleado>> UpdateDepartamentoEmpleado(int id, [FromBody] DepartamentoEmpleado departamentoEmpleado)
        {
            try
            {

                var departamentoExistente = await _context.DepartamentosEmpleados.FindAsync(id);
                if (departamentoExistente == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Registro no encontrado");
                }
                departamentoExistente.nombreDepartamentoEmpleado = departamentoEmpleado.nombreDepartamentoEmpleado;
                departamentoExistente.descripcionAreaEmpleado = departamentoEmpleado.descripcionAreaEmpleado;
                await _context.SaveChangesAsync();
                return Ok("Producto actualizado correctamente");
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al actualizar el registro");
            }
        }

        [ValidateJWT]
        [HttpDelete]
        [Route("deleteDepartamentoEmpleado")]
        public async Task<ActionResult> DeleteDepartamentoEmpleado(int id)
        {
            try
            {
                var departamentoExistente = await _context.DepartamentosEmpleados.FindAsync(id);
                if (departamentoExistente == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Registro no encontrado");
                }
                _context.DepartamentosEmpleados.Remove(departamentoExistente);
                await _context.SaveChangesAsync();
                return Ok("Departamento eliminado correctamente");
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al eliminar el registro");;
            }
        }
    }
}