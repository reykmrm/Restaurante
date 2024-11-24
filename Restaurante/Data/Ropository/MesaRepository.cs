using Microsoft.EntityFrameworkCore;
using Restaurante.Data.Ropository.IRepository;
using Restaurante.Models.Tables;
using System.Linq.Expressions;

namespace Restaurante.Data.Ropository
{
    public class MesaRepository : IMesaRepository
    {
        private readonly RestauranteContext _context;
        public MesaRepository(RestauranteContext context)
        {
            _context = context;
        }
        public async Task<bool> AddMesa(Mesa model)
        {
            await _context.Mesas.AddAsync(model);
            return await Save();
        }
        public async Task<List<Mesa>> GetAllMesaLambda(Expression<Func<Mesa, bool>> predicate = null)
        {
            if (predicate == null)
            {
                return await _context.Mesas
                    .ToListAsync();
            }
            return await _context.Mesas
                .Where(predicate).ToListAsync();
        }
        public async Task<Mesa?> GetByCondition(Expression<Func<Mesa, bool>> predicate)
        {
            return await _context.Mesas
                .FirstOrDefaultAsync(predicate);
        }
        public async Task<bool> RemoveMesa(Mesa model)
        {
            _context.Mesas.Remove(model);
            return await Save();
        }
        public async Task<bool> Save()
        {
            return await _context.SaveChangesAsync() > 0 ? true : false;
        }
        public async Task<bool> UpdateMesa(Mesa model)
        {
            _context.Mesas.Update(model);
            return await Save();
        }
    }
}
