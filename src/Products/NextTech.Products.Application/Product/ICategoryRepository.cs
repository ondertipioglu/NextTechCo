using NextTech.Products.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NextTech.Products.Application.Product
{
    public interface ICategoryRepository
    {
        Task UpdateAsync(Category category, CancellationToken cancellationToken);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        Task<Category?> GetAsync(int id, CancellationToken cancellationToken);
        Task AddAsync(Category category, CancellationToken cancellationToken);
        Task AddRangeAsync(List<Category> categories, CancellationToken cancellationToken);
    }
}
