using Restaurante.Models.Tables;
using System.Linq.Expressions;

namespace Restaurante.Data.Ropository.IRepository
{
    public interface IClienteRepository
    {
        Task<bool> AddCliente(Cliente model);
        Task<bool> UpdateCliente(Cliente model);
        Task<bool> RemoveCliente(Cliente model);
        Task<List<Cliente>> GetAllClienteLambda(Expression<Func<Cliente, bool>> predicate = null);
        Task<Cliente?> GetByCondition(Expression<Func<Cliente, bool>> predicate);
    }
}
