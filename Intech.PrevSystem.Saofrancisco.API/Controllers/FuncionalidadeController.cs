using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Intech.Lib.Web.API;
using Intech.PrevSystem.API;
using Intech.PrevSystem.Entidades;
using Intech.PrevSystem.Negocio.Proxy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Intech.PrevSystem.Saofrancisco.API.Controllers
{
    /// <service nome="Funcionalidade" />
    [Route(RotasApi.Funcionalidade)]
    public class FuncionalidadeController : BaseController
    {
        /// <rota caminho="[action]" tipo="GET" />
        /// <retorno tipo="WebBloqueioFuncEntidade" lista="true" />
        [HttpGet("[action]")]
        [Retorno(nameof(WebBloqueioFuncEntidade), true)]
        [Authorize("Bearer")]
        public IActionResult Buscar()
        {
            try
            {
                return Ok(new WebBloqueioFuncProxy().BuscarJoinPlanoEmpresaEntidade());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <rota caminho="[action]/{IND_ATIVO}" tipo="GET" />
        /// <parametros>
        ///     <parametro nome="IND_ATIVO" tipo="string" />
        /// </parametros>
        /// <retorno tipo="FuncionalidadeEntidade" lista="true" />
        [HttpGet("[action]/{IND_ATIVO}")]
        [Retorno(nameof(FuncionarioEntidade), true)]
        [Authorize("Bearer")]
        public IActionResult BuscarPorIndAtivo(string IND_ATIVO)
        {
            try
            {
                return Ok(new FuncionalidadeProxy().BuscarPorIndAtivo(IND_ATIVO));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <rota caminho="[action]" tipo="POST" />
        /// <parametros>
        ///     <parametro nome="func" tipo="WebBloqueioFuncEntidade" />
        /// </parametros>
        /// <retorno tipo="string" />
        [HttpPost("[action]")]
        [Retorno(nameof(String), true)]
        [Authorize("Bearer")]
        public IActionResult Bloquear([FromBody] WebBloqueioFuncEntidade func)
        {
            try
            {
                var dataAtual = DateTime.Now;

                func.DTA_CRIACAO = dataAtual;
                func.DTA_INICIO = dataAtual;

                new WebBloqueioFuncProxy().Inserir(func);

                return Ok("Bloqueio cadastrado com sucesso");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <rota caminho="[action]" tipo="POST" />
        /// <parametros>
        ///     <parametro nome="func" tipo="WebBloqueioFuncEntidade" />
        /// </parametros>
        /// <retorno tipo="any" />
        [HttpPost("[action]")]
        [Retorno(nameof(String), true)]
        [Authorize("Bearer")]
        public IActionResult Desbloquear([FromBody] WebBloqueioFuncEntidade func)
        {
            try
            {
                func.DTA_FIM = DateTime.Now;
                new WebBloqueioFuncProxy().Atualizar(func);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <rota caminho="[action]/{NUM_FUNCIONALIDADE}/{CD_PLANO}/{CD_PLANO2}/{CD_PLANO3}" tipo="GET" />
        /// <parametros>
        ///     <parametro nome="NUM_FUNCIONALIDADE" tipo="number" />
        ///     <parametro nome="CD_PLANO" tipo="string" />
        ///     <parametro nome="CD_PLANO2" tipo="string" />
        ///     <parametro nome="CD_PLANO3" tipo="string" />
        /// </parametros>
        /// <retorno tipo="string" />
        [HttpGet("[action]/{NUM_FUNCIONALIDADE}/{CD_PLANO}/{CD_PLANO2}/{CD_PLANO3}")]
        [Retorno(nameof(String), true)]
        [Authorize("Bearer")]
        public IActionResult BuscarBloqueiosPorNumFuncionalidade(decimal NUM_FUNCIONALIDADE, string CD_PLANO, string CD_PLANO2, string CD_PLANO3)
        {
            try
            {
                var dataFim = DateTime.Now;
                var bloqueios = new WebBloqueioFuncProxy().BuscarPorCdFundacaoNumFuncionalidadeCdEmpresaCdPlanoNumMatriculaOrderByDtaFim(CdFundacao, NUM_FUNCIONALIDADE, CdEmpresa, Matricula, CD_PLANO, CD_PLANO2, CD_PLANO3, dataFim).FirstOrDefault();
                var res = bloqueios != null ? bloqueios.TXT_MOTIVO_BLOQUEIO : "";
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}