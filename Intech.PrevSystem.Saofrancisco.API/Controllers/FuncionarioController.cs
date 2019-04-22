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
    public class FuncionarioController : BaseFuncionarioController
    {
        [HttpPost("pesquisar")]
        [Authorize("Bearer")]
        public IActionResult Pesquisar([FromBody] PesquisaDados dados)
        {
            try
            {
                return Json(new FuncionarioProxy().BuscarPorPesquisa(dados.CD_FUNDACAO, dados.CD_EMPRESA, dados.CD_PLANO, dados.CD_SIT_PLANO, dados.NUM_MATRICULA, dados.NOME));
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