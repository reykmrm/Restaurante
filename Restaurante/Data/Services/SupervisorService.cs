using Restaurante.Data.Ropository.IRepository;
using Restaurante.Models.DTO;
using Restaurante.Models.Tables;

namespace Restaurante.Data.Services
{
    public interface ISupervisorService
    {
        Task<ResponseModel<bool>> AddSupervisorAsync(SupervisorDTO supervisorDTO);

        Task<ResponseModel<List<SupervisorDTO>>> GetAllSupervisorsAsync();

        Task<ResponseModel<SupervisorDTO?>> GetSupervisorByIdAsync(int id);

        Task<ResponseModel<bool>> UpdateSupervisorAsync(SupervisorDTO supervisorDTO);

        Task<ResponseModel<bool>> RemoveSupervisorAsync(int id);
    }
    public class SupervisorService : ISupervisorService
    {
        private readonly ISupervisorRepository _supervisorRepository;

        public SupervisorService(ISupervisorRepository supervisorRepository)
        {
            _supervisorRepository = supervisorRepository;
        }

        public async Task<ResponseModel<bool>> AddSupervisorAsync(SupervisorDTO supervisorDTO)
        {
            try
            {
                var supervisor = new Supervisor
                {
                    IdSupervisor = supervisorDTO.IdSupervisor,
                    Nombres = supervisorDTO.Nombres,
                    Apellidos = supervisorDTO.Apellidos,
                    Edad = supervisorDTO.Edad,
                    Antiguedad = supervisorDTO.Antiguedad
                };

                bool result = await _supervisorRepository.AddSupervisor(supervisor);

                return new ResponseModel<bool>(
                    success: result,
                    warning: !result,
                    message: result ? "Supervisor agregado exitosamente." : "Error al agregar el supervisor.",
                    data: result
                );
            }
            catch (Exception ex)
            {
                return new ResponseModel<bool>(
                    success: false,
                    warning: true,
                    message: $"Error: {ex.Message}"
                );
            }
        }

        public async Task<ResponseModel<List<SupervisorDTO>>> GetAllSupervisorsAsync()
        {
            try
            {
                var supervisors = await _supervisorRepository.GetAllSupervisorLambda();

                if (supervisors == null || !supervisors.Any())
                    return new ResponseModel<List<SupervisorDTO>>(
                        success: true,
                        warning: true,
                        message: "No se encontraron supervisores.",
                        data: new List<SupervisorDTO>()
                    );

                var supervisorDTOs = supervisors.Select(s => new SupervisorDTO
                {
                    IdSupervisor = s.IdSupervisor,
                    Nombres = s.Nombres,
                    Apellidos = s.Apellidos,
                    Edad = s.Edad,
                    Antiguedad = s.Antiguedad
                }).ToList();

                return new ResponseModel<List<SupervisorDTO>>(
                    success: true,
                    warning: false,
                    message: "Supervisores obtenidos exitosamente.",
                    data: supervisorDTOs
                );
            }
            catch (Exception ex)
            {
                return new ResponseModel<List<SupervisorDTO>>(
                    success: false,
                    warning: true,
                    message: $"Error: {ex.Message}"
                );
            }
        }

        public async Task<ResponseModel<SupervisorDTO?>> GetSupervisorByIdAsync(int id)
        {
            try
            {
                var supervisor = await _supervisorRepository.GetByCondition(s => s.IdSupervisor == id);

                if (supervisor == null)
                    return new ResponseModel<SupervisorDTO?>(
                        success: false,
                        warning: true,
                        message: "Supervisor no encontrado."
                    );

                var supervisorDTO = new SupervisorDTO
                {
                    IdSupervisor = supervisor.IdSupervisor,
                    Nombres = supervisor.Nombres,
                    Apellidos = supervisor.Apellidos,
                    Edad = supervisor.Edad,
                    Antiguedad = supervisor.Antiguedad
                };

                return new ResponseModel<SupervisorDTO?>(
                    success: true,
                    warning: false,
                    message: "Supervisor obtenido exitosamente.",
                    data: supervisorDTO
                );
            }
            catch (Exception ex)
            {
                return new ResponseModel<SupervisorDTO?>(
                    success: false,
                    warning: true,
                    message: $"Error: {ex.Message}"
                );
            }
        }

        public async Task<ResponseModel<bool>> UpdateSupervisorAsync(SupervisorDTO supervisorDTO)
        {
            try
            {
                var supervisor = await _supervisorRepository.GetByCondition(s => s.IdSupervisor == supervisorDTO.IdSupervisor);

                if (supervisor == null)
                    return new ResponseModel<bool>(
                        success: false,
                        warning: true,
                        message: "Supervisor no encontrado."
                    );

                supervisor.Nombres = supervisorDTO.Nombres;
                supervisor.Apellidos = supervisorDTO.Apellidos;
                supervisor.Edad = supervisorDTO.Edad;
                supervisor.Antiguedad = supervisorDTO.Antiguedad;

                bool result = await _supervisorRepository.UpdateSupervisor(supervisor);

                return new ResponseModel<bool>(
                    success: result,
                    warning: !result,
                    message: result ? "Supervisor actualizado exitosamente." : "Error al actualizar el supervisor.",
                    data: result
                );
            }
            catch (Exception ex)
            {
                return new ResponseModel<bool>(
                    success: false,
                    warning: true,
                    message: $"Error: {ex.Message}"
                );
            }
        }

        public async Task<ResponseModel<bool>> RemoveSupervisorAsync(int id)
        {
            try
            {
                var supervisor = await _supervisorRepository.GetByCondition(s => s.IdSupervisor == id);

                if (supervisor == null)
                    return new ResponseModel<bool>(
                        success: false,
                        warning: true,
                        message: "Supervisor no encontrado."
                    );

                bool result = await _supervisorRepository.RemoveSupervisor(supervisor);

                return new ResponseModel<bool>(
                    success: result,
                    warning: !result,
                    message: result ? "Supervisor eliminado exitosamente." : "Error al eliminar el supervisor.",
                    data: result
                );
            }
            catch (Exception ex)
            {
                return new ResponseModel<bool>(
                    success: false,
                    warning: true,
                    message: $"Error: {ex.Message}"
                );
            }
        }
    }
}

