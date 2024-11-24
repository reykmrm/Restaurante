using Restaurante.Models.Tables;
using System.Linq.Expressions;

namespace Restaurante.Data.Ropository.IRepository
{
    public interface IMeseroRepository
    {
        Task<bool> AddMesero(Mesero model);
        Task<bool> UpdateMesero(Mesero model);
        Task<bool> RemoveMesero(Mesero model);
        Task<List<Mesero>> GetAllMeseroLambda(Expression<Func<Mesero, bool>> predicate = null);
        Task<Mesero?> GetByCondition(Expression<Func<Mesero, bool>> predicate);
    }
}
