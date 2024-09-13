using GestorInventario.src.Models.Contexts;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace GestorInventario.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : Controller
    {

        private readonly IConfiguration _configuracion;

        public UsuarioController(IConfiguration configuracion)
        {
            _configuracion = configuracion;
        }
        private readonly InventarioContext _context;

        public UsuarioController(InventarioContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<Usuario>> Login([FromBody] Usuario u) {
            try
            {
                if (!ModelState.IsValid!)
                {
                    return BadRequest(ModelState);
                }

                var user = await _context.Usuarios.FindAsync(u.usuario, u.pass);
                if (user != null)
                {
                    var usuarioLogeado = (
                        uid: u.idUsuario,
                        usuario: u.usuario
                    );
                    var token = GenerarJWT(u);
                    return Ok((usuarioLogeado, new {token}));
                }
                return StatusCode(StatusCodes.Status401Unauthorized, "Credenciales invalidas");
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al logearse");
            }
        }

        private string GenerarJWT(Usuario user){
            var llave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuracion["Jwt:Key"]));
            var credenciales = new SigningCredentials(llave, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.usuario),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

                    var token = new JwtSecurityToken(
            issuer: _configuracion["Jwt:Issuer"],
            audience: _configuracion["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddDays(2),
            signingCredentials: credenciales
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}