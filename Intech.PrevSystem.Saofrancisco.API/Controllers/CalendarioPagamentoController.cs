#region Usings
using Intech.PrevSystem.API;
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
        public IActionResult Get()
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
    }
}