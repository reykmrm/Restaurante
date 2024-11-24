using Microsoft.EntityFrameworkCore;
using Restaurante.Data.Ropository.IRepository;
using Restaurante.Models.Tables;
using System.Linq.Expressions;

namespace Restaurante.Data.Ropository
{
    public class MeseroRepository : IMeseroRepository
    {
        private readonly RestauranteContext _context;
        public MeseroRepository(RestauranteContext context)
        {
            _context = context;
        }
        public async Task<bool> AddMesero(Mesero model)
        {
            await _context.Meseros.AddAsync(model);
            return await Save();
        }
        public async Task<List<Mesero>> GetAllMeseroLambda(Expression<Func<Mesero, bool>> predicate = null)
        {
            if (predicate == null)
            {
                return await _context.Meseros
                    .Include(x => x.Facturas)
                    .ThenInclude(f => f.DetalleFacturas)
                    .ToListAsync();
            }
            return await _context.Meseros
                .Include(x => x.Facturas)
                .ThenInclude(f => f.DetalleFacturas)
                .Where(predicate).ToListAsync();
        }
        public async Task<Mesero?> GetByCondition(Expression<Func<Mesero, bool>> predicate)
        {
            return await _context.Meseros
                .Include(x => x.Facturas)
                .ThenInclude(f => f.DetalleFacturas)
                .FirstOrDefaultAsync(predicate);
        }
        public async Task<bool> RemoveMesero(Mesero model)
        {
            _context.Meseros.Remove(model);
            return await Save();
        }
        public async Task<bool> Save()
        {
            return await _context.SaveChangesAsync() > 0 ? true : false;
        }
        public async Task<bool> UpdateMesero(Mesero model)
        {
            _context.Meseros.Update(model);
            return await Save();
        }
    }
}
