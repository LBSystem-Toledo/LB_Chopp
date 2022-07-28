using LB_ChoppAPI.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LB_ChoppAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ChoppController : ControllerBase
    {
        private readonly IChopp _queryDAO;
        public ChoppController(IChopp queryDAO) { _queryDAO = queryDAO; }

        [HttpGet, Route("GetChoppAsync")]
        public async Task<IActionResult> Get()
        {
            if (!Request.Headers.ContainsKey("token"))
                return StatusCode(500, "Acesso não autorizado");
            try
            {
                var retorno = await _queryDAO.GetChoppAsync(Request.Headers["token"].ToString());
                return Ok(retorno);
            }
            catch { return NotFound(); }
        }
    }
}
