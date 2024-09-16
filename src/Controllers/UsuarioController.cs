using GestorInventario.src.Models.Contexts;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;

namespace GestorInventario.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : Controller
    {

        private readonly IConfiguration _configuracion;
        private readonly InventarioContext _context;

        public UsuarioController(IConfiguration configuracion, InventarioContext context)
        {
            _configuracion = configuracion;
            _context = context;
        }
        
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<Usuario>> Login([FromBody] Usuario us) {
            try
            {
                var user = await _context.Usuarios.SingleOrDefaultAsync(u => u.usuario == us.usuario && u.pass == us.pass);

                if (user == null)
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, "Credenciales no validas");
                }

                var claims = new[] {
                    new Claim(JwtRegisteredClaimNames.Sub, user.usuario),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuracion["JWT:Key"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: _configuracion["JWT:Issuer"],
                    audience:_configuracion["JWT:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddDays(2),
                    signingCredentials: creds);

                return Ok(new{token = new JwtSecurityTokenHandler().WriteToken(token)});
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al logearse");
            }
        }
    }
}