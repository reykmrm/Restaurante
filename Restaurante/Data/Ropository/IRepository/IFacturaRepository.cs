using Restaurante.Models.Tables;
using System.Linq.Expressions;

namespace Restaurante.Data.Ropository.IRepository
{
    public interface IFacturaRepository
    {
        Task<bool> AddFactura(Factura model);
        Task<bool> UpdateFactura(Factura model);
        Task<bool> RemoveFactura(Factura model);
        Task<List<Factura>> GetAllFacturaLambda(Expression<Func<Factura, bool>> predicate = null);
        Task<Factura?> GetByCondition(Expression<Func<Factura, bool>> predicate);
    }
}
