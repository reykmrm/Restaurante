using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurante.Data.Services;
using Restaurante.Models.DTO;

namespace Restaurante.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturaController : ControllerBase
    {
        private readonly IFacturaService _facturaService;

        public FacturaController(IFacturaService facturaService)
        {
            _facturaService = facturaService;
        }

        [HttpPost]
        public async Task<IActionResult> AddFactura([FromBody] FacturaDTO facturaDTO)
        {
            if (facturaDTO == null)
                return BadRequest(new ResponseModel<string>(false, true, "La factura no puede ser nula."));

            var response = await _facturaService.AddFacturaAsync(facturaDTO);

            if (response.Success)
                return Ok(response);

            return StatusCode(500, response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFacturas()
        {
            var response = await _facturaService.GetAllFacturasAsync();

            if (response.Success)
                return Ok(response);

            return StatusCode(500, response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFacturaById(int id)
        {
            var response = await _facturaService.GetFacturaByIdAsync(id);

            if (response.Success)
                return Ok(response);

            if (response.Warning)
                return NotFound(response);

            return StatusCode(500, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFactura(int id, [FromBody] FacturaDTO facturaDTO)
        {
            if (facturaDTO == null || facturaDTO.NroFactura != id)
                return BadRequest(new ResponseModel<string>(false, true, "Datos de factura inválidos."));

            var response = await _facturaService.UpdateFacturaAsync(facturaDTO);

            if (response.Success)
                return Ok(response);

            if (response.Warning)
                return NotFound(response);

            return StatusCode(500, response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveFactura(int id)
        {
            var response = await _facturaService.RemoveFacturaAsync(id);

            if (response.Success)
                return Ok(response);

            if (response.Warning)
                return NotFound(response);

            return StatusCode(500, response);
        }
    }
}
