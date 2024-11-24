using Restaurante.Models.Tables;
using System.Linq.Expressions;

namespace Restaurante.Data.Ropository.IRepository
{
    public interface IDetalleFacturaRepository
    {
        Task<bool> AddDetalleFactura(DetalleFactura model);
        Task<bool> UpdateDetalleFactura(DetalleFactura model);
        Task<bool> RemoveDetalleFactura(DetalleFactura model);
        Task<List<DetalleFactura>> GetAllDetalleFacturaLambda(Expression<Func<DetalleFactura, bool>> predicate = null);
        Task<DetalleFactura?> GetByCondition(Expression<Func<DetalleFactura, bool>> predicate);
    }
}
