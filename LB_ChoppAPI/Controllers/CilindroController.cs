using LB_ChoppAPI.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LB_ChoppAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class CilindroController : ControllerBase
    {
        private readonly ICilindro _queryDAO;
        public CilindroController(ICilindro queryDAO) { _queryDAO = queryDAO; }

        [HttpGet, Route("GetCilindroLivreAsync")]
        public async Task<IActionResult> Get()
        {
            if (!Request.Headers.ContainsKey("token"))
                return StatusCode(500, "Acesso não autorizado");
            try
            {
                var retorno = await _queryDAO.GetCilindroLivreAsync(Request.Headers["token"].ToString());
                return Ok(retorno);
            }
            catch { return NotFound(); }
        }
    }
}
