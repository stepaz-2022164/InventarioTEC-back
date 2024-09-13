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

        [HttpGet]
        [Route("getPaises")]
        public async Task<ActionResult<IEnumerable<Pais>>> GetPaises()
        {
            var paises = await _context.Paises.ToListAsync();
            return Ok(paises);
        }
    }
}