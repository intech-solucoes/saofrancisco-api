using Intech.PrevSystem.API;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Intech.PrevSystem.Saofrancisco.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RelatoriosController : BaseController
    {
        //[HttpPost]
        //[AllowAnonymous]
        //public IActionResult Get([FromBody] dynamic content)
        //{
        //    try
        //    {
        //        var html = (string)content.html;

        //        var pdf = OpenHtmlToPdf.Pdf
        //            .From(html)
        //            //.WithGlobalSetting("orientation", "Landscape")
        //            .WithObjectSetting("web.defaultEncoding", "utf-8")
        //            .WithObjectSetting("footer.right", "[page]/[topage]")
        //            .Content();

        //        return File(pdf, "application/pdf");
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ex.InnerException != null)
        //            return BadRequest(ex.InnerException.Message);

        //        return BadRequest(ex.Message);
        //    }
        //}
    }
}