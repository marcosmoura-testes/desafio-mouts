using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    public class SaleRepository : ISaleRepository
    {
        private readonly DefaultContext _context;

        public SaleRepository(DefaultContext context)
        {
            _context = context;
        }

        public async Task<Sale> CreateAsync(Sale sale, CancellationToken cancellationToken = default)
        {
            await _context.Set<Sale>().AddAsync(sale, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return sale;
        }

        public async Task<bool> DeleteAsync(Guid saleId, CancellationToken cancellationToken = default)
        {
            var sale = await _context.Set<Sale>().FirstOrDefaultAsync(s => s.SaleId == saleId, cancellationToken);
            if (sale == null) return false;

            _context.Set<Sale>().Remove(sale);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<IEnumerable<Sale>> GetAllSalesAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            return await _context.Set<Sale>()
                .AsNoTracking()
                .OrderBy(s => s.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);
        }

        public async Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Set<Sale>()
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.SaleId == id, cancellationToken);
        }

        public async Task UpdateAsync(Sale existingSale)
        {
            _context.Set<Sale>().Update(existingSale);
            await _context.SaveChangesAsync();
        }
    }
}
