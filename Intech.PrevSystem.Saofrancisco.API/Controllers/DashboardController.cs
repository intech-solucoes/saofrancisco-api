#region Usings
using Intech.PrevSystem.API;
using Intech.PrevSystem.Negocio.Saofrancisco;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
#endregion

namespace Intech.PrevSystem.Saofrancisco.API.Controllers
{
    [Route(RotasApi.Dashboard)]
    public class DashboardController : BaseController
    {

        [HttpGet("{cdPlano}")]
        [Authorize("Bearer")]
        public IActionResult Get(string cdPlano)
        {

            return Json( new Dashboard().Buscar(CdFundacao, cdPlano, Inscricao));
        }
    }
}