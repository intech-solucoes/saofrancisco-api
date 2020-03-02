#region Usings
using Intech.Lib.Web.API;
using Intech.PrevSystem.API;
using Intech.PrevSystem.Entidades;
using Intech.PrevSystem.Negocio.Proxy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
#endregion

namespace Intech.PrevSystem.Saofrancisco.API.Controllers
{
    [Route("api/[controller]")]
    public class DependenteController : BaseController
    {
        [HttpGet]
        [Authorize("Bearer")]
        [Retorno(nameof(DependenteEntidade), true)]
        public IActionResult Buscar()
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

        [HttpGet("[action]/{id}")]
        [Authorize("Bearer")]
        [Retorno(nameof(DependenteEntidade), true)]
        public IActionResult BuscarPorID(int id)
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