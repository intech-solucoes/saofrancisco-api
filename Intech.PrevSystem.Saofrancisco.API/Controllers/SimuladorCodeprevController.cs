using Intech.PrevSystem.API;
using Intech.PrevSystem.Negocio.Proxy;
using Intech.PrevSystem.Negocio.Saofrancisco.Simuladores;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Intech.PrevSystem.Saofrancisco.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SimuladorCodeprevController : BaseController
    {
        [HttpPost("[action]")]
        [Authorize("Bearer")]
        public IActionResult Simular(SimuladorBeneficioCodeprevDados dados)
        {
            try
            {
                DateTime DataNascimento;

                if (!NaoParticipante)
                    DataNascimento = new DadosPessoaisProxy().BuscarPorCodEntid(CodEntid).DT_NASCIMENTO;
                else
                    DataNascimento = new FuncionarioNPProxy().BuscarDadosNaoParticipantePorMatriculaEmpresa(Matricula, CdEmpresa).Funcionario.DT_NASCIMENTO.Value;

                var simulador = new SimuladorBeneficioCodeprev();
                decimal saldoProjetado, saque, saldoBeneficio,
                    saldoProjetado8 = 0M, saque8 = 0M, saldoBeneficio8 = 0M;
                var memoriaCalculo = simulador.Calcular(dados, dados.PercentualContrib, DataNascimento, CdEmpresa, out saldoProjetado, out saque, out saldoBeneficio);

                if (dados.PercentualContrib < 8)
                    simulador.Calcular(dados, 8, DataNascimento, CdEmpresa, out saldoProjetado8, out saque8, out saldoBeneficio8);

                var listaRendaMensal = new List<RendaMensalItem>();

                for (var i = 0.001M; i <= 0.015M; i = i + 0.001M)
                {
                    var rendaMensal = saldoBeneficio * i;
                    var tempoRecebimento = saldoBeneficio / rendaMensal / 13;
                    var anosCompletos = Math.Floor(tempoRecebimento);
                    var meses = Math.Floor((tempoRecebimento - anosCompletos) * 12);

                    var rendaMensalItem = new RendaMensalItem
                    {
                        Percentual = i * 100,
                        Renda = rendaMensal,
                        StringTempoRecebimento = $"{anosCompletos} anos {meses} meses"
                    };

                    if(dados.PercentualContrib < 8)
                    {
                        var rendaMensal8 = saldoBeneficio8 * i;
                        rendaMensalItem.Renda8 = rendaMensal8;
                    }

                    listaRendaMensal.Add(rendaMensalItem);
                }

                var retorno = new
                {
                    SaldoProjetado = saldoProjetado,
                    SaldoProjetado8 = dados.PercentualContrib < 8 ? saldoProjetado8 : 0M,
                    Saque = saque,
                    Saque8 = dados.PercentualContrib < 8 ? saque8 : 0M,
                    RendaMensal = listaRendaMensal,
                    MemoriaCalculo = memoriaCalculo
                };

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}