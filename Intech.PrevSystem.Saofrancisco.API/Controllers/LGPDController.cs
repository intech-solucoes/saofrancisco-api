using Intech.PrevSystem.API;
using Intech.PrevSystem.Entidades;
using Intech.PrevSystem.Entidades.Constantes;
using Intech.PrevSystem.Negocio.Proxy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Intech.PrevSystem.Saofrancisco.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LGPDController : BaseController
    {
        [HttpGet]
        [Authorize("Bearer")]
        public IActionResult BuscarPorCPF()
        {
            try
            {
                return Json(new LGPDConsentimentoProxy().BuscarPorCPF(Cpf).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{origem}")]
        [Authorize("Bearer")]
        public IActionResult Inserir(int origem)
        {
            try
            {
                var data = DateTime.Now;

                var lgpd = new LGPDConsentimentoEntidade
                {
                    CD_FUNDACAO = CdFundacao,
                    COD_IDENTIFICADOR = $"{data.ToString("ddMMyyyyhhmmss")}{Cpf}",
                    COD_CPF = Cpf,
                    DTA_CONSENTIMENTO = data,
                    TXT_DISPOSITIVO = "",
                    TXT_IPV4 = "",
                    TXT_IPV6 = "",
                    TXT_ORIGEM = DMN_SISTEMA_ORIGEM.Valor(origem)
                };
                new LGPDConsentimentoProxy().Insert(lgpd);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}