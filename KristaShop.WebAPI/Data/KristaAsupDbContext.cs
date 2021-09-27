using Microsoft.EntityFrameworkCore;

namespace KristaShop.WebAPI.Data
{
    public class KristaAsupDbContext : DbContext
    {
        public KristaAsupDbContext
               (DbContextOptions<KristaAsupDbContext> options)
               : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserRate>()
                .HasKey(wt => new { wt.user_id });

            builder.Entity<UserGroupMembership>()
                .HasKey(wt => new { wt.user_id, wt.group_id });
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<UserGroupMembership> UserGroupMemberships { get; set; }
        public DbSet<AccessControl> AccessControls { get; set; }
        public DbSet<UserRate> UserRates { get; set; }
        public DbSet<Counterparty> Counterparties { get; set; }
        public DbSet<WebApiRequest> WebApiRequests { get; set; }
        public DbSet<ClientCounter> ClientCounters { get; set; }
    }
}
