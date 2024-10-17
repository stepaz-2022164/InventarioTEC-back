    using GestorInventario.src.Middlewares;
    using GestorInventario.src.Models.Contexts;
    using GestorInventario.src.Models.DTO;
    using GestorInventario.src.Models.DTOUpdate;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    namespace GestorInventario.src.Models
    {
        [Route("api/[controller]")]
        [ApiController]
        public class PropietarioEquipoController : ControllerBase
        {
            private readonly InventarioContext _context;
            public PropietarioEquipoController(InventarioContext context)
            {
                _context = context;
            }

            [ValidateJWT]
            [HttpGet]
            [Route("getPropietariosEquipos")]
            public async Task<ActionResult<IEnumerable<PropietarioEquipo>>> GetPropietariosEquipos([FromQuery] int pagina = 1, [FromQuery] int numeroPaginas = 10)
            {
                try
                {
                    var totalRecords = await _context.PropietarioEquipos.CountAsync(pe => pe.estado == 1);
                    var propietariosEquipos = await _context.PropietarioEquipos
                    .Where(pe => pe.estado == 1)
                    .Include(pe => pe.Empleado)
                    .Include(pe => pe.TipoDeEquipo)
                    .Include(pe => pe.Equipo)
                    .Include(pe => pe.HUB)
                    .Skip((pagina - 1) * numeroPaginas)
                    .Take(numeroPaginas)
                    .Select(pe => new {
                        pe.idPropietarioEquipo,
                        nombreEmpleado = pe.Empleado.nombreEmpleado,
                        nombreTipoDeEquipo = pe.TipoDeEquipo.nombreTipoDeEquipo,
                        numeroDeSerie = pe.Equipo.numeroDeSerie,
                        nombreHUB = pe.HUB.nombreHUB,
                        fechaDeEntrega = pe.fechaDeEntrega.ToString("dd/MM/yyyy")
                    })
                    .ToListAsync();

                    if (propietariosEquipos.Count() == 0)
                    {
                        return StatusCode(StatusCodes.Status404NotFound, "No se encontraron registros");
                    }
                    return Ok(new {data = propietariosEquipos, totalRecords});
                }
                catch (System.Exception e)
                {
                    Console.Error.WriteLine(e);
                    return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener los registros");
                }
            }

            [ValidateJWT]
            [HttpGet]
            [Route("getPropietarioEquipoById")]
            public async Task<ActionResult<PropietarioEquipo>> GetPropietarioEquipoById(int id)
            {
                try
                {
                    var propietarioEquipo = await _context.PropietarioEquipos.FindAsync(id);
                    if (propietarioEquipo == null || propietarioEquipo.estado == 0)
                    {
                        return StatusCode(StatusCodes.Status404NotFound, "Registro no encontrado");
                    }
                    return Ok(propietarioEquipo);
                }
                catch (System.Exception e)
                {
                    Console.Error.WriteLine(e);
                    return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener el registro");
                }
            }

            [ValidateJWT]
            [HttpGet]
            [Route("getEquipoByOwner")]
            public async Task<ActionResult<IEnumerable<PropietarioEquipo>>> GetPropietarioEquipoByOwner(string nombre)
            {
                try
                {
                    var propietarioEquipo = await _context.PropietarioEquipos
                    .Include(pe => pe.Empleado)
                    .Include(pe => pe.TipoDeEquipo)
                    .Include(pe => pe.Equipo)
                    .Include(pe => pe.HUB)
                    .Where(pe => pe.Empleado.nombreEmpleado.Contains(nombre))
                    .Select(pe => new {
                        pe.idPropietarioEquipo,
                        nombreEmpleado = pe.Empleado.nombreEmpleado,
                        nombreTipoDeEquipo = pe.TipoDeEquipo.nombreTipoDeEquipo,
                        numeroDeSerie = pe.Equipo.numeroDeSerie,
                        nombreHUB = pe.HUB.nombreHUB,
                        fechaDeEntrega = pe.fechaDeEntrega.ToString("dd/MM/yyyy")
                    })
                    .ToListAsync();
                    
                    if (propietarioEquipo.Count() == 0)
                    {
                        return StatusCode(StatusCodes.Status404NotFound, "No se encontraron registros");
                    }
                    return Ok(propietarioEquipo);
                }
                catch (System.Exception e)
                {
                    Console.Error.WriteLine(e);
                    return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener los registros");
                }
            }

            [ValidateJWT]
            [HttpGet]
            [Route("getPropietarioEquipoByHUB")]
            public async Task<ActionResult<IEnumerable<PropietarioEquipo>>> GetPropietarioEquipoByHub(string hub)
            {
                try
                {
                    var propietariosEquipos = await _context.PropietarioEquipos
                    .Include(pe => pe.Empleado)
                    .Include(pe => pe.TipoDeEquipo)
                    .Include(pe => pe.Equipo)
                    .Include(pe => pe.HUB)
                    .Where(pe => pe.estado == 1 && pe.HUB.nombreHUB == hub)
                    .Select(pe => new {
                        pe.idPropietarioEquipo,
                        nombreEmpleado = pe.Empleado.nombreEmpleado,
                        nombreTipoDeEquipo = pe.TipoDeEquipo.nombreTipoDeEquipo,
                        numeroDeSerie = pe.Equipo.numeroDeSerie,
                        nombreHUB = pe.HUB.nombreHUB,
                        fechaDeEntrega = pe.fechaDeEntrega.ToString("dd/MM/yyyy")
                    })
                    .ToListAsync();

                    if (propietariosEquipos.Count() == 0)
                    {
                        return StatusCode(StatusCodes.Status404NotFound, "No se encontraron registros");
                    }
                    return Ok(propietariosEquipos);
                }
                catch (System.Exception e)
                {
                    Console.Error.WriteLine(e);
                    return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener los registros");
                }
            }

            [ValidateJWT]
            [HttpPost]
            [Route("createPropietarioEquipo")]
            public async Task<ActionResult<PropietarioEquipo>> CreatePropietarioEquipo([FromBody] PropietarioEquipoDTO propietarioEquipoDTO)
            {
                try
                {
                    if (!ModelState.IsValid!)
                    {
                        return BadRequest(ModelState);
                    }

                    var empleadoExistente = await _context.Empleados.FirstOrDefaultAsync(em => em.idEmpleado == propietarioEquipoDTO.idEmpleado && em.estado == 1);
                    if (empleadoExistente == null)
                    {
                        return StatusCode(StatusCodes.Status404NotFound, "Empleado no encontrado");
                    }

                    var tipoDeEquipoExistente = await _context.TiposDeEquipos.FirstOrDefaultAsync(te => te.idTipoDeEquipo == propietarioEquipoDTO.idTipoDeEquipo && te.estado == 1);
                    if (tipoDeEquipoExistente == null)
                    {
                        return StatusCode(StatusCodes.Status404NotFound, "Tipo de equipo no encontrado");
                    }

                    var equipoExistente = await _context.Equipos.FirstOrDefaultAsync(eq => eq.idEquipo == propietarioEquipoDTO.idEquipo && eq.estado == 1);
                    if (equipoExistente == null)
                    {
                        return StatusCode(StatusCodes.Status404NotFound, "Equipo no encontrado");
                    }

                    /*
                    var equipoAsignado = await _context.PropietarioEquipos.FirstOrDefaultAsync(ea => ea.idEquipo == propietarioEquipoDTO.idEquipo && ea.estado == 1);
                    if (equipoAsignado != null)
                    {
                        return StatusCode(StatusCodes.Status400BadRequest, "El equipo ya tiene un propietario");
                    }
                    */

                    var propietarioEquipo = new PropietarioEquipo
                    {
                        idEmpleado = propietarioEquipoDTO.idEmpleado,
                        idTipoDeEquipo = propietarioEquipoDTO.idTipoDeEquipo,
                        idEquipo = propietarioEquipoDTO.idEquipo,
                        idHUB = propietarioEquipoDTO.idHUB,
                        fechaDeEntrega = propietarioEquipoDTO.fechaDeEntrega,
                        estado = 1
                    };
                    await _context.PropietarioEquipos.AddAsync(propietarioEquipo);
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                catch (System.Exception e)
                {
                    Console.Error.WriteLine(e);
                    return StatusCode(StatusCodes.Status500InternalServerError, "Error al crear el registro");
                }
            }

            [ValidateJWT]
            [HttpPut]
            [Route("updatePropietarioEquipo")]
            public async Task<ActionResult<PropietarioEquipo>> UpdatePropietarioEquipo(int id, [FromBody] PropietarioEquipoUpdateDTO propietarioEquipoUpdateDTO)
            {
                try
                {
                    var propietarioEquipoExistente = await _context.PropietarioEquipos.FirstOrDefaultAsync(pe => pe.idPropietarioEquipo == id && pe.estado == 1);
                    if (propietarioEquipoExistente == null)
                    {
                        return StatusCode(StatusCodes.Status404NotFound, "Registro no encontrado");
                    }

                    if (propietarioEquipoUpdateDTO.idEmpleado.HasValue)
                    {
                        var empleadoExistente = await _context.Empleados.FirstOrDefaultAsync(em => em.idEmpleado == propietarioEquipoUpdateDTO.idEmpleado && em.estado == 1);
                        if (empleadoExistente == null)
                        {
                            return StatusCode(StatusCodes.Status404NotFound, "Empleado no encontrado");
                        }
                        propietarioEquipoExistente!.idEmpleado = propietarioEquipoUpdateDTO.idEmpleado.Value;
                    }

                    
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                catch (System.Exception e)
                {
                    Console.Error.WriteLine(e);
                    return StatusCode(StatusCodes.Status500InternalServerError, "Error al actualizar el registro");
                }
            }

            [ValidateJWT]
            [HttpPut]
            [Route("deletePropietarioEquipo")]
            public async Task<ActionResult<PropietarioEquipo>> DeletePropietarioEquipo(int id)
            {
                try
                {
                    var propietarioEquipoExistente = await _context.PropietarioEquipos.FirstOrDefaultAsync(pe => pe.idPropietarioEquipo == id && pe.estado == 1);
                    if (propietarioEquipoExistente == null)
                    {
                        return StatusCode(StatusCodes.Status404NotFound, "Registro no encontrado");
                    }
                    propietarioEquipoExistente.estado = 0;
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                catch (System.Exception e)
                {
                    Console.Error.WriteLine(e);
                    return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener los registros");
                }
            }

            [ValidateJWT]
            [HttpGet]
            [Route("descargarHoja")]
            public IActionResult GenerarPDF(){
                try
                {
                    var ruta = Path.Combine(Directory.GetCurrentDirectory(), "Utils", "Hoja.pdf");

                    if (!System.IO.File.Exists(ruta))
                    {
                        return StatusCode(StatusCodes.Status400BadRequest);
                    }   

                    var fileBytes = System.IO.File.ReadAllBytes(ruta);
                    return File(fileBytes, "application/pdf", "Hoja.pdf");
                }
                catch (System.Exception e)
                {
                    Console.Error.WriteLine(e);
                    return StatusCode(StatusCodes.Status500InternalServerError, "Error al descargar el archivo");
                }
            }
        }
    }