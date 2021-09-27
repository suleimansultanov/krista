using KristaShop.DataReadOnly.Models;
using Microsoft.EntityFrameworkCore;

namespace KristaShop.DataReadOnly.Domain
{
    public class KristaReplicaDbContext : DbContext
    {
        public KristaReplicaDbContext
               (DbContextOptions<KristaReplicaDbContext> options)
               : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserGroupMembership>()
                .HasKey(wt => new { wt.user_id, wt.group_id });

            builder.Entity<NomFilterColor>()
                .HasKey(bc => bc.color_id);
            builder.Entity<NomFilterColor>()
                .HasOne(bc => bc.Color)
                .WithMany(c => c.NomFilterColors)
                .HasForeignKey(bc => bc.color_id);

            builder.Entity<NomFilterSize>()
                .HasKey(bc => bc.size_id);
            builder.Entity<NomFilterSize>()
                .HasOne(bc => bc.Size)
                .WithMany(c => c.NomFilterSizes)
                .HasForeignKey(bc => bc.size_id);

            builder.Entity<NomFilterSizeLine>()
                .HasKey(bc => bc.size_line_id);
            builder.Entity<NomFilterSizeLine>()
                .HasOne(bc => bc.SizeLine)
                .WithMany(c => c.NomFilterSizeLines)
                .HasForeignKey(bc => bc.size_line_id);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<UserGroupMembership> UserGroupMemberships { get; set; }
        public DbSet<AccessControl> AccessControls { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Counterparty> Counterparties { get; set; }

        public DbSet<CatalogItem> CatalogItems { get; set; }
        public DbSet<Nomenclature> Nomenclatures { get; set; }
        public DbSet<CatalogItemPrice> CatalogItemPrices { get; set; }
        public DbSet<PriceType> PriceTypes { get; set; }

        public DbSet<Option> Option { get; set; }

        public DbSet<Color> Colors { get; set; }
        public DbSet<NomFilterColor> NomFilterColors { get; set; }

        public DbSet<Size> Sizes { get; set; }
        public DbSet<NomFilterSize> NomFilterSizes { get; set; }

        public DbSet<SizeLine> SizeLines { get; set; }
        public DbSet<NomFilterSizeLine> NomFilterSizeLines { get; set; }

        public DbSet<StoreHouse> StoreHouses { get; set; }
        public DbSet<StoreHouseRest> StoreHouseRests { get; set; }

    }
}
