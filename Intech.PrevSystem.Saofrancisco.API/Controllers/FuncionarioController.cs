#region Usings
using Intech.PrevSystem.API;
using Intech.PrevSystem.Negocio.Proxy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
#endregion

namespace Intech.PrevSystem.Saofrancisco.API.Controllers
{
    [Route(RotasApi.Funcionario)]
    public class FuncionarioController : BaseController
    {
        [HttpGet]
        [Authorize("Bearer")]
        public IActionResult Buscar()
        {
            try
            {
                return Json(new FuncionarioProxy().BuscarDadosPorCodEntidEmpresa(CodEntid, CodEntidFuncionario, CdEmpresa));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("pesquisar")]
        [Authorize("Bearer")]
        public IActionResult Pesquisar([FromBody] PesquisaDados dados)
        {
            try
            {
                //dados.CD_FUNDACAO = string.IsNullOrEmpty(dados.CD_FUNDACAO) ? null : dados.CD_FUNDACAO;
                //dados.CD_EMPRESA = string.IsNullOrEmpty(dados.CD_EMPRESA) ? null : dados.CD_EMPRESA;
                //dados.CD_PLANO = string.IsNullOrEmpty(dados.CD_PLANO) ? null : dados.CD_PLANO;
                //dados.CD_SIT_PLANO = string.IsNullOrEmpty(dados.CD_SIT_PLANO) ? null : dados.CD_SIT_PLANO;
                //dados.NUM_MATRICULA = string.IsNullOrEmpty(dados.NUM_MATRICULA) ? null : dados.NUM_MATRICULA;
                //dados.NOME = string.IsNullOrEmpty(dados.NOME) ? null : dados.NOME;

                var pesquisa = new FuncionarioProxy().BuscarPorPesquisa(dados.CD_FUNDACAO, dados.CD_EMPRESA, dados.CD_PLANO, dados.CD_SIT_PLANO, dados.NUM_MATRICULA, dados.NOME);

                return Json(pesquisa);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

    public class PesquisaDados
    {
        public string CD_FUNDACAO { get; set; }
        public string CD_EMPRESA { get; set; }
        public string CD_PLANO { get; set; }
        public string CD_SIT_PLANO { get; set; }
        public string NUM_MATRICULA { get; set; }
        public string NOME { get; set; }
    }
}