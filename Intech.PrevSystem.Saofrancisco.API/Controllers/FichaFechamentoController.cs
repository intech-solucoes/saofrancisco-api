using Intech.Lib.Util.Date;
using Intech.PrevSystem.API;
using Intech.PrevSystem.Negocio.Proxy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

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
                var dataInicial = DateTime.Now;
                var dataFinal = DateTime.Now;

                if (cdPlano == "0003")
                {
                    var listaFicha = new FichaFinanceiraProxy().BuscarPorFundacaoPlanoInscricao(CdFundacao, cdPlano, Inscricao).ToList();

                    var ultimaFicha = listaFicha.Last();
                    dataInicial = new DateTime(Convert.ToInt32(ultimaFicha.ANO_REF), Convert.ToInt32(ultimaFicha.MES_REF), 1);

                    var primeiraFicha = listaFicha.First();
                    dataFinal = new DateTime(Convert.ToInt32(primeiraFicha.ANO_REF), Convert.ToInt32(primeiraFicha.MES_REF), 1);
                }
                else
                {
                    dataInicial = new PlanoVinculadoProxy().BuscarPorFundacaoEmpresaMatriculaPlano(CdFundacao, CdEmpresa, Matricula, cdPlano).DT_INSC_PLANO;
                    dataFinal = new FichaFechamentoProxy().BuscarDataUltimaContrib(CdFundacao, CdEmpresa, cdPlano, Inscricao);
                }

                return Json(new DatasInicialFinal
                {
                    DataInicial = dataInicial,
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