using Coop.Models;
using Microsoft.EntityFrameworkCore;

namespace Coop.Data
{
    public class ApplicationDBContecxt:DbContext
    {
        public ApplicationDBContecxt(DbContextOptions<ApplicationDBContecxt>options):base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Assortment> Assortments { get; set; }
        public DbSet<AssortmentProduct>AssortmentProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AssortmentProduct>()
                .HasKey(ap => new { ap.AssortmentId, ap.ProductId });

            modelBuilder.Entity<AssortmentProduct>()
                .HasOne<Product>(ap => ap.Products)
                .WithMany(ap => ap.AssortmentProducts)
                .HasForeignKey(ap => ap.ProductId);

            modelBuilder.Entity<AssortmentProduct>()
                .HasOne<Assortment>(ap => ap.Assortments)
                .WithMany(ap => ap.AssortmentProducts)
                .HasForeignKey(ap => ap.AssortmentId);
        }

    }
}
