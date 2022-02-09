using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NextTech.Products.Domain;

namespace NextTech.Products.Infrastructure.EF.Configurations
{
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            #region Primary Key

            builder.HasKey(q => q.Id);

            #endregion

            #region Properties

            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.Id).IsRequired();
            builder.Property(p => p.CreatedOn).IsRequired();
            builder.Property(p => p.Title).IsRequired();
            builder.Property(p => p.Description).IsRequired();
            builder.Property(p => p.Active).IsRequired();
            builder.Property(p => p.Stock).IsRequired();

            #endregion

            #region Relationship

            builder.HasOne<Category>(p => p.Category)
                .WithMany(g => g.Products)
                .HasForeignKey(p => p.CategoryId);              

            #endregion

            #region Table & Column Mappings

            builder.ToTable("Product", "Products");

            #endregion
        }
    }
}
