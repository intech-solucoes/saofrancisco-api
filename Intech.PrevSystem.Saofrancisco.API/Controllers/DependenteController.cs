#region Usings
using Intech.PrevSystem.API;
using Intech.PrevSystem.Negocio.Proxy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
#endregion

namespace Intech.PrevSystem.Saofrancisco.API.Controllers
{
    [Route(RotasApi.Depentende)]
    public class DependenteController : BaseController
    {
        [HttpGet]
        [Authorize("Bearer")]
        public IActionResult Get()
        {
            try
            {
                var funcionario = new FuncionarioProxy().BuscarPorCodEntid(CodEntid);

                return Json(new DependenteProxy().BuscarPorFundacaoInscricao(funcionario.CD_FUNDACAO, funcionario.NUM_INSCRICAO));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}