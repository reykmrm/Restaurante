using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurante.Data.Services;
using Restaurante.Models.DTO;

namespace Restaurante.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetalleFacturaController : ControllerBase
    {
        private readonly IDetalleFacturaService _detalleFacturaService;

        public DetalleFacturaController(IDetalleFacturaService detalleFacturaService)
        {
            _detalleFacturaService = detalleFacturaService;
        }

        [HttpPost]
        public async Task<IActionResult> AddDetalleFactura([FromBody] DetalleFacturaDTO detalleFacturaDTO)
        {
            if (detalleFacturaDTO == null)
                return BadRequest("El detalle de la factura no puede ser nulo.");

            var response = await _detalleFacturaService.AddDetalleFacturaAsync(detalleFacturaDTO);

            if (response.Success)
                return Ok(response);
            return StatusCode(500, response);
        }

        [HttpGet("get-facturas/{nFactura}")]
        public async Task<IActionResult> GetAllDetalleFacturas(int nFactura)
        {
            var response = await _detalleFacturaService.GetAllDetalleFacturasAsync(nFactura);
            if (response.Success)
                return Ok(response);
            return StatusCode(500, response);
        }

        [HttpPost("producto-mas-vendido")]
        public async Task<IActionResult> GetProductoMasVendido([FromBody] RequestDate requestDate)
        {
            if (requestDate.FechaInicio == null || requestDate.FechaFin == null)
                return BadRequest("Las fechas de inicio y fin son requeridas.");

            var result = await _detalleFacturaService.GetProductosVendidosAsync(requestDate);

            if (result.Success)
                return Ok(result.Data);
            else
                return StatusCode(500, result.Message);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetalleFacturaById(int id)
        {
            var response = await _detalleFacturaService.GetDetalleFacturaByIdAsync(id);
            if (response.Success)
                return Ok(response);
            return NotFound(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDetalleFactura(int id, [FromBody] DetalleFacturaDTO detalleFacturaDTO)
        {
            if (detalleFacturaDTO == null || detalleFacturaDTO.IdDetalleFactura != id)
                return BadRequest("Datos de detalle de factura inválidos.");

            var response = await _detalleFacturaService.UpdateDetalleFacturaAsync(detalleFacturaDTO);

            if (response.Success)
                return Ok(response);
            return StatusCode(500, response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveDetalleFactura(int id)
        {
            var response = await _detalleFacturaService.RemoveDetalleFacturaAsync(id);

            if (response.Success)
                return Ok(response);
            return NotFound(response);
        }
    }
}
