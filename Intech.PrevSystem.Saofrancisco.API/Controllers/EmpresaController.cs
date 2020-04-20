using Intech.Lib.Web.API;
using Intech.PrevSystem.API;
using Intech.PrevSystem.Entidades;
using Intech.PrevSystem.Negocio.Proxy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Intech.PrevSystem.Saofrancisco.API.Controllers
{
    /// <service nome="Empresa" />
    [Route(RotasApi.Empresa)]
    public class EmpresaController : BaseEmpresaController
    {
        /// <rota caminho="[action]" tipo="GET" />
        /// <retorno tipo="EmpresaEntidade" lista="true" />
        [HttpGet("[action]")]
        [Retorno(nameof(CalendarioPagamentoEntidade), true)]
        [Authorize("Bearer")]
        public IActionResult BuscarTodas() {
            try
            {
                return Json(new EmpresaProxy().BuscarTodasComSiglaEntid());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}