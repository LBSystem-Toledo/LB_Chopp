using LB_ChoppAPI.Repository.Interface;
using LB_ChoppAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LB_ChoppAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly ICliente _clienteDAO;

        public ClienteController(ICliente clienteDAO) { _clienteDAO = clienteDAO; }

        [HttpGet, Route("GetAsync")]
        public async Task<IActionResult> Get([FromQuery]string Cd_cliente,
                                             [FromQuery]string Nome)
        {
            if (!Request.Headers.ContainsKey("token"))
                return StatusCode(500, "Acesso não autorizado");
            try
            {
                var result = await _clienteDAO.GetAsync(Request.Headers["token"].ToString(), Cd_cliente, Nome);
                return Ok(result);
            }
            catch { return BadRequest(); }
        }
    }
}
