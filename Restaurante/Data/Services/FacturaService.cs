using Restaurante.Data.Ropository.IRepository;
using Restaurante.Models.DTO;
using Restaurante.Models.Tables;

namespace Restaurante.Data.Services
{
    public interface IFacturaService
    {
        Task<ResponseModel<string>> AddFacturaAsync(FacturaDTO facturaDTO);
        Task<ResponseModel<List<FacturaDTO>>> GetAllFacturasAsync();
        Task<ResponseModel<FacturaDTO>> GetFacturaByIdAsync(int id);
        Task<ResponseModel<string>> UpdateFacturaAsync(FacturaDTO facturaDTO);
        Task<ResponseModel<string>> RemoveFacturaAsync(int id);
    }

    public class FacturaService : IFacturaService
    {
        private readonly IFacturaRepository _facturaRepository;

        public FacturaService(IFacturaRepository facturaRepository)
        {
            _facturaRepository = facturaRepository;
        }

        public async Task<ResponseModel<string>> AddFacturaAsync(FacturaDTO facturaDTO)
        {
            try
            {
                var factura = new Factura
                {
                    NroFactura = facturaDTO.NroFactura,
                    IdCliente = facturaDTO.IdCliente,
                    NroMesa = facturaDTO.NroMesa,
                    IdMesero = facturaDTO.IdMesero,
                    Fecha = facturaDTO.Fecha
                };

                var success = await _facturaRepository.AddFactura(factura);

                if (!success)
                    return new ResponseModel<string>(false, false, "No se pudo agregar la factura.");

                return new ResponseModel<string>(true, false, "Factura agregada exitosamente.");
            }
            catch (Exception ex)
            {
                return new ResponseModel<string>(false, false, $"Error: {ex.Message}");
            }
        }

        public async Task<ResponseModel<List<FacturaDTO>>> GetAllFacturasAsync()
        {
            try
            {
                var facturas = await _facturaRepository.GetAllFacturaLambda();

                var facturaDTOs = facturas.Select(f => new FacturaDTO
                {
                    NroFactura = f.NroFactura,
                    IdCliente = f.IdCliente,
                    NroMesa = f.NroMesa,
                    IdMesero = f.IdMesero,
                    Fecha = f.Fecha,
                }).ToList();

                return new ResponseModel<List<FacturaDTO>>(true, false, "Facturas obtenidas exitosamente.", facturaDTOs);
            }
            catch (Exception ex)
            {
                return new ResponseModel<List<FacturaDTO>>(false, false, $"Error: {ex.Message}");
            }
        }

        public async Task<ResponseModel<FacturaDTO>> GetFacturaByIdAsync(int id)
        {
            try
            {
                var factura = await _facturaRepository.GetByCondition(f => f.NroFactura == id);

                if (factura == null)
                    return new ResponseModel<FacturaDTO>(false, true, "Factura no encontrada.");

                var facturaDTO = new FacturaDTO
                {
                    NroFactura = factura.NroFactura,
                    IdCliente = factura.IdCliente,
                    NroMesa = factura.NroMesa,
                    IdMesero = factura.IdMesero,
                    Fecha = factura.Fecha,
                };

                return new ResponseModel<FacturaDTO>(true, false, "Factura obtenida exitosamente.", facturaDTO);
            }
            catch (Exception ex)
            {
                return new ResponseModel<FacturaDTO>(false, false, $"Error: {ex.Message}");
            }
        }

        public async Task<ResponseModel<string>> UpdateFacturaAsync(FacturaDTO facturaDTO)
        {
            try
            {
                var factura = await _facturaRepository.GetByCondition(f => f.NroFactura == facturaDTO.NroFactura);

                if (factura == null)
                    return new ResponseModel<string>(false, true, "Factura no encontrada.");

                factura.IdCliente = facturaDTO.IdCliente;
                factura.NroMesa = facturaDTO.NroMesa;
                factura.IdMesero = facturaDTO.IdMesero;
                //factura.Fecha = facturaDTO.Fecha;

                var success = await _facturaRepository.UpdateFactura(factura);

                if (!success)
                    return new ResponseModel<string>(false, false, "No se pudo actualizar la factura.");

                return new ResponseModel<string>(true, false, "Factura actualizada exitosamente.");
            }
            catch (Exception ex)
            {
                return new ResponseModel<string>(false, false, $"Error: {ex.Message}");
            }
        }

        public async Task<ResponseModel<string>> RemoveFacturaAsync(int id)
        {
            try
            {
                var factura = await _facturaRepository.GetByCondition(f => f.NroFactura == id);

                if (factura == null)
                    return new ResponseModel<string>(false, true, "Factura no encontrada.");

                var success = await _facturaRepository.RemoveFactura(factura);

                if (!success)
                    return new ResponseModel<string>(false, false, "No se pudo eliminar la factura.");

                return new ResponseModel<string>(true, false, "Factura eliminada exitosamente.");
            }
            catch (Exception ex)
            {
                return new ResponseModel<string>(false, false, $"Error: {ex.Message}");
            }
        }
    }
}
