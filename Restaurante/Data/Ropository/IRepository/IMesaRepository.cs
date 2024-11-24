using Restaurante.Models.Tables;
using System.Linq.Expressions;

namespace Restaurante.Data.Ropository.IRepository
{
    public interface IMesaRepository
    {
        Task<bool> AddMesa(Mesa model);
        Task<bool> UpdateMesa(Mesa model);
        Task<bool> RemoveMesa(Mesa model);
        Task<List<Mesa>> GetAllMesaLambda(Expression<Func<Mesa, bool>> predicate = null);
        Task<Mesa?> GetByCondition(Expression<Func<Mesa, bool>> predicate);
    }
}
