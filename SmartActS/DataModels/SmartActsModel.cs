namespace SmartActS.DataModels
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class SmartActSModel : DbContext
    {
        public SmartActSModel()
            : base("name=DefaultConnection")
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<FileAttacth> FileAttacths { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<Rank> Ranks { get; set; }
        public virtual DbSet<Request> Requests { get; set; }
        public virtual DbSet<Response> Responses { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Supply> Supplies { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserClaim> UserClaims { get; set; }
        public virtual DbSet<UserLogin> UserLogins { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .Property(e => e.CategoryCode)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.CustomerCode)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.MobiPhone)
                .IsUnicode(false);

            modelBuilder.Entity<Location>()
                .Property(e => e.LocationCode)
                .IsUnicode(false);

            modelBuilder.Entity<Rank>()
                .Property(e => e.RankCode)
                .IsUnicode(false);

            modelBuilder.Entity<Request>()
                .Property(e => e.RequestCode)
                .IsUnicode(false);

            modelBuilder.Entity<Request>()
                .Property(e => e.FromBudget)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Request>()
                .Property(e => e.ToBudget)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Response>()
                .Property(e => e.PriceSuggest)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Role>()
                .HasMany(e => e.Users)
                .WithMany(e => e.Roles)
                .Map(m => m.ToTable("UserRole").MapLeftKey("RoleId").MapRightKey("UserId"));

            modelBuilder.Entity<Supply>()
                .Property(e => e.SupplyCode)
                .IsUnicode(false);

            modelBuilder.Entity<Supply>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Supply>()
                .Property(e => e.MobiPhone)
                .IsUnicode(false);

            modelBuilder.Entity<Supply>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<Supply>()
                .Property(e => e.Website)
                .IsUnicode(false);
        }
    }
}
