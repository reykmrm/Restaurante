using Microsoft.EntityFrameworkCore;
using Restaurante.Data.Ropository.IRepository;
using Restaurante.Models.Tables;
using System.Linq.Expressions;

namespace Restaurante.Data.Ropository
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly RestauranteContext  _context;
        public ClienteRepository(RestauranteContext context)
        {
            _context = context;
        }
        public async Task<bool> AddCliente(Cliente model)
        {
            await _context.Clientes.AddAsync(model);
            return await Save();

        }

        // Existing code...

        public async Task<List<Cliente>> GetAllClienteLambda(Expression<Func<Cliente, bool>> predicate = null)
        {
            if (predicate == null)
            {
                return await _context.Clientes
                    .Include(x => x.Facturas)
                    .ThenInclude(x => x.DetalleFacturas)
                    .ToListAsync();
            }
            return await _context.Clientes
                .Where(predicate).ToListAsync();
        }

        public async Task<Cliente?> GetByCondition(Expression<Func<Cliente, bool>> predicate)
        {
            return await _context.Clientes
                .FirstOrDefaultAsync(predicate);
        }

        public async Task<bool> RemoveCliente(Cliente model)
        {
            _context.Clientes.Remove(model);
            return await Save();
        }

        public async Task<bool> Save()
        {
            return await _context.SaveChangesAsync() > 0 ? true : false;
        }

        public async Task<bool> UpdateCliente(Cliente model)
        {
            _context.Clientes.Update(model);
            return await Save();
        }
    }
}

