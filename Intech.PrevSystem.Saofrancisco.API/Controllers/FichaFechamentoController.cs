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

        [HttpGet("datasExtrato/{cdPlano}")]
        [Authorize("Bearer")]
        public IActionResult GetDatasExtrato(string cdPlano)
        {
            try
            {
                var dataFinal = new FichaFechamentoProxy().BuscarDataUltimaContrib(CdFundacao, CdEmpresa, cdPlano, Inscricao);

                if (dataFinal == null)
                    dataFinal = DateTime.Now;

                return Json(new
                {
                    //DataInicial = new FichaFechamentoProxy().BuscarDataPrimeiraContrib(CdFundacao, CdEmpresa, cdPlano, Inscricao),
                    DataInicial = new PlanoVinculadoProxy().BuscarPorFundacaoEmpresaMatriculaPlano(CdFundacao, CdEmpresa, Matricula, cdPlano).DT_INSC_PLANO,
                    DataFinal = dataFinal
                });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}