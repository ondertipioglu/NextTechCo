using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NextTech.Products.Application
{
    public interface IProductRepository
    {
        Task<Domain.Product> Get(int id);
        Task UpdateAsync(Domain.Product product, CancellationToken cancellationToken);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        Task<Domain.Product?> GetAsync(int id, CancellationToken cancellationToken);
        Task AddAsync(Domain.Product product, CancellationToken cancellationToken);
        Task<int> CountAsync(CancellationToken cancellationToken);
        Task<IList<Domain.Product>> GetProductsAsync(int pageIndex, int pageSize, CancellationToken cancellationToken);
        Task<IList<Domain.Product>> SearchProductsAsync(string filter, int? minimumStockFilter, int? maximumStockFilter, int pageIndex, int pageSize, CancellationToken cancellationToken);
        Task<int> SearchProductsCountAsync(string filter, int? minimumStockFilter, int? maximumStockFilter, CancellationToken cancellationToken);

    }
}