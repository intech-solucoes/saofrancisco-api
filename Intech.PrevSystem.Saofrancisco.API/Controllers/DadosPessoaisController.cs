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
    [Route(RotasApi.DadosPessoais)]
    public class DadosPessoaisController : BaseController
    {
        [HttpGet]
        [Authorize("Bearer")]
        [Retorno(nameof(DadosPessoaisEntidade))]
        public IActionResult Buscar()
        {
            try
            {
                return Ok(new DadosPessoaisProxy().BuscarPorCodEntid(CodEntid));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}