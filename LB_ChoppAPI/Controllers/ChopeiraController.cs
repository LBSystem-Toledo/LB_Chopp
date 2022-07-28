using LB_ChoppAPI.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LB_ChoppAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ChopeiraController : ControllerBase
    {
        private readonly IChopeira _queryDAO;
        public ChopeiraController(IChopeira queryDAO) { _queryDAO = queryDAO; }

        [HttpGet, Route("GetChopeiraLivreAsync")]
        public async Task<IActionResult> Get([FromQuery] string Voltagem,
                                             [FromQuery] string Qt_torneiras)
        {
            if (!Request.Headers.ContainsKey("token"))
                return StatusCode(500, "Acesso não autorizado");
            try
            {
                var retorno = await _queryDAO.GetChopeiraLivreAsync(Request.Headers["token"].ToString(), Voltagem, Qt_torneiras);
                return Ok(retorno);
            }
            catch { return NotFound(); }
        }
        [HttpGet, Route("GetChopeirasDisponiveisAsync")]
        public async Task<IActionResult> GetChopeirasDisponiveisAsync([FromQuery] string dt_ini,
                                                                      [FromQuery] string dt_fin)
        {
            if (!Request.Headers.ContainsKey("token"))
                return StatusCode(500, "Acesso não autorizado");
            try
            {
                var retorno = await _queryDAO.GetChopeirasDisponiveisAsync(Request.Headers["token"].ToString(), dt_ini, dt_fin);
                return Ok(retorno);
            }
            catch { return NotFound(); }
        }
    }
}
