namespace Game.Dal.Model
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class gameDbContext : DbContext
    {
        public gameDbContext()
            : base("name=gameDbContext")
        {
        }

        public virtual DbSet<Admins> Admins { get; set; }
        public virtual DbSet<Bans> Bans { get; set; }
        public virtual DbSet<BuildingQueue> BuildingQueue { get; set; }
        public virtual DbSet<Buildings> Buildings { get; set; }
        public virtual DbSet<Dolars> Dolars { get; set; }
        public virtual DbSet<Maps> Maps { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<UserBuildings> UserBuildings { get; set; }
        public virtual DbSet<UserProducts> UserProducts { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BuildingQueue>()
                .Property(e => e.FinishTime)
                .HasPrecision(0);

            modelBuilder.Entity<Buildings>()
                .HasMany(e => e.UserBuildings)
                .WithRequired(e => e.Buildings)
                .HasForeignKey(e => e.Building_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Products>()
                .HasMany(e => e.Buildings)
                .WithRequired(e => e.Products)
                .HasForeignKey(e => e.Product_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Products>()
                .HasMany(e => e.UserProducts)
                .WithRequired(e => e.Products)
                .HasForeignKey(e => e.Product_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserBuildings>()
                .HasMany(e => e.BuildingQueue)
                .WithRequired(e => e.UserBuildings)
                .HasForeignKey(e => e.UserBuilding_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Admins)
                .WithRequired(e => e.Users)
                .HasForeignKey(e => e.User_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Bans)
                .WithRequired(e => e.Users)
                .HasForeignKey(e => e.User_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.BuildingQueue)
                .WithRequired(e => e.Users)
                .HasForeignKey(e => e.User_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Dolars)
                .WithRequired(e => e.Users)
                .HasForeignKey(e => e.User_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Maps)
                .WithRequired(e => e.Users)
                .HasForeignKey(e => e.User_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.UserBuildings)
                .WithRequired(e => e.Users)
                .HasForeignKey(e => e.User_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.UserProducts)
                .WithRequired(e => e.Users)
                .HasForeignKey(e => e.User_ID)
                .WillCascadeOnDelete(false);
        }
    }
}
