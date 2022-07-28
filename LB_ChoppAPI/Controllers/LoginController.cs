using LB_ChoppAPI.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LB_ChoppAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IVendedor _vendedorDAO;

        public LoginController(IVendedor vendedor) { _vendedorDAO = vendedor; }

        [HttpGet, Route("LoginAsync")]
        public async Task<IActionResult> LoginAsync()
        {
            if (!Request.Headers.ContainsKey("token"))
                return StatusCode(500, "Acesso não autorizado");
            if (!Request.Headers.ContainsKey("cnpj"))
                return StatusCode(500, "Obrigatório informar CNPJ");
            if (!Request.Headers.ContainsKey("login"))
                return StatusCode(500, "Obrigatório informar LOGIN");
            if (!Request.Headers.ContainsKey("senha"))
                return StatusCode(500, "Obrigatório informar SENHA");
            try
            {
                var ret = await _vendedorDAO.validarAsync(Request.Headers["token"].ToString(), Request.Headers["login"].ToString(), Request.Headers["senha"].ToString(), Request.Headers["cnpj"].ToString());
                return Ok(ret);
            }
            catch { return BadRequest(); }
        }
    }
}
