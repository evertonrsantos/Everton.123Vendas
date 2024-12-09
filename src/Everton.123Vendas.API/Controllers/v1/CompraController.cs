using AutoMapper;
using Everton._123Vendas.API.Models.Request;
using Everton._123Vendas.API.Models.Response;
using Everton._123Vendas.Domain.Entities;
using Everton._123Vendas.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Everton._123Vendas.API.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/compra")]
    public class CompraController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly ICompraService _compraService;

        public CompraController(ICompraService compraService, IMapper mapper)
        {
            _mapper = mapper;
            _compraService = compraService;
        }

        [HttpPost]
        public async Task<IActionResult> CriarCompraAsync([FromBody] CompraRequest model)
        {
            var compra = _mapper.Map<Compra>(model);
            var compraId = await _compraService.CreateAsync(compra);
            return Ok(new { id = compraId });
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> AlterarCompraAsync([FromRoute] Guid id, [FromBody] CompraRequest model)
        {
            var compra = _mapper.Map<Compra>(model);
            await _compraService.UpdateAsync(id, compra);
            return Ok();
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> ObterCompra([FromRoute] Guid id)
        {
            var compra = await _compraService.GetAsync(id);
            if (compra == null)
                return Ok();

            return Ok(_mapper.Map<CompraDetalheResponse>(compra));
        }

        [HttpGet]
        public async Task<IActionResult> ObterTodasCompras()
        {
            var compras = await _compraService.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<CompraResponse>>(compras));
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> CancelarCompra(Guid id)
        {
            await _compraService.DeleteAsync(id);
            return Ok();
        }
    }
}
