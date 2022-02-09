using LinqKit;
using Microsoft.EntityFrameworkCore;
using NextTech.Products.Application;
using NextTech.Products.Application.Product;
using NextTech.Products.Domain;
using NextTech.Products.Infrastructure.EF.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NextTech.Products.Infrastructure.EF.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ProductDBContext _dbContext;
        public CategoryRepository(ProductDBContext context)
        {
            _dbContext = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task AddAsync(Category category, CancellationToken cancellationToken)
        {
            await _dbContext.Categories.AddAsync(category, cancellationToken);
        }

        public async Task AddRangeAsync(List<Category> categories, CancellationToken cancellationToken)
        {
            await _dbContext.Categories.AddRangeAsync(categories, cancellationToken);
        }

        public async Task<Category> GetAsync(int id, CancellationToken cancellationToken)
        {
            return await _dbContext.Categories.SingleOrDefaultAsync(x => x.Id == id, cancellationToken: cancellationToken);
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(Category category, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            _dbContext.Categories.Update(category);
        }
    }
}
