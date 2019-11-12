#region Usings
using System;
using Intech.Lib.Email;
using Intech.PrevSystem.API;
using Intech.PrevSystem.Entidades;
using Intech.PrevSystem.Negocio.Proxy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration; 
#endregion

namespace Intech.PrevSystem.Sabesprev.Api.Controllers
{
    [Route("api/[controller]")]
    public class RelacionamentoController : BaseController
    {
        private IConfiguration Config;

        public RelacionamentoController(IConfiguration configuration)
        {
            Config = configuration;
        }

        [HttpPost]
        public IActionResult Post([FromBody]RelacionamentoEntidade relacionamentoEntidade)
        {
            try
            {
                var dados = new DadosPessoaisProxy().BuscarPorCodEntid(CodEntid);
                
                var mensagem = $"E-mail: <b>{relacionamentoEntidade.Email}</b><br/>" +
                    $"Nome Completo: <b>{dados.NOME_ENTID}</b>:br/>" +
                    $"Matrícula: <b>{Matricula}</b><br/>" +
                    $"<br/>" +
                    $"{relacionamentoEntidade.Mensagem}";
                var emailConfig = Config.GetSection("Email").Get<ConfigEmail>();
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