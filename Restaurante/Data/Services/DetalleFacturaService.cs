using Restaurante.Data.Ropository.IRepository;
using Restaurante.Models.DTO;
using Restaurante.Models.Tables;

namespace Restaurante.Data.Services
{
    public interface IDetalleFacturaService
    {
        Task<ResponseModel<bool>> AddDetalleFacturaAsync(DetalleFacturaDTO detalleFacturaDTO);
        Task<ResponseModel<List<object>>> GetAllDetalleFacturasAsync(int numeroFactura);
        Task<ResponseModel<DetalleFacturaDTO?>> GetDetalleFacturaByIdAsync(int id);
        Task<ResponseModel<bool>> UpdateDetalleFacturaAsync(DetalleFacturaDTO detalleFacturaDTO);
        Task<ResponseModel<bool>> RemoveDetalleFacturaAsync(int id);
        Task<ResponseModel<List<object>>> GetProductosVendidosAsync(RequestDate requestDate);
    }

    public class DetalleFacturaService : IDetalleFacturaService
    {
        private readonly IDetalleFacturaRepository _detalleFacturaRepository;

        public DetalleFacturaService(IDetalleFacturaRepository detalleFacturaRepository)
        {
            _detalleFacturaRepository = detalleFacturaRepository;
        }

        public async Task<ResponseModel<bool>> AddDetalleFacturaAsync(DetalleFacturaDTO detalleFacturaDTO)
        {
            var detalleFactura = new DetalleFactura
            {
                NroFactura = detalleFacturaDTO.NroFactura,
                IdSupervisor = detalleFacturaDTO.IdSupervisor,
                Plato = detalleFacturaDTO.Plato,
                Valor = detalleFacturaDTO.Valor
            };

            bool result = await _detalleFacturaRepository.AddDetalleFactura(detalleFactura);

            if (result)
                return new ResponseModel<bool>(true, false, "Detalle de factura agregado exitosamente.", result);

            return new ResponseModel<bool>(false, true, "Hubo un problema al agregar el detalle de factura.", result);
        }

        public async Task<ResponseModel<List<object>>> GetAllDetalleFacturasAsync(int nFactura)
        {
            var detalles = await _detalleFacturaRepository.GetAllDetalleFacturaLambda(x => x.NroFactura == nFactura);
            var detallesDTO = detalles.Select(d => new
            {
                IdDetalleFactura = d.IdDetalleFactura,
                NroFactura = d.NroFactura,
                IdSupervisor = d.IdSupervisor,
                Supervisor = d.IdSupervisorNavigation.Nombres,
                Plato = d.Plato,
                Valor = d.Valor,
            }).ToList();

            return new ResponseModel<List<object>>(true, false, "Detalles de factura obtenidos exitosamente.", detallesDTO.Cast<object>().ToList());
        }

        public async Task<ResponseModel<DetalleFacturaDTO?>> GetDetalleFacturaByIdAsync(int id)
        {
            var detalle = await _detalleFacturaRepository.GetByCondition(d => d.IdDetalleFactura == id);

            if (detalle == null)
                return new ResponseModel<DetalleFacturaDTO?>(false, true, "Detalle de factura no encontrado.");

            var detalleDTO = new DetalleFacturaDTO
            {
                IdDetalleFactura = detalle.IdDetalleFactura,
                NroFactura = detalle.NroFactura,
                IdSupervisor = detalle.IdSupervisor,
                Plato = detalle.Plato,
                Valor = detalle.Valor,
            };

            return new ResponseModel<DetalleFacturaDTO?>(true, false, "Detalle de factura encontrado.", detalleDTO);
        }

        public async Task<ResponseModel<List<object>>> GetProductosVendidosAsync(RequestDate requestDate)
        {
            try
            {
                // Validación de las fechas
                if (!requestDate.FechaInicio.HasValue || !requestDate.FechaFin.HasValue)
                {
                    return new ResponseModel<List<object>>(
                        success: false,
                        warning: true,
                        message: "Las fechas de inicio y fin son requeridas."
                    );
                }

                // Convertir las fechas a DateOnly para la comparación
                var fechaInicio = requestDate.FechaInicio.Value;
                var fechaFin = requestDate.FechaFin.Value;

                // Filtrar los detalles de facturas por el rango de fechas
                var detalles = await _detalleFacturaRepository.GetAllDetalleFacturaLambda(
                    d => d.NroFacturaNavigation.Fecha >= fechaInicio && d.NroFacturaNavigation.Fecha <= fechaFin);

                if (detalles == null || !detalles.Any())
                {
                    return new ResponseModel<List<object>>(
                        success: true,
                        warning: true,
                        message: $"No se encontraron productos vendidos entre {fechaInicio:yyyy-MM-dd} y {fechaFin:yyyy-MM-dd}.",
                        data: new List<object>()
                    );
                }

                // Agrupar los detalles por plato y calcular la cantidad y el monto total
                var productosVendidos = detalles
                    .GroupBy(d => d.Plato)
                    .Select(g => new
                    {
                        Producto = g.Key, // Nombre del producto
                        CantidadVendida = g.Count(), // Cantidad total de platos vendidos
                        MontoTotalFacturado = g.Sum(x => x.Valor) // Monto total facturado
                    })
                    .OrderByDescending(x => x.CantidadVendida) // Ordenar por cantidad vendida
                    .ToList();

                return new ResponseModel<List<object>>(
                    success: true,
                    warning: false,
                    message: "Productos vendidos obtenidos exitosamente.",
                    data: productosVendidos.Cast<object>().ToList()
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


        public async Task<ResponseModel<bool>> UpdateDetalleFacturaAsync(DetalleFacturaDTO detalleFacturaDTO)
        {
            var detalle = await _detalleFacturaRepository.GetByCondition(d => d.IdDetalleFactura == detalleFacturaDTO.IdDetalleFactura);

            if (detalle == null)
                return new ResponseModel<bool>(false, true, "Detalle de factura no encontrado para actualizar.");

            detalle.NroFactura = detalleFacturaDTO.NroFactura;
            detalle.IdSupervisor = detalleFacturaDTO.IdSupervisor;
            detalle.Plato = detalleFacturaDTO.Plato;
            detalle.Valor = detalleFacturaDTO.Valor;

            bool result = await _detalleFacturaRepository.UpdateDetalleFactura(detalle);

            if (result)
                return new ResponseModel<bool>(true, false, "Detalle de factura actualizado exitosamente.", result);

            return new ResponseModel<bool>(false, true, "Hubo un problema al actualizar el detalle de factura.", result);
        }

        public async Task<ResponseModel<bool>> RemoveDetalleFacturaAsync(int id)
        {
            var detalle = await _detalleFacturaRepository.GetByCondition(d => d.IdDetalleFactura == id);

            if (detalle == null)
                return new ResponseModel<bool>(false, true, "Detalle de factura no encontrado para eliminar.");

            bool result = await _detalleFacturaRepository.RemoveDetalleFactura(detalle);

            if (result)
                return new ResponseModel<bool>(true, false, "Detalle de factura eliminado exitosamente.", result);

            return new ResponseModel<bool>(false, true, "Hubo un problema al eliminar el detalle de factura.", result);
        }
    }
}
