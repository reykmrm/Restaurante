using Restaurante.Data.Ropository.IRepository;
using Restaurante.Models.DTO;
using Restaurante.Models.Tables;
using static Restaurante.Data.Services.MesaService;

namespace Restaurante.Data.Services
{
    public interface IMesaService
    {
        Task<ResponseModel<bool>> AddMesaAsync(MesaDTO mesaDTO);

        Task<ResponseModel<List<MesaDTO>>> GetAllMesasAsync();

        Task<ResponseModel<MesaDTO?>> GetMesaByIdAsync(int nroMesa);

        Task<ResponseModel<bool>> UpdateMesaAsync(MesaDTO mesaDTO);

        Task<ResponseModel<bool>> RemoveMesaAsync(int nroMesa);
    }

    public class MesaService : IMesaService
    {
        private readonly IMesaRepository _mesaRepository;

        public MesaService(IMesaRepository mesaRepository)
        {
            _mesaRepository = mesaRepository;
        }

        public async Task<ResponseModel<bool>> AddMesaAsync(MesaDTO mesaDTO)
        {
            try
            {
                var mesa = new Mesa
                {
                    NroMesa = mesaDTO.NroMesa,
                    Nombre = mesaDTO.Nombre,
                    Reservada = mesaDTO.Reservada,
                    Puestos = mesaDTO.Puestos
                };

                bool result = await _mesaRepository.AddMesa(mesa);
                return new ResponseModel<bool>(
                    success: result,
                    warning: false,
                    message: result ? "Mesa agregada exitosamente." : "Error al agregar la mesa.",
                    data: result
                );
            }
            catch (Exception ex)
            {
                return CreateErrorResponse<bool>("Error al agregar la mesa", ex);
            }
        }

        public async Task<ResponseModel<List<MesaDTO>>> GetAllMesasAsync()
        {
            try
            {
                var mesas = await _mesaRepository.GetAllMesaLambda();
                if (mesas == null || !mesas.Any())
                {
                    return new ResponseModel<List<MesaDTO>>(
                        success: true,
                        warning: true,
                        message: "No se encontraron mesas.",
                        data: new List<MesaDTO>()
                    );
                }

                var mesaDTOs = mesas.Select(m => new MesaDTO
                {
                    NroMesa = m.NroMesa,
                    Nombre = m.Nombre,
                    Reservada = m.Reservada,
                    Puestos = m.Puestos
                }).ToList();

                return new ResponseModel<List<MesaDTO>>(
                    success: true,
                    warning: false,
                    message: "Mesas obtenidas exitosamente.",
                    data: mesaDTOs
                );
            }
            catch (Exception ex)
            {
                return CreateErrorResponse<List<MesaDTO>>("Error al obtener las mesas", ex);
            }
        }

        public async Task<ResponseModel<MesaDTO?>> GetMesaByIdAsync(int nroMesa)
        {
            try
            {
                var mesa = await _mesaRepository.GetByCondition(m => m.NroMesa == nroMesa);
                if (mesa == null)
                {
                    return new ResponseModel<MesaDTO?>(
                        success: false,
                        warning: true,
                        message: "Mesa no encontrada."
                    );
                }

                var mesaDTO = new MesaDTO
                {
                    NroMesa = mesa.NroMesa,
                    Nombre = mesa.Nombre,
                    Reservada = mesa.Reservada,
                    Puestos = mesa.Puestos
                };

                return new ResponseModel<MesaDTO?>(
                    success: true,
                    warning: false,
                    message: "Mesa obtenida exitosamente.",
                    data: mesaDTO
                );
            }
            catch (Exception ex)
            {
                return CreateErrorResponse<MesaDTO?>("Error al obtener la mesa", ex);
            }
        }

        public async Task<ResponseModel<bool>> UpdateMesaAsync(MesaDTO mesaDTO)
        {
            try
            {
                var mesa = await _mesaRepository.GetByCondition(m => m.NroMesa == mesaDTO.NroMesa);
                if (mesa == null)
                    return new ResponseModel<bool>(
                        success: false,
                        warning: true,
                        message: "Mesa no encontrada."
                    );

                mesa.Nombre = mesaDTO.Nombre;
                mesa.Reservada = mesaDTO.Reservada;
                mesa.Puestos = mesaDTO.Puestos;

                bool result = await _mesaRepository.UpdateMesa(mesa);
                return new ResponseModel<bool>(
                    success: result,
                    warning: false,
                    message: result ? "Mesa actualizada exitosamente." : "Error al actualizar la mesa.",
                    data: result
                );
            }
            catch (Exception ex)
            {
                return CreateErrorResponse<bool>("Error al actualizar la mesa", ex);
            }
        }

        public async Task<ResponseModel<bool>> RemoveMesaAsync(int nroMesa)
        {
            try
            {
                var mesa = await _mesaRepository.GetByCondition(m => m.NroMesa == nroMesa);
                if (mesa == null)
                    return new ResponseModel<bool>(
                        success: false,
                        warning: true,
                        message: "Mesa no encontrada."
                    );

                bool result = await _mesaRepository.RemoveMesa(mesa);
                return new ResponseModel<bool>(
                    success: result,
                    warning: false,
                    message: result ? "Mesa eliminada exitosamente." : "Error al eliminar la mesa.",
                    data: result
                );
            }
            catch (Exception ex)
            {
                return CreateErrorResponse<bool>("Error al eliminar la mesa", ex);
            }
        }

        // Método auxiliar para la creación de respuestas con error
        private ResponseModel<T> CreateErrorResponse<T>(string message, Exception ex)
        {
            return new ResponseModel<T>(
                success: false,
                warning: true,
                message: $"{message}: {ex.Message}"
            );
        }
    }
}

