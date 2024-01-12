using Microsoft.EntityFrameworkCore;
using SnackboxAPI.Models;

namespace SnackboxAPI
{
    public class ApplicationdbContext : DbContext
    {
        public ApplicationdbContext(DbContextOptions<ApplicationdbContext> options) : base(options)
        {
        }

        public DbSet<Menu> Menu { get; set; }
        public DbSet<Items> Items { get; set; }
        public DbSet<SubItems> SubItems { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<MenuResult> MenuResults { get; set; }
        public DbSet<Snack> Snacks { get; set; }
        public DbSet<Dessert> Desserts { get; set; }
        public DbSet<Addon> Addons { get; set; }
        public DbSet<Drink> Drinks { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<QueryResult> QueryResult { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categories>().HasKey(c => c.category_id);

            modelBuilder.Entity<Items>().HasKey(i => i.item_id);

            modelBuilder.Entity<SubItems>().HasKey(s => s.subitem_id);

            modelBuilder.Entity<Menu>().HasKey(m => m.id);

            modelBuilder.Entity<MenuResult>().HasNoKey().ToView(null);
            modelBuilder.Entity<QueryResult>().HasNoKey().ToView(null);

			modelBuilder.Entity<Snack>().HasKey(m => m.Id);

            modelBuilder.Entity<Dessert>().HasKey(m => m.Id);
            modelBuilder.Entity<Addon>().HasKey(m => m.Id);

            modelBuilder.Entity<Drink>().HasKey(m => m.Id);
			modelBuilder.Entity<User>(entity =>
			{
				entity.ToTable("User");

				entity.Property(e => e.CreatedDate)
					.HasDefaultValueSql("(getdate())")
					.HasColumnType("datetime");
				entity.Property(e => e.ModifiedDate)
					.HasDefaultValueSql("(getdate())")
					.HasColumnType("datetime");
				entity.Property(e => e.UserRole).HasMaxLength(50);
				entity.Property(e => e.Username).HasMaxLength(100);
			});
		}
    }
}