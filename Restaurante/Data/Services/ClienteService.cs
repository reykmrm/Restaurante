using Restaurante.Data.Ropository.IRepository;
using Restaurante.Models.DTO;
using Restaurante.Models.Tables;

namespace Restaurante.Data.Services
{
    public interface IClienteService
    {
        Task<ResponseModel<bool>> AddClienteAsync(ClienteDTO clienteDTO);
        Task<ResponseModel<List<ClienteDTO>>> GetAllClientesAsync();
        Task<ResponseModel<ClienteDTO?>> GetClienteByIdAsync(int id);
        Task<ResponseModel<bool>> UpdateClienteAsync(ClienteDTO clienteDTO);
        Task<ResponseModel<bool>> RemoveClienteAsync(int id);
        Task<ResponseModel<List<object>>> GetClientesConTotalConsumoAsync();
    }

    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteService(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<ResponseModel<bool>> AddClienteAsync(ClienteDTO clienteDTO)
        {
            try
            {
                var cliente = new Cliente
                {
                    Identificacion = clienteDTO.Identificacion,
                    Nombres = clienteDTO.Nombres,
                    Apellidos = clienteDTO.Apellidos,
                    Direccion = clienteDTO.Direccion,
                    Telefono = clienteDTO.Telefono
                };

                bool success = await _clienteRepository.AddCliente(cliente);

                if (success)
                    return new ResponseModel<bool>(true, false, "Cliente agregado exitosamente.", true);
                else
                    return new ResponseModel<bool>(false, false, "Hubo un problema al agregar el cliente.", false);
            }
            catch (Exception ex)
            {
                return new ResponseModel<bool>(false, false, $"Error al agregar el cliente: {ex.Message}", false);
            }
        }

        public async Task<ResponseModel<List<ClienteDTO>>> GetAllClientesAsync()
        {
            try
            {
                var clientes = await _clienteRepository.GetAllClienteLambda();

                var clienteDTOs = clientes.Select(c => new ClienteDTO
                {
                    Identificacion = c.Identificacion,
                    Nombres = c.Nombres,
                    Apellidos = c.Apellidos,
                    Direccion = c.Direccion,
                    Telefono = c.Telefono
                }).ToList();

                return new ResponseModel<List<ClienteDTO>>(true, false, "Clientes recuperados exitosamente.", clienteDTOs);
            }
            catch (Exception ex)
            {
                return new ResponseModel<List<ClienteDTO>>(false, false, $"Error al recuperar los clientes: {ex.Message}", null);
            }
        }

        public async Task<ResponseModel<List<object>>> GetClientesConTotalConsumoAsync()
        {
            try
            {
                // Obtener todos los clientes con sus facturas y detalles
                var clientes = await _clienteRepository.GetAllClienteLambda();

                if (clientes == null || !clientes.Any())
                {
                    return new ResponseModel<List<object>>(
                        success: true,
                        warning: true,
                        message: "No se encontraron clientes con consumo.",
                        data: new List<object>()
                    );
                }

                // Seleccionar los clientes y calcular el total de consumo por cliente
                var resultado = clientes.Select(c => new
                {
                    c.Identificacion,
                    c.Nombres,
                    c.Apellidos,
                    c.Direccion,
                    c.Telefono,
                    TotalConsumo = c.Facturas
                                    .SelectMany(f => f.DetalleFacturas) 
                                    .Sum(d => d.Valor) 
                })
                .OrderBy(x => x.Nombres) // Ordenar por nombres
                .ToList();

                return new ResponseModel<List<object>>(
                    success: true,
                    warning: false,
                    message: "Datos de clientes y su total de consumo obtenidos exitosamente.",
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


        public async Task<ResponseModel<ClienteDTO?>> GetClienteByIdAsync(int id)
        {
            try
            {
                var cliente = await _clienteRepository.GetByCondition(c => c.Identificacion == id);

                if (cliente == null)
                    return new ResponseModel<ClienteDTO?>(false, false, "Cliente no encontrado.", null);

                var clienteDTO = new ClienteDTO
                {
                    Identificacion = cliente.Identificacion,
                    Nombres = cliente.Nombres,
                    Apellidos = cliente.Apellidos,
                    Direccion = cliente.Direccion,
                    Telefono = cliente.Telefono
                };

                return new ResponseModel<ClienteDTO?>(true, false, "Cliente encontrado.", clienteDTO);
            }
            catch (Exception ex)
            {
                return new ResponseModel<ClienteDTO?>(false, false, $"Error al recuperar el cliente: {ex.Message}", null);
            }
        }

        public async Task<ResponseModel<bool>> UpdateClienteAsync(ClienteDTO clienteDTO)
        {
            try
            {
                var cliente = await _clienteRepository.GetByCondition(c => c.Identificacion == clienteDTO.Identificacion);

                if (cliente == null)
                    return new ResponseModel<bool>(false, false, "Cliente no encontrado.", false);

                cliente.Nombres = clienteDTO.Nombres;
                cliente.Apellidos = clienteDTO.Apellidos;
                cliente.Direccion = clienteDTO.Direccion;
                cliente.Telefono = clienteDTO.Telefono;

                bool success = await _clienteRepository.UpdateCliente(cliente);

                return success
                    ? new ResponseModel<bool>(true, false, "Cliente actualizado exitosamente.", true)
                    : new ResponseModel<bool>(false, false, "Hubo un problema al actualizar el cliente.", false);
            }
            catch (Exception ex)
            {
                return new ResponseModel<bool>(false, false, $"Error al actualizar el cliente: {ex.Message}", false);
            }
        }

        public async Task<ResponseModel<bool>> RemoveClienteAsync(int id)
        {
            try
            {
                var cliente = await _clienteRepository.GetByCondition(c => c.Identificacion == id);

                if (cliente == null)
                    return new ResponseModel<bool>(false, false, "Cliente no encontrado.", false);

                bool success = await _clienteRepository.RemoveCliente(cliente);

                return success
                    ? new ResponseModel<bool>(true, false, "Cliente eliminado exitosamente.", true)
                    : new ResponseModel<bool>(false, false, "Hubo un problema al eliminar el cliente.", false);
            }
            catch (Exception ex)
            {
                return new ResponseModel<bool>(false, false, $"Error al eliminar el cliente: {ex.Message}", false);
            }
        }
    }
}
