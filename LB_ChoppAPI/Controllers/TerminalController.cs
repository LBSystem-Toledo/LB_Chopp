using LB_ChoppAPI.Models;
using LB_ChoppAPI.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LB_ChoppAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class TerminalController : ControllerBase
    {
        private readonly ITerminalMobile _terminalDAO;

        public TerminalController(ITerminalMobile terminalDAO) { _terminalDAO = terminalDAO; }

        [HttpPost, Route("ValidarDeviceAsync")]
        public async Task<IActionResult> ValidarDeviceAsync([FromBody] TerminalMobile terminal)
        {
            if (!Request.Headers.ContainsKey("token"))
                return StatusCode(500, "Acesso não autorizado");
            try
            {
                var result = await _terminalDAO.ValidarDeviceAsync(Request.Headers["token"].ToString(), terminal);
                return Ok(result);
            }
            catch { return BadRequest(); }
        }
    }
}
