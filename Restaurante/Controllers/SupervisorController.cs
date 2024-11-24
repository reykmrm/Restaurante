using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurante.Data.Services;
using Restaurante.Models.DTO;

namespace Restaurante.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupervisorController : ControllerBase
    {
        private readonly ISupervisorService _supervisorService;

        public SupervisorController(ISupervisorService supervisorService)
        {
            _supervisorService = supervisorService;
        }

        [HttpPost]
        public async Task<IActionResult> AddSupervisor([FromBody] SupervisorDTO supervisorDTO)
        {
            var response = await _supervisorService.AddSupervisorAsync(supervisorDTO);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSupervisors()
        {
            var response = await _supervisorService.GetAllSupervisorsAsync();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSupervisorById(int id)
        {
            var response = await _supervisorService.GetSupervisorByIdAsync(id);
            if (!response.Success)
                return NotFound(response.Message);
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSupervisor(int id, [FromBody] SupervisorDTO supervisorDTO)
        {
            if (id != supervisorDTO.IdSupervisor)
                return BadRequest("El ID no coincide con los datos proporcionados.");
            var response = await _supervisorService.UpdateSupervisorAsync(supervisorDTO);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveSupervisor(int id)
        {
            var response = await _supervisorService.RemoveSupervisorAsync(id);
            if (!response.Success)
                return NotFound(response.Message);
            return Ok(response);
        }
    }
}

