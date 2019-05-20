#region Usings
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
        [HttpGet("relatorio/{cdPlano}/{referencia}/{cdTipoFolha}")]
        [Authorize("Bearer")]
        public IActionResult GetRelatorio(string cdPlano, string referencia, string cdTipoFolha)
        {
            try
            {
                var dataReferencia = DateTime.ParseExact(referencia, "dd.MM.yyyy", new CultureInfo("pt-BR"));

                var retorno = new
                {
                    Funcionario = new FuncionarioProxy().BuscarPorMatricula(Matricula),
                    Fundacao = new FundacaoProxy().BuscarPorCodigo(CdFundacao),
                    Entidade = new EntidadeProxy().BuscarPorCodEntid(CodEntid),
                    Empresa = new EmpresaProxy().BuscarPorCodigo(CdEmpresa),
                    Plano = new PlanoVinculadoProxy().BuscarPorFundacaoEmpresaMatriculaPlano(CdFundacao, CdEmpresa, Matricula, cdPlano),
                    Ficha = new FichaFinanceiraAssistidoProxy().BuscarRubricasPorFundacaoEmpresaMatriculaPlanoReferencia(CdFundacao, CdEmpresa, Matricula, cdPlano, dataReferencia, cdTipoFolha)
                };

                return Json(retorno);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}