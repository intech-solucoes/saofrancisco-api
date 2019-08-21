﻿#region Usings
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
using System.Linq;
#endregion

namespace Intech.PrevSystem.Saofrancisco.API.Controllers
{
    /// <summary>
    /// Usuario Controller
    /// Rota Base: /usuario
    /// </summary>
    [Route("api/[controller]")]
    public class UsuarioController : BaseController
    {
        /// <summary>
        /// Verifica se o usuário atual está logado.
        /// 
        /// Rota: [GET] /usuario
        /// </summary>
        /// <returns>200 OK</returns>
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

        /// <summary>
        /// Verifica se o usuário é administrador.
        /// 
        /// Rota: [GET] /usuario/admin
        /// </summary>
        /// <returns>Retorna true caso o usuário seja administrador</returns>
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

        /// <summary>
        /// Seleciona um participante por cpf.
        /// 
        /// Rota: [POST] /usuario/selecionar
        /// </summary>
        /// <param name="signingConfigurations">Parâmetro preenchido por injeção de dependência</param>
        /// <param name="tokenConfigurations">Parâmetro preenchido por injeção de dependência</param>
        /// <param name="login">{ Cpf: "12345678901" }</param>
        /// <returns>Retorna o token da sessão do participante.</returns>
        [HttpPost("selecionar")]
        [Authorize("Bearer")]
        public IActionResult Selecionar(
            [FromServices] SigningConfigurations signingConfigurations,
            [FromServices] TokenConfigurations tokenConfigurations,
            [FromBody] LoginEntidade login)
        {
            try
            {
                return MontarToken(signingConfigurations, tokenConfigurations, login.Cpf, "", true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// Realiza login com usuário e senha do participante.
        /// 
        /// Rota: [POST] /usuario/login
        /// </summary>
        /// <param name="signingConfigurations">Parâmetro preenchido por injeção de dependência</param>
        /// <param name="tokenConfigurations">Parâmetro preenchido por injeção de dependência</param>
        /// <param name="login">{ Cpf: "12345678901", Senha: "123" }</param>
        /// <returns>Retorna o token da sessão do participante.</returns>
        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult Login(
            [FromServices] SigningConfigurations signingConfigurations,
            [FromServices] TokenConfigurations tokenConfigurations,
            [FromBody] LoginEntidade login)
        {
            try
            {
                return MontarToken(signingConfigurations, tokenConfigurations, login.Cpf, login.Senha);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Cria acesso do participante, enviando a nova senha para o e-mail do participante.
        /// 
        /// Rota: [POST] /usuario/criarAcesso
        /// </summary>
        /// <param name="data">{ Cpf: "12345678901", DataNascimento: "01/01/0001" }</param>
        /// <returns>retorna a mensagem de criação do novo acesso.</returns>
        [HttpPost("criarAcesso")]
        [AllowAnonymous]
        public IActionResult CriarAcesso([FromBody] LoginEntidade data)
        {
            try
            {
                return Json(new UsuarioProxy().CriarAcesso(data.Cpf, data.DataNascimento));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Cria acesso para usuários internos.
        /// </summary>
        /// <param name="data">{ Cpf: "12345678901", Chave: "123" }</param>
        /// <returns>200 OK</returns>
        [HttpPost("criarAcessoIntech")]
        [AllowAnonymous]
        public IActionResult CriarAcessoIntech([FromBody] LoginEntidade data)
        {
            try
            {
                new UsuarioProxy().CriarAcessoIntech(data.Cpf, data.Chave);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Altera a senha do participante logado.
        /// </summary>
        /// <param name="data">{ SenhaAntiga: "123", SenhaNova: "123" }</param>
        /// <returns>Retorna a mensagem de alteração efetuada com sucesso.</returns>
        [HttpPost("alterarSenha")]
        [Authorize("Bearer")]
        public IActionResult AlterarSenha([FromBody] LoginEntidade data)
        {
            try
            {
                return Json(new UsuarioProxy().AlterarSenha(Cpf, data.SenhaAntiga, data.SenhaNova));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Altera a senha do participante para o primeiro acesso.
        /// </summary>
        /// <param name="data">{ SenhaNova: "123" }</param>
        /// <returns>Retorna a mensagem de alteração efetuada com sucesso.</returns>
        [HttpPost("alterarSenhaPrimeiroAcesso")]
        [Authorize("Bearer")]
        public IActionResult AlterarSenhaPrimeiroAcesso([FromBody] LoginEntidade data)
        {
            try
            {
                return Json(new UsuarioProxy().AlterarSenhaPrimeiroAcesso(Cpf, data.SenhaNova));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("matriculas")]
        [Authorize("Bearer")]
        public IActionResult BuscarMatriculas()
        {
            try
            {
                var dados = new DadosPessoaisProxy().BuscarPorCodEntid(CodEntid);
                var matriculas = new FuncionarioProxy().BuscarPorPesquisa(null, null, null, null, null, dados.NOME_ENTID);

                var listaMatriculas = matriculas.GroupBy(x => x.NUM_MATRICULA).Select(x => x.Key).ToList();

                return Json(listaMatriculas);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("selecionarMatricula/{matricula}")]
        [Authorize("Bearer")]
        public IActionResult SelecionarMatricula(
            [FromServices] SigningConfigurations signingConfigurations,
            [FromServices] TokenConfigurations tokenConfigurations,
            string matricula)
        {
            try
            {
                var funcionario = new FuncionarioProxy().BuscarPrimeiroPorCpf(Cpf);

                if (funcionario.NUM_MATRICULA != matricula)
                    funcionario = null;

                var pensionista = false;
                string codEntid;
                string seqRecebedor;
                string grupoFamilia;
                string codEntidFuncionario = "";

                if (funcionario != null)
                {
                    codEntid = funcionario.COD_ENTID.ToString();
                    seqRecebedor = "0";
                    grupoFamilia = "0";
                }
                else
                {
                    var recebedorBeneficio = new RecebedorBeneficioProxy().BuscarPensionistaPorCpf(Cpf).First();

                    if (recebedorBeneficio == null)
                        throw new Exception("CPF ou senha incorretos!");

                    codEntid = recebedorBeneficio.COD_ENTID.ToString();
                    funcionario = new FuncionarioProxy().BuscarPorMatricula(recebedorBeneficio.NUM_MATRICULA);
                    codEntidFuncionario = funcionario.COD_ENTID.ToString();
                    pensionista = true;
                    seqRecebedor = recebedorBeneficio.SEQ_RECEBEDOR.ToString();
                    grupoFamilia = recebedorBeneficio.NUM_SEQ_GR_FAMIL.ToString();
                }

                if (codEntid != null)
                {
                    var dadosPessoais = new DadosPessoaisProxy().BuscarPorCodEntid(codEntid);
                    var claims = new List<KeyValuePair<string, string>> {
                        new KeyValuePair<string, string>("Cpf", Cpf),
                        new KeyValuePair<string, string>("CodEntid", codEntid),
                        new KeyValuePair<string, string>("CodEntidFuncionario", codEntidFuncionario),
                        new KeyValuePair<string, string>("Matricula", funcionario.NUM_MATRICULA),
                        new KeyValuePair<string, string>("Inscricao", funcionario.NUM_INSCRICAO),
                        new KeyValuePair<string, string>("CdFundacao", funcionario.CD_FUNDACAO),
                        new KeyValuePair<string, string>("CdEmpresa", funcionario.CD_EMPRESA),
                        new KeyValuePair<string, string>("Pensionista", pensionista.ToString()),
                        new KeyValuePair<string, string>("SeqRecebedor", seqRecebedor.ToString()),
                        new KeyValuePair<string, string>("GrupoFamilia", grupoFamilia),
                        new KeyValuePair<string, string>("Admin", Admin ? "S" : "N")
                    };

                    var token = AuthenticationToken.Generate(signingConfigurations, tokenConfigurations, Cpf, claims);

                    return Json(new
                    {
                        token.AccessToken,
                        token.Authenticated,
                        token.Created,
                        token.Expiration,
                        token.Message,
                        Pensionista,
                        Admin = Admin ? "S" : "N"
                    });
                }
                else
                {
                    return Unauthorized();
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        //[HttpGet("menu")]
        //[Authorize("Bearer")]
        //public IActionResult Menu()
        //{
        //    try
        //    {
        //        var dadosPlano = new PlanoVinculadoProxy().BuscarPorFundacaoMatricula(CdFundacao, Matricula);

        //        var menuAtivos = new List<string> {
        //            "home",
        //            "dados",
        //            "plano",
        //            "emprestimos",
        //            "trocarSenha",
        //            "relacionamento"
        //        };

        //        var menuAssistidos = new List<string> {
        //            "home",
        //            "dados",
        //            "beneficios",
        //            "emprestimos",
        //            "trocarSenha",
        //            "relacionamento"
        //        };

        //        if (dadosPlano.IsAtivo())
        //            return Json(menuAtivos);
        //        else
        //            return Json(menuAssistidos);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        #region Métodos Provados

        private IActionResult MontarToken(SigningConfigurations signingConfigurations, TokenConfigurations tokenConfigurations, string cpf, string senha, bool semSenha = false)
        {
            var funcionarioProxy = new FuncionarioProxy();

            var usuario = new UsuarioEntidade();

            if (!semSenha)
            {
                usuario = new UsuarioProxy().BuscarPorLogin(cpf, senha);

                if (usuario == null)
                    throw new Exception("CPF ou senha incorretos!");
            }

            var pensionista = false;
            string codEntid;
            string seqRecebedor;
            string grupoFamilia;
            string codEntidFuncionario = "";
            var funcionario = funcionarioProxy.BuscarPrimeiroPorCpf(cpf);

            if (funcionario != null)
            {
                codEntid = funcionario.COD_ENTID.ToString();
                seqRecebedor = "0";
                grupoFamilia = "0";
            }
            else
            {
                var recebedorBeneficio = new RecebedorBeneficioProxy().BuscarPensionistaPorCpf(cpf).First();

                if (recebedorBeneficio == null)
                    throw new Exception("CPF ou senha incorretos!");

                codEntid = recebedorBeneficio.COD_ENTID.ToString();
                funcionario = funcionarioProxy.BuscarPorMatricula(recebedorBeneficio.NUM_MATRICULA);
                codEntidFuncionario = funcionario.COD_ENTID.ToString();
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
                    new KeyValuePair<string, string>("CodEntidFuncionario", codEntidFuncionario),
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

        #endregion
    }
}