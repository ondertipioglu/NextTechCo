using NextTech.Products.Application;
using NextTech.Products.Application.Product;
using NextTech.Products.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NextTech.Products.Infrastructure.EF
{
    public class DataMigrator
    {
        private readonly IProductRepository productRepository;
        private readonly ICategoryRepository categoryRepository;

        public DataMigrator(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            this.productRepository = productRepository;
            this.categoryRepository = categoryRepository;
        }
        public async Task Seed()
        {
            await SeedCategories();
            await SeedProducts();
        }

        public async Task SeedCategories()
        {
            var cancellationToken = new CancellationTokenSource().Token;

            foreach (var item in GetCategories())
            {
                var category = await categoryRepository.GetAsync(item.id, cancellationToken);
                if (category == null)
                    await categoryRepository.AddAsync(item.category, cancellationToken);
            }
            await categoryRepository.SaveChangesAsync(cancellationToken);
        }
        public async Task SeedProducts()
        {
            var cancellationToken = new CancellationTokenSource().Token;

            foreach (var item in GetProducts())
            {
                var product = await productRepository.GetAsync(item.id, cancellationToken);

                if (product == null)
                    await productRepository.AddAsync(item.product, cancellationToken);
            }
            await productRepository.SaveChangesAsync(cancellationToken);
        }
        IEnumerable<(int id, Category category)> GetCategories()
        {
            yield return (1, Category.Create("Category 1", 100));
            yield return (2, Category.Create("Category 2", 200));
            yield return (3, Category.Create("Category 3", 300));
            yield return (4, Category.Create("Category 4", 400));
            yield return (5, Category.Create("Category 5", 500));
        }

        IEnumerable<(int id, Product product)> GetProducts()
        {
            yield return (1, Product.Create(DateTime.Now, "Product 1 title(no category)", "Product 1 Description", 120, true, null));
            yield return (2, Product.Create(DateTime.Now, "Product 2 title(no category)", "Product 2 Description", 220, true, null));
            yield return (3, Product.Create(DateTime.Now, "Product 3 title(no category)", "Product 3 Description", 320, true, null));
            yield return (4, Product.Create(DateTime.Now, "Product 4 title(no category)", "Product 4 Description", 420, true, null));
            yield return (5, Product.Create(DateTime.Now, "Product 5 title(no category)", "Product 5 Description", 520, true, null));
            yield return (6, Product.Create(DateTime.Now, "Product 6 title(no category)", "Product 5 Description", 120, true, null));
            yield return (7, Product.Create(DateTime.Now, "Product 7 title(no category)", "Product 5 Description", 220, true, null));
            yield return (8, Product.Create(DateTime.Now, "Product 8 title(no category)", "Product 5 Description", 320, true, null));
            yield return (9, Product.Create(DateTime.Now, "Product 9 title(no category)", "Product 5 Description", 420, true, null));
            yield return (10, Product.Create(DateTime.Now, "Product 10 title(no category)", "Product 5 Description", 520, true, null));
            yield return (11, Product.Create(DateTime.Now, "Product 11 title(with category)", "Product 11 Description", 120, true, 1));
            yield return (12, Product.Create(DateTime.Now, "Product 12 title(with category)", "Product 12 Description", 220, true, 2));
            yield return (13, Product.Create(DateTime.Now, "Product 13 title(with category)", "Product 13 Description", 320, true, 3));
            yield return (14, Product.Create(DateTime.Now, "Product 14 title(with category)", "Product 14 Description", 420, true, 4));
            yield return (15, Product.Create(DateTime.Now, "Product 15 title(with category)", "Product 15 Description", 520, true, 5));
            yield return (16, Product.Create(DateTime.Now, "Product 16 title(with category)", "Product 16 Description", 120, true, 1));
            yield return (17, Product.Create(DateTime.Now, "Product 17 title(with category)", "Product 17 Description", 220, true, 2));
            yield return (18, Product.Create(DateTime.Now, "Product 18 title(with category)", "Product 18 Description", 320, true, 3));
            yield return (19, Product.Create(DateTime.Now, "Product 19 title(with category)", "Product 19 Description", 420, true, 4));
            yield return (20, Product.Create(DateTime.Now, "Product 20 title(with category)", "Product 20 Description", 520, true, 5));
        }
    }
}