using Restaurante.Models.Tables;
using System.Linq.Expressions;

namespace Restaurante.Data.Ropository.IRepository
{
    public interface ISupervisorRepository
    {
        Task<bool> AddSupervisor(Supervisor model);
        Task<bool> UpdateSupervisor(Supervisor model);
        Task<bool> RemoveSupervisor(Supervisor model);
        Task<List<Supervisor>> GetAllSupervisorLambda(Expression<Func<Supervisor, bool>> predicate = null);
        Task<Supervisor?> GetByCondition(Expression<Func<Supervisor, bool>> predicate);
    }
}
