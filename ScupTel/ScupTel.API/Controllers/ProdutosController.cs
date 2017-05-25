using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ScupTel.Domain.Produto;
using ScupTel.API.DataTransferObject.Factories;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using ScupTel.API.DataTransferObject.Interfaces;
using Microsoft.Extensions.Logging;

namespace ScupTel.API.Controllers
{
    [Produces("application/json")]
    [Route("/Produtos")]
    public class ProdutosController : Controller
    {
        private readonly IProdutoChamadaService _produtoChamadaService;

        public ProdutosController(IProdutoChamadaService produtoChamadaService)
        {
            _produtoChamadaService = produtoChamadaService;
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                var produto = ProdutoChamadaDtoFactory.ReturnProdutoDto(_produtoChamadaService.Get(id));

                if (produto == null)
                {
                    return NotFound(id);
                }

                return Ok(produto);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }

        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = _produtoChamadaService.All().Select(o => ProdutoChamadaDtoFactory.ReturnProdutoDto(o)).ToList();

            if (result.Count == 0)
            {
                return NoContent();
            }

            return Ok(result);
        }

        [HttpPost]
        public IActionResult AdicionarProdutoFaleMais([FromBody]JObject produtoChamada)
        {
            try
            {
                var nome = produtoChamada.GetValue("Nome", StringComparison.CurrentCultureIgnoreCase).ToString();
                var minutosFranquia = produtoChamada.GetValue("MinutosFranquia", StringComparison.CurrentCultureIgnoreCase).Value<Int32>();
                var produto = new FaleMais(nome, minutosFranquia);

                var novoProduto = _produtoChamadaService.Save(produto);

                return Created("/Produtos/{id}", novoProduto.Id);
            }
            catch (InvalidCastException)
            {
                return BadRequest();
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
