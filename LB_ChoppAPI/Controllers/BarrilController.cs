using LB_ChoppAPI.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LB_ChoppAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class BarrilController : ControllerBase
    {
        private readonly IBarril _queryDAO;
        public BarrilController(IBarril queryDAO) { _queryDAO = queryDAO; }

        [HttpGet, Route("GetBarrilLivreAsync")]
        public async Task<IActionResult> Get([FromQuery] string Cd_produto,
                                             [FromQuery] int Volume)
        {
            if (!Request.Headers.ContainsKey("token"))
                return StatusCode(500, "Acesso não autorizado");
            try
            {
                var retorno = await _queryDAO.GetBarrilLivreAsync(Request.Headers["token"].ToString(), Cd_produto, Volume);
                return Ok(retorno);
            }
            catch { return NotFound(); }
        }
        [HttpGet, Route("GetBarrisTiposAsync")]
        public async Task<IActionResult> GetBarrisTiposAsync([FromQuery] string Cd_produto)
        {
            if (!Request.Headers.ContainsKey("token"))
                return StatusCode(500, "Acesso não autorizado");
            try
            {
                var retorno = await _queryDAO.GetBarrisTiposAsync(Request.Headers["token"].ToString(), Cd_produto);
                return Ok(retorno);
            }
            catch { return NotFound(); }
        }
    }
}
