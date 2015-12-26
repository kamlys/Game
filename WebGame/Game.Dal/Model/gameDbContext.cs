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
        public virtual DbSet<Deals> Deals { get; set; }
        public virtual DbSet<DealsBuildings> DealsBuildings { get; set; }
        public virtual DbSet<Dolars> Dolars { get; set; }
        public virtual DbSet<Friends> Friends { get; set; }
        public virtual DbSet<Ignored> Ignored { get; set; }
        public virtual DbSet<Maps> Maps { get; set; }
        public virtual DbSet<Market> Market { get; set; }
        public virtual DbSet<Messages> Messages { get; set; }
        public virtual DbSet<Notifications> Notifications { get; set; }
        public virtual DbSet<ProductRequirements> ProductRequirements { get; set; }
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
                .HasMany(e => e.Deals)
                .WithRequired(e => e.Buildings)
                .HasForeignKey(e => e.Building_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Buildings>()
                .HasMany(e => e.DealsBuildings)
                .WithRequired(e => e.Buildings)
                .HasForeignKey(e => e.Building_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Buildings>()
                .HasMany(e => e.UserBuildings)
                .WithRequired(e => e.Buildings)
                .HasForeignKey(e => e.Building_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Deals>()
                .HasMany(e => e.DealsBuildings)
                .WithRequired(e => e.Deals)
                .HasForeignKey(e => e.Deal_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Maps>()
                .HasMany(e => e.Deals)
                .WithRequired(e => e.Maps)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Messages>()
                .Property(e => e.PostDate)
                .HasPrecision(0);

            modelBuilder.Entity<Products>()
                .HasMany(e => e.Buildings)
                .WithRequired(e => e.Products)
                .HasForeignKey(e => e.Product_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Products>()
                .HasMany(e => e.ProductRequirements)
                .WithRequired(e => e.Products)
                .HasForeignKey(e => e.Base_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Products>()
                .HasMany(e => e.ProductRequirements1)
                .WithRequired(e => e.Products1)
                .HasForeignKey(e => e.Require_ID)
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
                .HasMany(e => e.Deals)
                .WithRequired(e => e.Users)
                .HasForeignKey(e => e.User1_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Deals1)
                .WithRequired(e => e.Users1)
                .HasForeignKey(e => e.User2_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.DealsBuildings)
                .WithRequired(e => e.Users)
                .HasForeignKey(e => e.User_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Dolars)
                .WithRequired(e => e.Users)
                .HasForeignKey(e => e.User_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Friends)
                .WithRequired(e => e.Users)
                .HasForeignKey(e => e.Friend_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Friends1)
                .WithRequired(e => e.Users1)
                .HasForeignKey(e => e.User_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Ignored)
                .WithRequired(e => e.Users)
                .HasForeignKey(e => e.Ignored_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Ignored1)
                .WithRequired(e => e.Users1)
                .HasForeignKey(e => e.User_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Maps)
                .WithRequired(e => e.Users)
                .HasForeignKey(e => e.User_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Market)
                .WithRequired(e => e.Users)
                .HasForeignKey(e => e.User_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Messages)
                .WithRequired(e => e.Users)
                .HasForeignKey(e => e.Customer_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Messages1)
                .WithRequired(e => e.Users1)
                .HasForeignKey(e => e.Sender_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Notifications)
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
