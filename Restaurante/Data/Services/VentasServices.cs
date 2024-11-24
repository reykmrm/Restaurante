using Microsoft.EntityFrameworkCore;
using Restaurante.Data.Ropository;
using Restaurante.Data.Ropository.IRepository;
using Restaurante.Models.DTO;
using Restaurante.Models.Tables;

namespace Restaurante.Data.Services
{
    public class VentasServices
    {

        private readonly IClienteRepository _clienteRepository;
        private readonly IDetalleFacturaRepository _detalleFacturaRepository;
        private readonly IFacturaRepository _facturaRepository;
        private readonly IMesaRepository _mesaRepository;
        private readonly IMeseroRepository _meseroRepository;
        private readonly ISupervisorRepository _supervisorRepository;
        public VentasServices(IClienteRepository ClienteRepository, IDetalleFacturaRepository DetalleFacturaRepository,
            IFacturaRepository FacturaRepository, IMesaRepository MesaRepository, IMeseroRepository MeseroRepository,
            ISupervisorRepository SupervisorRepository)
        {
            _clienteRepository = ClienteRepository;
            _detalleFacturaRepository = DetalleFacturaRepository;
            _facturaRepository = FacturaRepository;
            _mesaRepository = MesaRepository;
            _meseroRepository = MeseroRepository;
            _supervisorRepository = SupervisorRepository;
        }

        public async Task<ResponseModel<List<object>>> GetVentas(RequestDate RequestDate)
        {
            var factura = await _facturaRepository.GetAllFacturaLambda(x => x.Fecha >= RequestDate.FechaInicio && x.Fecha <= RequestDate.FechaFin);


            var formattedData = factura.Select(x => new
            {
                NroFactura = x.NroFactura,
                Fecha = x.Fecha.ToString("yyyy-MM-dd"),
                Cliente = x.IdClienteNavigation.Nombres,
                Mesa = x.NroMesaNavigation.Nombre,
                Mesero = x.IdMeseroNavigation.Nombres,
                total = x.DetalleFacturas.Sum(d => d.Valor),
                IdCliente = x.IdCliente,
                IdMesero = x.IdMesero,
                NroMesa = x.NroMesa,
                Detalles = x.DetalleFacturas.Select(d => new
                {
                    Plato = d.Plato,
                    Supervisor = d.IdSupervisorNavigation.Nombres,
                    Valor = d.Valor
                }).ToList()
            })
            .OrderByDescending(x => x.Fecha)
            .ToList();

            return new ResponseModel<List<object>>(true, false, "OK", formattedData.Cast<object>().ToList());
        }

        

    }
}
