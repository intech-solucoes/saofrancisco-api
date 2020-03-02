#region Usings
using Intech.Lib.Web.API;
using Intech.PrevSystem.API;
using Intech.PrevSystem.Entidades;
using Intech.PrevSystem.Negocio.Proxy;
using Microsoft.AspNetCore.Mvc;
using System; 
#endregion

namespace Intech.PrevSystem.Saofrancisco.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalendarioPagamentoController : BaseController
    {
        [HttpGet]
        [Retorno(nameof(CalendarioPagamentoEntidade), true)]
        public IActionResult Listar()
        {
            try
            {
                return Json(new CalendarioPagamentoProxy().Listar());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("porPlano/{cdPlano}")]
        [Retorno(nameof(CalendarioPagamentoEntidade), true)]
        public IActionResult BuscarPorPlano(string cdPlano)
        {
            try
            {
                return Json(new CalendarioPagamentoProxy().BuscarPorPlano(cdPlano));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}