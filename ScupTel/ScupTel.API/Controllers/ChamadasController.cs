using Microsoft.AspNetCore.Mvc;
using ScupTel.Domain.Chamada;
using System;
using ScupTel.API.DataTransferObject;
using System.Net;

namespace ScupTel.API.Controllers
{
    [Produces("application/json")]
    [Route("/Chamadas")]
    public class ChamadasController : Controller
    {
        private readonly ISimulacaoChamadaService _simulacaoChamadaService;

        public ChamadasController(ISimulacaoChamadaService simulacaoChamadaService)
        {
            _simulacaoChamadaService = simulacaoChamadaService;
        }

        [HttpGet("/Chamadas/SimulacaoProdutoFaleMais")]
        public IActionResult Simulacao(int dddOrigem, int dddDestino, int tempoChamada, Guid produtoFaleMaisId)
        {
            try
            {
                var chamadaCalculadaPlanoBasico = _simulacaoChamadaService.SimulacaoPlanoBasico(dddOrigem, dddDestino, tempoChamada);
                var chamadaCalculadaFaleMais = _simulacaoChamadaService.SimulacaoFaleMais(dddOrigem, dddDestino, tempoChamada, produtoFaleMaisId);

                return Ok(new SimulacaoChamadaDto(
                    dddOrigem,
                    dddDestino,
                    tempoChamada,
                    chamadaCalculadaFaleMais.Produto.Nome,
                    chamadaCalculadaFaleMais.Valor ?? new Nullable<decimal>(),
                    chamadaCalculadaPlanoBasico.Valor ?? new Nullable<decimal>()));
            }
            catch(ArgumentException arEx)
            {
                return NotFound(arEx.Message);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
