using Microsoft.EntityFrameworkCore;
using Restaurante.Data.Ropository.IRepository;
using Restaurante.Models.Tables;
using System.Linq.Expressions;

namespace Restaurante.Data.Ropository
{
    public class SupervisorRepository : ISupervisorRepository
    {
        private readonly RestauranteContext _context;
        public SupervisorRepository(RestauranteContext context)
        {
            _context = context;
        }
        public async Task<bool> AddSupervisor(Supervisor model)
        {
            await _context.Supervisors.AddAsync(model);
            return await Save();
        }
        public async Task<List<Supervisor>> GetAllSupervisorLambda(Expression<Func<Supervisor, bool>> predicate = null)
        {
            if (predicate == null)
            {
                return await _context.Supervisors
                    .ToListAsync();
            }
            return await _context.Supervisors
                .Where(predicate).ToListAsync();
        }
        public async Task<Supervisor?> GetByCondition(Expression<Func<Supervisor, bool>> predicate)
        {
            return await _context.Supervisors
                .FirstOrDefaultAsync(predicate);
        }
        public async Task<bool> RemoveSupervisor(Supervisor model)
        {
            _context.Supervisors.Remove(model);
            return await Save();
        }
        public async Task<bool> Save()
        {
            return await _context.SaveChangesAsync() > 0 ? true : false;
        }
        public async Task<bool> UpdateSupervisor(Supervisor model)
        {
            _context.Supervisors.Update(model);
            return await Save();
        }
    }
}
