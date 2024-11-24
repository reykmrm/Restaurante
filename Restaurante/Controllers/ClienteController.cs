using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurante.Data.Services;
using Restaurante.Models.DTO;

namespace Restaurante.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpPost]
        public async Task<IActionResult> AddCliente([FromBody] ClienteDTO clienteDTO)
        {
            if (clienteDTO == null)
                return BadRequest("Cliente no puede ser nulo.");

            // Llamar al servicio y obtener el ResponseModel
            var result = await _clienteService.AddClienteAsync(clienteDTO);

            if (result.Success)
                return Ok(result);  // Devuelve la respuesta completa
            return StatusCode(500, result);  // Devuelve la respuesta completa con el mensaje de error
        }

        [HttpGet]
        public async Task<IActionResult> GetAllClientes()
        {
            var result = await _clienteService.GetAllClientesAsync();
            return Ok(result);  // Devuelve la respuesta completa
        }

        [HttpGet("cliente-consumo")]
        public async Task<IActionResult> GetClientesConTotalConsumo()
        {
            var result = await _clienteService.GetClientesConTotalConsumoAsync();

            if (result.Success)
                return Ok(result.Data);
            else
                return StatusCode(500, result.Message);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetClienteById(int id)
        {
            var result = await _clienteService.GetClienteByIdAsync(id);
            if (result.Data == null)
                return NotFound(result);  // Devuelve la respuesta completa con el mensaje de error
            return Ok(result);  // Devuelve la respuesta completa
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCliente(int id, [FromBody] ClienteDTO clienteDTO)
        {
            if (clienteDTO == null || clienteDTO.Identificacion != id)
                return BadRequest("Datos de cliente inválidos.");

            var result = await _clienteService.UpdateClienteAsync(clienteDTO);

            if (result.Success)
                return Ok(result);  // Devuelve la respuesta completa
            return StatusCode(500, result);  // Devuelve la respuesta completa con el mensaje de error
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveCliente(int id)
        {
            var result = await _clienteService.RemoveClienteAsync(id);

            if (result.Success)
                return Ok(result);  // Devuelve la respuesta completa
            return NotFound(result);  // Devuelve la respuesta completa con el mensaje de error
        }
    }
}
