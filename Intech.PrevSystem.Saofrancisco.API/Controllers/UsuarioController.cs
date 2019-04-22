#region Usings
using Intech.Lib.Util.Seguranca;
using Intech.Lib.Web.JWT;
using Intech.PrevSystem.API;
using Intech.PrevSystem.Entidades;
using Intech.PrevSystem.Negocio.Proxy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
#endregion

namespace Intech.PrevSystem.Saofrancisco.API.Controllers
{
    [Route("api/[controller]")]
    public class UsuarioController : BaseController
    {
        [HttpGet]
        [Authorize("Bearer")]
        public IActionResult Get()
        {
            try
            {
                if (CodEntid != null)
                    return Ok();

                return Unauthorized();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("admin")]
        [Authorize("Bearer")]
        public IActionResult GetAdmin()
        {
            try
            {
                if (CodEntid != null)
                {
                    if (Admin)
                        return Json(true);
                    else
                        return Json(false);
                }

                return Unauthorized();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("selecionar")]
        [Authorize("Bearer")]
        public IActionResult Selecionar(
            [FromServices] SigningConfigurations signingConfigurations,
            [FromServices] TokenConfigurations tokenConfigurations,
            [FromBody] dynamic login)
        {
            try
            {
                string cpf = login.Cpf.Value;

                return MontarToken(signingConfigurations, tokenConfigurations, cpf, "", true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult Login(
            [FromServices] SigningConfigurations signingConfigurations,
            [FromServices] TokenConfigurations tokenConfigurations,
            [FromBody] dynamic login)
        {
            try
            {
                string cpf = login.Cpf.Value;
                string senha = login.Senha.Value;

                return MontarToken(signingConfigurations, tokenConfigurations, cpf, senha);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private IActionResult MontarToken(SigningConfigurations signingConfigurations, TokenConfigurations tokenConfigurations, string cpf, string senha, bool semSenha = false)
        {
            var funcionarioProxy = new FuncionarioProxy();

            var usuario = new UsuarioEntidade();

            if (!semSenha)
            {
                usuario = new UsuarioProxy().BuscarPorLogin(cpf, senha);

                if (usuario == null)
                    throw new Exception("Matrícula ou senha incorretos!");
            }

            var pensionista = false;
            string codEntid;
            string seqRecebedor;
            string grupoFamilia;
            var funcionario = funcionarioProxy.BuscarPrimeiroPorCpf(cpf);

            if (funcionario != null)
            {
                codEntid = funcionario.COD_ENTID.ToString();
                seqRecebedor = "0";
                grupoFamilia = "0";
            }
            else
            {
                var recebedorBeneficio = new RecebedorBeneficioProxy().BuscarPensionistaPorCpf(cpf);

                if (recebedorBeneficio == null)
                    throw new Exception("CPF ou senha incorretos!");

                codEntid = recebedorBeneficio.COD_ENTID.ToString();
                funcionario = funcionarioProxy.BuscarPorMatricula(recebedorBeneficio.NUM_MATRICULA);
                pensionista = true;
                seqRecebedor = recebedorBeneficio.SEQ_RECEBEDOR.ToString();
                grupoFamilia = recebedorBeneficio.NUM_SEQ_GR_FAMIL.ToString();
            }

            if (semSenha)
            {
                usuario.NOM_LOGIN = cpf;
                usuario.IND_ADMINISTRADOR = "N";
            }

            if (codEntid != null)
            {
                var dadosPessoais = new DadosPessoaisProxy().BuscarPorCodEntid(codEntid);

                var claims = new List<KeyValuePair<string, string>> {
                    new KeyValuePair<string, string>("Cpf", dadosPessoais.CPF_CGC),
                    new KeyValuePair<string, string>("CodEntid", codEntid),
                    new KeyValuePair<string, string>("Matricula", funcionario.NUM_MATRICULA),
                    new KeyValuePair<string, string>("Inscricao", funcionario.NUM_INSCRICAO),
                    new KeyValuePair<string, string>("CdFundacao", funcionario.CD_FUNDACAO),
                    new KeyValuePair<string, string>("CdEmpresa", funcionario.CD_EMPRESA),
                    new KeyValuePair<string, string>("Pensionista", pensionista.ToString()),
                    new KeyValuePair<string, string>("SeqRecebedor", seqRecebedor),
                    new KeyValuePair<string, string>("GrupoFamilia", grupoFamilia),
                    new KeyValuePair<string, string>("Admin", (usuario.IND_ADMINISTRADOR == "S").ToString())
                };

                var token = AuthenticationToken.Generate(signingConfigurations, tokenConfigurations, usuario.NOM_LOGIN, claims);

                return Json(new
                {
                    token.AccessToken,
                    token.Authenticated,
                    token.Created,
                    token.Expiration,
                    token.Message,
                    Pensionista = pensionista,
                    Admin = usuario.IND_ADMINISTRADOR == "S"
                });
            }

            return Unauthorized();
        }

        [HttpPost("criarAcesso")]
        [AllowAnonymous]
        public IActionResult CriarAcesso([FromBody] dynamic data)
        {
            try
            {
                string cpf = data.Cpf.Value;
                DateTime dataNascimento = data.DataNascimento.Value;

                return Json(new UsuarioProxy().CriarAcesso(cpf, dataNascimento));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("criarAcessoIntech")]
        [AllowAnonymous]
        public IActionResult CriarAcessoIntech([FromBody] dynamic data)
        {
            try
            {
                string cpf = data.Cpf.Value;
                string chave = data.Chave.Value;
                new UsuarioProxy().CriarAcessoIntech(cpf, chave);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("alterarSenha")]
        [Authorize("Bearer")]
        public IActionResult AlterarSenha([FromBody] dynamic data)
        {
            try
            {
                string senhaAntiga = data.senhaAntiga.Value;
                string senhaNova = data.senhaNova.Value;

                return Json(new UsuarioProxy().AlterarSenha(Cpf, senhaAntiga, senhaNova));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}