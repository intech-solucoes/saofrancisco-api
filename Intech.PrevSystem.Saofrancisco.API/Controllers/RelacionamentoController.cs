﻿#region Usings
using System;
using System.Linq;
using Intech.Lib.Email;
using Intech.Lib.Web;
using Intech.Lib.Web.API;
using Intech.PrevSystem.API;
using Intech.PrevSystem.Entidades;
using Intech.PrevSystem.Negocio.Proxy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration; 
#endregion

namespace Intech.PrevSystem.Sabesprev.Api.Controllers
{
    [Route("[controller]")]
    public class RelacionamentoController : BaseController
    {
        [HttpPost]
        [Authorize("Bearer")]
        [Retorno("any")]
        public IActionResult Enviar([FromBody]RelacionamentoEntidade relacionamentoEntidade)
        {
            try
            {
                string nome;
                if (!NaoParticipante)
                    nome = new DadosPessoaisProxy().BuscarPorCodEntid(CodEntid).NOME_ENTID;
                else
                    nome = new FuncionarioNPProxy().BuscarPorMatricula(Matricula).FirstOrDefault().NOME_ENTID;
                
                var mensagem = $"E-mail: <b>{relacionamentoEntidade.Email}</b><br/>" +
                    $"Nome Completo: <b>{nome}</b>:br/>" +
                    $"Matrícula: <b>{Matricula}</b><br/>" +
                    $"<br/>" +
                    $"{relacionamentoEntidade.Mensagem}";
                var emailConfig = AppSettings.Get().Email;
                EnvioEmail.Enviar(emailConfig, emailConfig.EmailRelacionamento, $"São Francisco - {relacionamentoEntidade.Assunto}", mensagem);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest($"Ocorreu um erro ao enviar socilitação. {ex.Message}");
            }
        }
    }
}