using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurante.Data.Services;
using Restaurante.Models.DTO;

namespace Restaurante.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentasController : ControllerBase
    {
        private readonly VentasServices _service;

        public VentasController(VentasServices service)
        {
            _service = service;
        }

        [HttpPost("get-ventas")]
        public async Task<IActionResult> Get(RequestDate RequestDate)
        {
            var response = await _service.GetVentas(RequestDate);
            return Ok(response);
        }
    }
}
