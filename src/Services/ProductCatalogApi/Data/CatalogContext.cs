using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductCatalogApi.Domain;

namespace ProductCatalogApi.Data
{
    public class CatalogContext : DbContext
    {
        public CatalogContext(DbContextOptions options) : base(options) { }

        public DbSet<CatalogItem> CatalogItems { get; set; }
        public DbSet<CatalogBrand> CatalogBrands { get; set; }
        public DbSet<CatalogType> CatalogTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<CatalogItem>(ConfigureCatalogItem);
            builder.Entity<CatalogType>(ConfigureCatalogType);
            builder.Entity<CatalogBrand>(ConfigureCatalogBrand);
        }
        private void ConfigureCatalogItem(EntityTypeBuilder<CatalogItem> builder)
        {
            builder.ToTable("Catalog");
            builder.Property(p => p.Id)
                .ForSqlServerUseSequenceHiLo("catalog_hilo")
                .IsRequired(true);

            builder.Property(p => p.Name)
                .IsRequired(true)
                .HasMaxLength(50);

            builder.Property(p => p.Price)
                .IsRequired(true);

            builder.Property(p => p.PictureUri)
                .IsRequired(false);

            builder.HasOne(c => c.CatalogBrand)
                .WithMany()
                .HasForeignKey(c => c.CatalogBrandId);

            builder.HasOne(c => c.CatalogType)
                .WithMany()
                .HasForeignKey(c => c.CatalogTypeId);

        }
        private void ConfigureCatalogType(EntityTypeBuilder<CatalogType> builder)
        {

            builder.ToTable("CatalogType");
            builder.Property(p => p.Id)
                .ForSqlServerUseSequenceHiLo("catalog_type_hilo")
                .IsRequired(true);

            builder.Property(p => p.Type)
                .IsRequired()
                .HasMaxLength(100);
        }

        private void ConfigureCatalogBrand(EntityTypeBuilder<CatalogBrand> builder)
        {

            builder.ToTable("CatalogBrand");
            builder.Property(p => p.Id)
                .ForSqlServerUseSequenceHiLo("catalog_brand_hilo")
                .IsRequired(true);

            builder.Property(p => p.Brand)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
