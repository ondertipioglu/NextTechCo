using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NextTech.Products.Domain;

namespace NextTech.Products.Infrastructure.EF.Configurations
{    
    internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            #region Primary Key

            builder.HasKey(q => q.Id);

            #endregion

            #region Properties

            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.Name).IsRequired().HasMaxLength(50);
            builder.Property(p => p.MinumumStockQuantity).IsRequired();

            #endregion

            #region Table & Column Mappings

            builder.ToTable("Category", "Products");

            #endregion
        }
    }
}
