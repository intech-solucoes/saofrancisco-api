#region Usings
using Intech.PrevSystem.API;
using Intech.PrevSystem.Negocio.Proxy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

#endregion
namespace Intech.PrevSystem.Saofrancisco.API.Controllers
{
    [Route(RotasApi.ProcessoBeneficio)]
    [ApiController]
    public class ProcessoBeneficioController : BaseProcessoBeneficioController
    {
        //[HttpGet("porPlanoCDSaoFrancisco")]
        //[Authorize("Bearer")]
        //public IActionResult GetPorPlano()
        //{
        //    try
        //    {
        //        return Json(new ProcessoBeneficioProxy().BuscarPorFundacaoEmpresaInscricaoPlano(CdFundacao, CdEmpresa, Matricula, "0002"));
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
    }
}