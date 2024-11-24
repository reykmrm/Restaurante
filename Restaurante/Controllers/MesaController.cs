using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurante.Data.Services;
using Restaurante.Models.DTO;

namespace Restaurante.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MesaController : ControllerBase
    {
        private readonly IMesaService _mesaService;

        public MesaController(IMesaService mesaService)
        {
            _mesaService = mesaService;
        }

        [HttpPost]
        public async Task<IActionResult> AddMesa([FromBody] MesaDTO mesaDTO)
        {
            if (mesaDTO == null)
                return BadRequest("La mesa no puede ser nula.");

            var response = await _mesaService.AddMesaAsync(mesaDTO);

            if (!response.Success)
                return StatusCode(500, response.Message);

            return Ok(response.Message);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMesas()
        {
            var response = await _mesaService.GetAllMesasAsync();

            if (!response.Success)
                return StatusCode(500, response.Message);

            if (response.Warning)
                return Ok(new { message = response.Message, mesas = response.Data });

            return Ok(response.Data);
        }

        [HttpGet("{nroMesa}")]
        public async Task<IActionResult> GetMesaById(int nroMesa)
        {
            var response = await _mesaService.GetMesaByIdAsync(nroMesa);

            if (!response.Success)
                return StatusCode(500, response.Message);

            if (response.Data == null)
                return NotFound(response.Message);

            return Ok(response.Data);
        }

        [HttpPut("{nroMesa}")]
        public async Task<IActionResult> UpdateMesa(int nroMesa, [FromBody] MesaDTO mesaDTO)
        {
            if (mesaDTO == null || mesaDTO.NroMesa != nroMesa)
                return BadRequest("Datos de mesa inválidos.");

            var response = await _mesaService.UpdateMesaAsync(mesaDTO);

            if (!response.Success)
                return StatusCode(500, response.Message);

            if (response.Warning)
                return NotFound(response.Message);

            return Ok(response.Message);
        }

        [HttpDelete("{nroMesa}")]
        public async Task<IActionResult> RemoveMesa(int nroMesa)
        {
            var response = await _mesaService.RemoveMesaAsync(nroMesa);

            if (!response.Success)
                return StatusCode(500, response.Message);

            if (response.Warning)
                return NotFound(response.Message);

            return Ok(response.Message);
        }
    }
}
