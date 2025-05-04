using Ambev.DeveloperEvaluation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    public interface ISaleRepository
    {
        Task<Sale> CreateAsync(Sale sale, CancellationToken cancellationToken = default);
        Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(Guid saleId, CancellationToken cancellationToken = default);
        Task UpdateAsync(Sale existingSale);
        Task<IEnumerable<Sale>> GetAllSalesAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
    }
}
