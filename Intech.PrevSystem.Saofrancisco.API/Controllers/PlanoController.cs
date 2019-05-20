#region Usings
using Intech.PrevSystem.API;
using Intech.PrevSystem.Negocio.Proxy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Globalization;
#endregion

namespace Intech.PrevSystem.Saofrancisco.API.Controllers
{
    [Route(RotasApi.Plano)]
    public class PlanoController : BasePlanoController
    {
        [HttpGet("relatorioExtratoPorPlanoReferencia/{cdPlano}/{dtInicio}/{dtFim}")]
        [Authorize("Bearer")]
        public IActionResult GetRelatorioExtratoPorPlanoReferencia(string cdPlano, string dtInicio, string dtFim)
        {
            try
            {
                var dataInicio = DateTime.ParseExact(dtInicio, "dd.MM.yyyy", new CultureInfo("pt-BR"));
                var dataFim = DateTime.ParseExact(dtFim, "dd.MM.yyyy", new CultureInfo("pt-BR"));

                string AnoRefMesRefInicio = dataInicio.ToString("yyyyMM");
                string AnoRefMesRefFim = dataFim.ToString("yyyyMM");

                var retorno = new
                {
                    Funcionario = new FuncionarioProxy().BuscarPorCodEntid(CodEntid),
                    Fundacao = new FundacaoProxy().BuscarPorCodigo(CdFundacao),
                    Empresa = new EmpresaProxy().BuscarPorCodigo(CdEmpresa),
                    Plano = new PlanoVinculadoProxy().BuscarPorFundacaoEmpresaMatriculaPlano(CdFundacao, CdEmpresa, Matricula, cdPlano),
                    Ficha = new FichaFechamentoProxy()
                        .BuscarRelatorioPorFundacaoEmpresaPlanoInscricaoReferencia(CdFundacao, CdEmpresa, cdPlano, Inscricao, AnoRefMesRefInicio, AnoRefMesRefFim)
                };

                retorno.Fundacao.CEP_ENTID = retorno.Fundacao.CEP_ENTID.AplicarMascara(Mascaras.CEP);
                retorno.Fundacao.CPF_CGC = retorno.Fundacao.CPF_CGC.AplicarMascara(Mascaras.CNPJ);

                return Json(retorno);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}