using Intech.PrevSystem.API;
using Intech.PrevSystem.Negocio.Proxy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Intech.PrevSystem.Saofrancisco.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FichaFechamentoController : BaseController
    {
        [HttpGet("porPlano/{cdPlano}")]
        [Authorize("Bearer")]
        public IActionResult Get(string cdPlano)
        {
            try
            {
                return Json(new FichaFechamentoProxy().BuscarSaldoPorFundacaoEmpresaPlanoInscricao(CdFundacao, CdEmpresa, cdPlano, Inscricao));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}