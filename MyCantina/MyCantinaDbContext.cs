using System.Data.Entity;

namespace MyCantina
{
    class MyCantinaDbContext : DbContext
    {
        public MyCantinaDbContext() : base()
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Cantina> Cantinas { get; set; }
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLine> OrderLines { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(c => c.Orders)
                .WithRequired(o => o.User);

            modelBuilder.Entity<Cantina>()
                .HasMany(c => c.Dishes)
                .WithRequired(o => o.Cantina);

            modelBuilder.Entity<Order>()
                .HasMany(c => c.OrderLines)
                .WithRequired(o => o.Order);

            modelBuilder.Entity<OrderLine>()
                .HasRequired(c => c.Order)
                .WithMany(o => o.OrderLines)
                .HasForeignKey(o => o.OrderID);

            
        }
    }
}
