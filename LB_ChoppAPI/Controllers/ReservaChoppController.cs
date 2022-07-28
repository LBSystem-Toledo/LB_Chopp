using LB_ChoppAPI.Repository.Interface;
using LB_ChoppAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LB_ChoppAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ReservaChoppController : ControllerBase
    {
        private readonly IReservaChopp _reservaDAO;
        public ReservaChoppController(IReservaChopp reservaDAO) { _reservaDAO = reservaDAO; }

        [HttpGet, Route("GetReservaExpedirAsync")]
        public async Task<IActionResult> GetReservaExpedirAsync([FromQuery] int Id_reserva,
                                                                [FromQuery] string Cd_clifor,
                                                                [FromQuery] string Dt_ini,
                                                                [FromQuery] string Dt_fin)
        {
            if (!Request.Headers.ContainsKey("token"))
                return StatusCode(500, "Acesso não autorizado");
            try
            {
                var result = await _reservaDAO.GetReservaExpedirAsync(Request.Headers["token"].ToString(), 
                                                                      Id_reserva, 
                                                                      Cd_clifor,
                                                                      Dt_ini,
                                                                      Dt_fin);
                return Ok(result);
            }
            catch { return BadRequest(); }
        }
        [HttpGet, Route("GetChopeirasExpedirAsync")]
        public async Task<IActionResult> GetChopeirasExpedirAsync([FromQuery] string Cd_empresa,
                                                                  [FromQuery] int Id_reserva)
        {
            if (!Request.Headers.ContainsKey("token"))
                return StatusCode(500, "Acesso não autorizado");
            try
            {
                var result = await _reservaDAO.GetChopeirasExpedirAsync(Request.Headers["token"].ToString(),
                                                                        Cd_empresa,
                                                                        Id_reserva);
                return Ok(result);
            }
            catch { return BadRequest(); }
        }
        [HttpGet, Route("GetBarrisExpedirAsync")]
        public async Task<IActionResult> GetBarrisExpedirAsync([FromQuery] string Cd_empresa,
                                                               [FromQuery] int Id_reserva)
        {
            if (!Request.Headers.ContainsKey("token"))
                return StatusCode(500, "Acesso não autorizado");
            try
            {
                var result = await _reservaDAO.GetBarrisExpedirAsync(Request.Headers["token"].ToString(),
                                                                     Cd_empresa,
                                                                     Id_reserva);
                return Ok(result);
            }
            catch { return BadRequest(); }
        }
        [HttpGet, Route("GetCilindrosExpedirAsync")]
        public async Task<IActionResult> GetCilindrosExpedirAsync([FromQuery] string Cd_empresa,
                                                                  [FromQuery] int Id_reserva)
        {
            if (!Request.Headers.ContainsKey("token"))
                return StatusCode(500, "Acesso não autorizado");
            try
            {
                var result = await _reservaDAO.GetCilindrosExpedirAsync(Request.Headers["token"].ToString(),
                                                                        Cd_empresa,
                                                                        Id_reserva);
                return Ok(result);
            }
            catch { return BadRequest(); }
        }
        [HttpGet, Route("GetReservaColetarAsync")]
        public async Task<IActionResult> GetReservaColetarAsync([FromQuery] int Id_reserva,
                                                                [FromQuery] string Cd_clifor,
                                                                [FromQuery] string Dt_ini,
                                                                [FromQuery] string Dt_fin)
        {
            if (!Request.Headers.ContainsKey("token"))
                return StatusCode(500, "Acesso não autorizado");
            try
            {
                var result = await _reservaDAO.GetReservaColetarAsync(Request.Headers["token"].ToString(),
                                                                      Id_reserva,
                                                                      Cd_clifor,
                                                                      Dt_ini,
                                                                      Dt_fin);
                return Ok(result);
            }
            catch { return BadRequest(); }
        }
        [HttpGet, Route("GetChopeirasColetarAsync")]
        public async Task<IActionResult> GetChopeirasColetarAsync([FromQuery] string Cd_empresa,
                                                                  [FromQuery] int Id_reserva)
        {
            if (!Request.Headers.ContainsKey("token"))
                return StatusCode(500, "Acesso não autorizado");
            try
            {
                var result = await _reservaDAO.GetChopeirasColetarAsync(Request.Headers["token"].ToString(),
                                                                        Cd_empresa,
                                                                        Id_reserva);
                return Ok(result);
            }
            catch { return BadRequest(); }
        }
        [HttpGet, Route("GetBarrisColetarAsync")]
        public async Task<IActionResult> GetBarrisColetarAsync([FromQuery] string Cd_empresa,
                                                               [FromQuery] int Id_reserva)
        {
            if (!Request.Headers.ContainsKey("token"))
                return StatusCode(500, "Acesso não autorizado");
            try
            {
                var result = await _reservaDAO.GetBarrisColetarAsync(Request.Headers["token"].ToString(),
                                                                     Cd_empresa,
                                                                     Id_reserva);
                return Ok(result);
            }
            catch { return BadRequest(); }
        }
        [HttpGet, Route("GetCilindrosColetarAsync")]
        public async Task<IActionResult> GetCilindrosColetarAsync([FromQuery] string Cd_empresa,
                                                                  [FromQuery] int Id_reserva)
        {
            if (!Request.Headers.ContainsKey("token"))
                return StatusCode(500, "Acesso não autorizado");
            try
            {
                var result = await _reservaDAO.GetCilindrosColetarAsync(Request.Headers["token"].ToString(),
                                                                        Cd_empresa,
                                                                        Id_reserva);
                return Ok(result);
            }
            catch { return BadRequest(); }
        }
        [HttpPost, Route("GravarExpedicaoAsync")]
        public async Task<IActionResult> GravarExpedicaoAsync([FromBody] ReservaChopp reserva)
        {
            if (!Request.Headers.ContainsKey("token"))
                return StatusCode(500, "Acesso não autorizado");
            try
            {
                var result = await _reservaDAO.GravarExpedicaoAsync(Request.Headers["token"].ToString(), reserva);
                return Ok(result);
            }
            catch { return BadRequest(); }
        }
        [HttpPost, Route("ProrrogarColetaAsync")]
        public async Task<IActionResult> ProrrogarColetaAsync([FromBody] ReservaChopp reserva)
        {
            if (!Request.Headers.ContainsKey("token"))
                return StatusCode(500, "Acesso não autorizado");
            try
            {
                var result = await _reservaDAO.ProrrogarColetaAsync(Request.Headers["token"].ToString(), reserva);
                return Ok(result);
            }
            catch { return BadRequest(); }
        }
        [HttpPost, Route("ColetarChopeiraAsync")]
        public async Task<IActionResult> ColetarChopeiraAsync([FromBody] ReservaChopeira reservaChopeira)
        {
            if (!Request.Headers.ContainsKey("token"))
                return StatusCode(500, "Acesso não autorizado");
            try
            {
                var result = await _reservaDAO.ColetarChopeiraAsync(Request.Headers["token"].ToString(), reservaChopeira);
                return Ok(result);
            }
            catch { return BadRequest(); }
        }
        [HttpPost, Route("ColetarBarrilAsync")]
        public async Task<IActionResult> ColetarBarrilAsync([FromBody] ReservaBarril reservaBarril)
        {
            if (!Request.Headers.ContainsKey("token"))
                return StatusCode(500, "Acesso não autorizado");
            try
            {
                var result = await _reservaDAO.ColetarBarrilAsync(Request.Headers["token"].ToString(), reservaBarril);
                return Ok(result);
            }
            catch { return BadRequest(); }
        }
        [HttpPost, Route("ColetarCilindroAsync")]
        public async Task<IActionResult> ColetarCilindroAsync([FromBody] ReservaCilindro reservaCilindro)
        {
            if (!Request.Headers.ContainsKey("token"))
                return StatusCode(500, "Acesso não autorizado");
            try
            {
                var result = await _reservaDAO.ColetarCilindroAsync(Request.Headers["token"].ToString(), reservaCilindro);
                return Ok(result);
            }
            catch { return BadRequest(); }
        }
        [HttpPost, Route("GravarFotoAsync")]
        public async Task<IActionResult> GravarFotoAsync([FromBody] ReservaFoto foto)
        {
            if (!Request.Headers.ContainsKey("token"))
                return StatusCode(500, "Acesso não autorizado");
            try
            {
                var result = await _reservaDAO.GravarFotoAsync(Request.Headers["token"].ToString(), foto);
                return Ok(result);
            }
            catch { return BadRequest(); }
        }
        [HttpPost, Route("GravarReservaAsync")]
        public async Task<IActionResult> GravarReservaAsync([FromBody] ReservaChopp reserva)
        {
            if (!Request.Headers.ContainsKey("token"))
                return StatusCode(500, "Acesso não autorizado");
            try
            {
                var result = await _reservaDAO.GravarReservaAsync(Request.Headers["token"].ToString(), reserva);
                return Ok(result);
            }
            catch { return BadRequest(); }
        }
    }
}
