using Microsoft.AspNetCore.Mvc;

namespace Everton._123Vendas.API.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/compra")]
    public class CompraController : BaseController
    {
        public async Task<IActionResult> TestAsync()
        {
            return Ok(new { message = "Deu certo" });
        }
    }
}
