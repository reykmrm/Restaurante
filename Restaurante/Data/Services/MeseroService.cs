using Restaurante.Data.Ropository.IRepository;
using Restaurante.Models.DTO;
using Restaurante.Models.Tables;

namespace Restaurante.Data.Services
{
    public interface IMeseroService
    {
        Task<ResponseModel<bool>> AddMeseroAsync(MeseroDTO meseroDTO);

        Task<ResponseModel<List<MeseroDTO>>> GetAllMeserosAsync();

        Task<ResponseModel<MeseroDTO?>> GetMeseroByIdAsync(int idMesero);

        Task<ResponseModel<bool>> UpdateMeseroAsync(MeseroDTO meseroDTO);

        Task<ResponseModel<bool>> RemoveMeseroAsync(int idMesero);

        Task<ResponseModel<List<object>>> GetTotalVentasPorMeseroAsync();

    }

    public class MeseroService : IMeseroService
    {
        private readonly IMeseroRepository _meseroRepository;

        public MeseroService(IMeseroRepository meseroRepository)
        {
            _meseroRepository = meseroRepository;
        }

        public async Task<ResponseModel<bool>> AddMeseroAsync(MeseroDTO meseroDTO)
        {
            try
            {
                var mesero = new Mesero
                {
                    Nombres = meseroDTO.Nombres,
                    Apellidos = meseroDTO.Apellidos,
                    Edad = meseroDTO.Edad,
                    Antiguedad = meseroDTO.Antiguedad
                };

                bool result = await _meseroRepository.AddMesero(mesero);

                return new ResponseModel<bool>(
                    success: result,
                    warning: !result,
                    message: result ? "Mesero agregado exitosamente." : "Error al agregar el mesero.",
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

        public async Task<ResponseModel<List<MeseroDTO>>> GetAllMeserosAsync()
        {
            try
            {
                var meseros = await _meseroRepository.GetAllMeseroLambda();

                if (meseros == null || !meseros.Any())
                    return new ResponseModel<List<MeseroDTO>>(
                        success: true,
                        warning: true,
                        message: "No se encontraron meseros.",
                        data: new List<MeseroDTO>()
                    );

                var meseroDTOs = meseros.Select(m => new MeseroDTO
                {
                    IdMesero = m.IdMesero,
                    Nombres = m.Nombres,
                    Apellidos = m.Apellidos,
                    Edad = m.Edad,
                    Antiguedad = m.Antiguedad
                }).ToList();

                return new ResponseModel<List<MeseroDTO>>(
                    success: true,
                    warning: false,
                    message: "Meseros obtenidos exitosamente.",
                    data: meseroDTOs
                );
            }
            catch (Exception ex)
            {
                return new ResponseModel<List<MeseroDTO>>(
                    success: false,
                    warning: true,
                    message: $"Error: {ex.Message}"
                );
            }
        }

        public async Task<ResponseModel<List<object>>> GetTotalVentasPorMeseroAsync()
        {
            try
            {

                var meseros = await _meseroRepository.GetAllMeseroLambda();

                if (meseros == null || !meseros.Any())
                {
                    return new ResponseModel<List<object>>(
                        success: true,
                        warning: true,
                        message: "No se encontraron meseros con ventas.",
                        data: new List<object>()
                    );
                }


                var resultado = meseros.Select(m => new
                {
                    m.IdMesero,
                    m.Nombres,
                    m.Apellidos,
                    TotalAtenciones = m.Facturas.Count(),
                    TotalVentas = m.Facturas.Sum(f => f.DetalleFacturas.Sum(d => d.Valor)) 
                }).OrderBy(x=> x.Nombres)
                    .ToList();

                return new ResponseModel<List<object>>(
                    success: true,
                    warning: false,
                    message: "Datos de ventas por mesero obtenidos exitosamente.",
                    data: resultado.Cast<object>().ToList()
                );
            }
            catch (Exception ex)
            {
                return new ResponseModel<List<object>>(
                    success: false,
                    warning: true,
                    message: $"Error: {ex.Message}"
                );
            }
        }



        public async Task<ResponseModel<MeseroDTO?>> GetMeseroByIdAsync(int idMesero)
        {
            try
            {
                var mesero = await _meseroRepository.GetByCondition(m => m.IdMesero == idMesero);

                if (mesero == null)
                    return new ResponseModel<MeseroDTO?>(
                        success: false,
                        warning: true,
                        message: "Mesero no encontrado."
                    );

                var meseroDTO = new MeseroDTO
                {
                    IdMesero = mesero.IdMesero,
                    Nombres = mesero.Nombres,
                    Apellidos = mesero.Apellidos,
                    Edad = mesero.Edad,
                    Antiguedad = mesero.Antiguedad
                };

                return new ResponseModel<MeseroDTO?>(
                    success: true,
                    warning: false,
                    message: "Mesero obtenido exitosamente.",
                    data: meseroDTO
                );
            }
            catch (Exception ex)
            {
                return new ResponseModel<MeseroDTO?>(
                    success: false,
                    warning: true,
                    message: $"Error: {ex.Message}"
                );
            }
        }

        public async Task<ResponseModel<bool>> UpdateMeseroAsync(MeseroDTO meseroDTO)
        {
            try
            {
                var mesero = await _meseroRepository.GetByCondition(m => m.IdMesero == meseroDTO.IdMesero);

                if (mesero == null)
                    return new ResponseModel<bool>(
                        success: false,
                        warning: true,
                        message: "Mesero no encontrado."
                    );

                mesero.Nombres = meseroDTO.Nombres;
                mesero.Apellidos = meseroDTO.Apellidos;
                mesero.Edad = meseroDTO.Edad;
                mesero.Antiguedad = meseroDTO.Antiguedad;

                bool result = await _meseroRepository.UpdateMesero(mesero);

                return new ResponseModel<bool>(
                    success: result,
                    warning: !result,
                    message: result ? "Mesero actualizado exitosamente." : "Error al actualizar el mesero.",
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

        public async Task<ResponseModel<bool>> RemoveMeseroAsync(int idMesero)
        {
            try
            {
                var mesero = await _meseroRepository.GetByCondition(m => m.IdMesero == idMesero);

                if (mesero == null)
                    return new ResponseModel<bool>(
                        success: false,
                        warning: true,
                        message: "Mesero no encontrado."
                    );

                bool result = await _meseroRepository.RemoveMesero(mesero);

                return new ResponseModel<bool>(
                    success: result,
                    warning: !result,
                    message: result ? "Mesero eliminado exitosamente." : "Error al eliminar el mesero.",
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
