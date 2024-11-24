using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurante.Data.Services;
using Restaurante.Models.DTO;

namespace Restaurante.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeseroController : ControllerBase
    {
        private readonly IMeseroService _meseroService;

        public MeseroController(IMeseroService meseroService)
        {
            _meseroService = meseroService;
        }

        [HttpPost]
        public async Task<IActionResult> AddMesero([FromBody] MeseroDTO meseroDTO)
        {
            var response = await _meseroService.AddMeseroAsync(meseroDTO);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMeseros()
        {
            var response = await _meseroService.GetAllMeserosAsync();
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpGet("ventas-mesero")]
        public async Task<IActionResult> GetTotalVentasPorMesero()
        {
            var response = await _meseroService.GetTotalVentasPorMeseroAsync();
            if (!response.Success)
                return StatusCode(500, response.Message);

            return Ok(response);
        }


        [HttpGet("{idMesero}")]
        public async Task<IActionResult> GetMeseroById(int idMesero)
        {
            var response = await _meseroService.GetMeseroByIdAsync(idMesero);
            return response.Success ? Ok(response) : NotFound(response);
        }

        [HttpPut("{idMesero}")]
        public async Task<IActionResult> UpdateMesero(int idMesero, [FromBody] MeseroDTO meseroDTO)
        {
            if (meseroDTO.IdMesero != idMesero)
                return BadRequest("El ID proporcionado no coincide.");

            var response = await _meseroService.UpdateMeseroAsync(meseroDTO);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpDelete("{idMesero}")]
        public async Task<IActionResult> RemoveMesero(int idMesero)
        {
            var response = await _meseroService.RemoveMeseroAsync(idMesero);
            return response.Success ? Ok(response) : NotFound(response);
        }
    }
}
