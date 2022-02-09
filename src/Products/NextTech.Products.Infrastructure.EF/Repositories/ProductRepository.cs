using LinqKit;
using Microsoft.EntityFrameworkCore;
using NextTech.Products.Application;
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
    public class ProductRepository : IProductRepository
    {
        private readonly ProductDBContext _dbContext;
        public ProductRepository(ProductDBContext context)
        {
            _dbContext = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task AddAsync(Product product, CancellationToken cancellationToken)
        {
            await _dbContext.Products.AddAsync(product, cancellationToken);
        }

        public async Task<int> CountAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.Products.CountAsync(cancellationToken);
        }

        public async Task<Product> Get(int id)
        {
            var product = _dbContext.Products.AsNoTracking().SingleOrDefault(x => x.Id == id);

            return product;
        }

        public async Task<Product?> GetAsync(int id, CancellationToken cancellationToken)
        {
            return await _dbContext.Products
                                    .Include(x => x.Category)
                                    .SingleOrDefaultAsync(x => x.Id == id, cancellationToken: cancellationToken);
        }
        public async Task<IList<Product>> GetProductsAsync(int pageIndex, int pageSize, CancellationToken cancellationToken)
        {
            return await _dbContext.Products
                                   .AsNoTracking()
                                   .OrderBy(x => x.Id)
                                   .Skip((pageIndex - 1) * pageSize)
                                   .Take(pageSize)
                                   .Include(x => x.Category)
                                   .ToListAsync(cancellationToken);
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<IList<Product>> SearchProductsAsync(string filter, int? minimumStockFilter, int? maximumStockFilter, int pageIndex, int pageSize, CancellationToken cancellationToken)
        {
            var searchFilter = SearchProductsFilterExpression(filter, minimumStockFilter, maximumStockFilter);

            return await _dbContext.Products
                                   .AsNoTracking()
                                   .Where(searchFilter)
                                   .OrderBy(x => x.Id)
                                   .Skip((pageIndex - 1) * pageSize)
                                   .Take(pageSize)
                                   .Include(x => x.Category)
                                   .ToListAsync(cancellationToken);
        }

        public async Task<int> SearchProductsCountAsync(string filter, int? minimumStockFilter, int? maximumStockFilter, CancellationToken cancellationToken)
        {
            var searchFilter = SearchProductsFilterExpression(filter, minimumStockFilter, maximumStockFilter);

            return await _dbContext.Products
                .AsNoTracking()
                .Where(searchFilter)
                .CountAsync(cancellationToken);
        }

        public async Task UpdateAsync(Product product, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            _dbContext.Products.Update(product);
        }

        private Expression<Func<Domain.Product, bool>> SearchProductsFilterExpression(string filter, int? minimumStockFilter, int? maximumStockFilter)
        {
            Expression<Func<Domain.Product, bool>> productFilterExpression = x => x.CategoryId.HasValue && x.Stock >= x.Category.MinumumStockQuantity;

            var predicate = PredicateBuilder.New(productFilterExpression);

            if (minimumStockFilter.HasValue) predicate = predicate.And(x => x.Stock >= minimumStockFilter);
            if (maximumStockFilter.HasValue) predicate = predicate.And(x => x.Stock <= maximumStockFilter);
            if (!string.IsNullOrEmpty(filter)) predicate.And(x => (x.Title.Contains(filter) || x.Description.Contains(filter) || x.Category.Name.Contains(filter)));

            return predicate;
        }
    }
}
