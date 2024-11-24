using Microsoft.EntityFrameworkCore;
using Restaurante.Data.Ropository.IRepository;
using Restaurante.Models.Tables;
using System.Linq.Expressions;

namespace Restaurante.Data.Ropository
{
    public class DetalleFacturaRepository : IDetalleFacturaRepository
    {
        private readonly RestauranteContext _context;
        public DetalleFacturaRepository(RestauranteContext context)
        {
            _context = context;
        }
        public async Task<bool> AddDetalleFactura(DetalleFactura model)
        {
            await _context.DetalleFacturas.AddAsync(model);
            return await Save();

        }

        // Existing code...

        public async Task<List<DetalleFactura>> GetAllDetalleFacturaLambda(Expression<Func<DetalleFactura, bool>> predicate = null)
        {
            if (predicate == null)
            {
                return await _context.DetalleFacturas
                    .Include(x => x.NroFacturaNavigation)
                    .Include(x => x.IdSupervisorNavigation)
                    .ToListAsync();
            }
            return await _context.DetalleFacturas
                .Include(x => x.NroFacturaNavigation)
                .Include(x => x.IdSupervisorNavigation)
                .Where(predicate).ToListAsync();
        }

        public async Task<DetalleFactura?> GetByCondition(Expression<Func<DetalleFactura, bool>> predicate)
        {
            return await _context.DetalleFacturas
                .Include(x => x.NroFacturaNavigation)
                .Include(x => x.IdSupervisorNavigation)
                .FirstOrDefaultAsync(predicate);
        }

        public async Task<bool> RemoveDetalleFactura(DetalleFactura model)
        {
            _context.DetalleFacturas.Remove(model);
            return await Save();
        }

        public async Task<bool> Save()
        {
            return await _context.SaveChangesAsync() > 0 ? true : false;
        }

        public async Task<bool> UpdateDetalleFactura(DetalleFactura model)
        {
            _context.DetalleFacturas.Update(model);
            return await Save();
        }
    }
}

