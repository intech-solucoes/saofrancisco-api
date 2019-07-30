#region Usings
using Intech.Lib.Email;
using Intech.Lib.Web;
using Intech.PrevSystem.API;
using Intech.PrevSystem.Negocio.Proxy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
#endregion

namespace Intech.PrevSystem.Saofrancisco.API.Controllers
{
    [Route(RotasApi.FichaFinanceiraAssistido)]
    public class FichaFinanceiraAssistidoController : BaseFichaFinanceiraAssistidoController
    {
        //[HttpGet("relatorio/{cdPlano}/{referencia}/{cdTipoFolha}")]
        //[Authorize("Bearer")]
        //public IActionResult GetRelatorio(string cdPlano, string referencia, string cdTipoFolha)
        //{
        //    try
        //    {
        //        var dataReferencia = DateTime.ParseExact(referencia, "dd.MM.yyyy", new CultureInfo("pt-BR"));

        //        var retorno = new
        //        {
        //            Funcionario = new FuncionarioProxy().BuscarPorMatricula(Matricula),
        //            Fundacao = new FundacaoProxy().BuscarPorCodigo(CdFundacao),
        //            Entidade = new EntidadeProxy().BuscarPorCodEntid(CodEntid),
        //            Empresa = new EmpresaProxy().BuscarPorCodigo(CdEmpresa),
        //            Plano = new PlanoVinculadoProxy().BuscarPorFundacaoEmpresaMatriculaPlano(CdFundacao, CdEmpresa, Matricula, cdPlano),
        //            Ficha = new FichaFinanceiraAssistidoProxy().BuscarRubricasPorFundacaoEmpresaMatriculaPlanoReferencia(CdFundacao, CdEmpresa, Matricula, cdPlano, dataReferencia, cdTipoFolha)
        //        };

        //        return Json(retorno);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        [HttpGet("relatorio/{cdPlano}/{referencia}/{cdTipoFolha}/{enviarPorEmail}")]
        [Authorize("Bearer")]
        public IActionResult GetRelatorio(string cdPlano, string referencia, string cdTipoFolha, bool enviarPorEmail)
        {
            try
            {
                var dataReferencia = DateTime.ParseExact(referencia, "dd.MM.yyyy", new CultureInfo("pt-BR"));

                var funcionario = new FuncionarioProxy().BuscarPorMatricula(Matricula);
                var fichaFinanceiraAssistido = new FichaFinanceiraAssistidoProxy().BuscarRubricasPorFundacaoEmpresaMatriculaPlanoReferencia(funcionario.CD_FUNDACAO, funcionario.CD_EMPRESA, Matricula, cdPlano, dataReferencia, cdTipoFolha);
                var entidade = new EntidadeProxy().BuscarPorCodEntid(CodEntid);
                var empresa = new EmpresaProxy().BuscarPorCodigo(funcionario.CD_EMPRESA);
                var plano = new PlanoVinculadoProxy().BuscarPorFundacaoEmpresaMatriculaPlano(funcionario.CD_FUNDACAO, funcionario.CD_EMPRESA, Matricula, cdPlano);

                var relatorio = new Relatorios.ContraCheque();
                relatorio.GerarRelatorio(fichaFinanceiraAssistido, entidade, funcionario, empresa, plano);

                using (MemoryStream ms = new MemoryStream())
                {
                    relatorio.ExportToPdf(ms);

                    // Clona stream pois o método ExportToPdf fecha a atual
                    var pdfStream = new MemoryStream();
                    pdfStream.Write(ms.ToArray(), 0, ms.ToArray().Length);
                    pdfStream.Position = 0;

                    var filename = $"Contracheque - {dataReferencia.ToString("dd/MM/yyyy")}.pdf";

                    if (enviarPorEmail)
                    {
                        var dados = new DadosPessoaisProxy().BuscarPorCodEntid(CodEntid);
                        var emailConfig = AppSettings.Get().Email;
                        EnvioEmail.EnviarMailKit(emailConfig, dados.EMAIL_AUX, $"Contracheque - {dataReferencia.ToString("dd/MM/yyyy")}", "", pdfStream, filename);

                        return Json($"Contracheque enviado com sucesso para o e-mail {dados.EMAIL_AUX}");
                    }
                    else
                    {
                        return File(pdfStream, "application/pdf", filename);
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}