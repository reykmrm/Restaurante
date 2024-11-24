using Microsoft.EntityFrameworkCore;
using Restaurante.Data.Ropository.IRepository;
using Restaurante.Models.Tables;
using System.Linq.Expressions;

namespace Restaurante.Data.Ropository
{
    public class FacturaRepository : IFacturaRepository
    {
        private readonly RestauranteContext _context;
        public FacturaRepository(RestauranteContext context)
        {
            _context = context;
        }
        public async Task<bool> AddFactura(Factura model)
        {
            await _context.Facturas.AddAsync(model);
            return await Save();

        }

        // Existing code...

        public async Task<List<Factura>> GetAllFacturaLambda(Expression<Func<Factura, bool>> predicate = null)
        {
            var query = _context.Facturas
                .Include(f => f.IdClienteNavigation)
                .Include(f => f.IdMeseroNavigation)
                .Include(f => f.NroMesaNavigation)
                .Include(f => f.DetalleFacturas)
                    .ThenInclude(d => d.IdSupervisorNavigation);

            if (predicate == null)
            {
                return await query.ToListAsync();
            }

            return await query.Where(predicate).ToListAsync();
        }

        public async Task<Factura?> GetByCondition(Expression<Func<Factura, bool>> predicate)
        {
            return await _context.Facturas
                .Include(x => x.IdClienteNavigation)
                .Include(x => x.IdMeseroNavigation)
                .Include(x => x.NroMesaNavigation)
                .FirstOrDefaultAsync(predicate);
        }

        public async Task<bool> RemoveFactura(Factura model)
        {
            _context.Facturas.Remove(model);
            return await Save();
        }

        public async Task<bool> Save()
        {
            return await _context.SaveChangesAsync() > 0 ? true : false;
        }

        public async Task<bool> UpdateFactura(Factura model)
        {
            _context.Facturas.Update(model);
            return await Save();
        }
    }
}
