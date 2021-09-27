using KristaShop.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace KristaShop.DataAccess.Domain
{
    public class KristaShopDbContext : DbContext
    {
        public KristaShopDbContext
               (DbContextOptions<KristaShopDbContext> options)
               : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //builder.Entity<NomCatalog>()
            //    .HasKey(nc => new { nc.nom_id, nc.catalog_id });
            builder.Entity<NomCatalog>()
                .HasOne(nc => nc.Nomenclature)
                .WithMany(c => c.NomCatalogs)
                .HasForeignKey(nc => nc.nom_id);
            builder.Entity<NomCatalog>()
                .HasOne(nc => nc.Catalog)
                .WithMany(c => c.NomCatalogs)
                .HasForeignKey(nc => nc.catalog_id);

            //builder.Entity<NomCategory>()
            //    .HasKey(nc => new { nc.nom_id, nc.category_id });
            builder.Entity<NomCategory>()
                .HasOne(nc => nc.Nomenclature)
                .WithMany(c => c.NomCategories)
                .HasForeignKey(nc => nc.nom_id);
            builder.Entity<NomCategory>()
                .HasOne(nc => nc.Category)
                .WithMany(c => c.NomCategories)
                .HasForeignKey(nc => nc.category_id);

            builder.Entity<VisibleNomUser>()
                .HasOne(nc => nc.Nomenclature)
                .WithMany(c => c.VisibleNomUsers)
                .HasForeignKey(nc => nc.nom_id);
        }

        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<MenuContent> MenuContents { get; set; }

        public DbSet<AuthorizationLink> AuthorizationLinks { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<UrlAccess> UrlAccess { get; set; }
        public DbSet<Catalog> Catalogs { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }

        public DbSet<Nomenclature> Nomenclatures { get; set; }
        public DbSet<NomCatalog> NomCatalogs { get; set; }
        public DbSet<NomCategory> NomCategories { get; set; }
        public DbSet<VisibleNomUser> VisibleNomUsers { get; set; }

        public DbSet<NotVisibleProdCtlg> NotVisibleProdCtlgs { get; set; }
        public DbSet<NotVisibleProdCtgr> NotVisibleProdCtgrs { get; set; }
        public DbSet<NomProdPrice> NomProdPrices { get; set; }
        public DbSet<NomPhoto> NomPhotos { get; set; }

        public DbSet<NomDiscount> NomDiscounts { get; set; }
        public DbSet<UserDiscount> UserDiscounts { get; set; }
        public DbSet<CatalogDiscount> CatalogDiscounts { get; set; }
    }
}
