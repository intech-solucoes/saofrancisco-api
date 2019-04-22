#region Usings
using System;
using Intech.Lib.Util.Email;
using Intech.PrevSystem.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration; 
#endregion

namespace Intech.PrevSystem.Sabesprev.Api.Controllers
{
    [Route("api/[controller]")]
    public class RelacionamentoController : Controller
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
                var emailConfig = Config.GetSection("Email").Get<ConfigEmail>();
                EnvioEmail.Enviar(emailConfig, emailConfig.EmailRemetente, $"São francisco Mobile - {relacionamentoEntidade.Assunto}", $"Mensagem de <b>{relacionamentoEntidade.Email}</b>:<br/><br/>{relacionamentoEntidade.Mensagem}");
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest("Ocorreu um erro ao enviar socilitação.");
            }
        }
    }
}
