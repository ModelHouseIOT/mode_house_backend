using Microsoft.EntityFrameworkCore;
using ModelHouse.Security.Domain.Models;
using ModelHouse.ServiceManagement.Domain.Models;
using ModelHouse.Shared.Extensions;

namespace ModelHouse.Shared.Persistence.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<BusinessProfile> BusinessProfiles { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Request> Request { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Favorite> Favorites { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Notification>().ToTable("Notifications");
            builder.Entity<Notification>().HasKey(p => p.Id);
            builder.Entity<Notification>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Notification>().Property(p => p.Description).IsRequired().HasMaxLength(200);
            builder.Entity<Notification>().Property(p => p.Title).IsRequired().HasMaxLength(30);
            builder.Entity<Notification>().Property(p => p.StartDate).IsRequired();
            builder.Entity<Notification>().Property(p => p.AccountId).IsRequired();
        
            builder.Entity<Favorite>().ToTable("Favorites");
            builder.Entity<Favorite>().HasKey(p => p.Id);
            builder.Entity<Favorite>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Favorite>().Property(p => p.CreationDate).IsRequired();
            builder.Entity<Favorite>().Property(p => p.View).IsRequired();
            builder.Entity<Favorite>().Property(p => p.AccountId).IsRequired();
        
            builder.Entity<Request>().ToTable("Requests");
            builder.Entity<Request>().HasKey(p => p.Id);
            builder.Entity<Request>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Request>().Property(p => p.Description).IsRequired().HasMaxLength(200);
            builder.Entity<Request>().Property(p => p.Category).IsRequired();
            builder.Entity<Request>().Property(p => p.Precio).IsRequired();
            builder.Entity<Request>().Property(p => p.RequestDate).IsRequired();
            builder.Entity<Request>().Property(p => p.EstimatedTime).IsRequired();
            builder.Entity<Request>().Property(p => p.BusinessProfileId);
            builder.Entity<Request>().Property(p => p.UserId).IsRequired();

            builder.Entity<BusinessProfile>()
                .HasMany(p => p.Requests)
                .WithOne(p => p.BusinessProfile)
                .HasForeignKey(p => p.BusinessProfileId);

            builder.Entity<Order>().ToTable("Orders");
            builder.Entity<Order>().HasKey(p => p.Id);
            builder.Entity<Order>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Order>().Property(p => p.Description).IsRequired().HasMaxLength(200);
            builder.Entity<Order>().Property(p => p.Category).IsRequired();
            builder.Entity<Order>().Property(p => p.Location).IsRequired();
            builder.Entity<Order>().Property(p => p.Status).IsRequired();
            builder.Entity<Order>().Property(p => p.OrderDate).IsRequired();
            builder.Entity<Order>().Property(p => p.UserId).IsRequired();
            builder.Entity<Order>().Property(p => p.BusinessProfileId).IsRequired();

            builder.Entity<User>()
                .HasMany(p => p.Orders)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserId);

            //Project
            builder.Entity<Project>().ToTable("Projects");
            builder.Entity<Project>().HasKey(p => p.Id);
            builder.Entity<Project>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Project>().Property(p => p.Title).IsRequired();
            builder.Entity<Project>().Property(p => p.Description).IsRequired();
            builder.Entity<Project>().Property(p => p.Image).IsRequired();
            builder.Entity<Project>().Property(p => p.BusinessProfileId).IsRequired();

            //Business Profile
            builder.Entity<BusinessProfile>().ToTable("BusinessProfiles");
            builder.Entity<BusinessProfile>().HasKey(p => p.Id);
            builder.Entity<BusinessProfile>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<BusinessProfile>().Property(p => p.Name).IsRequired();
            builder.Entity<BusinessProfile>().Property(p => p.Description).IsRequired();
            builder.Entity<BusinessProfile>().Property(p => p.Address);
            builder.Entity<BusinessProfile>().Property(p => p.WebSite);
            builder.Entity<BusinessProfile>().Property(p => p.PhoneBusiness);
            builder.Entity<BusinessProfile>().Property(p => p.FoundationDate);
            builder.Entity<BusinessProfile>().Property(p => p.AccountId);

            builder.Entity<BusinessProfile>()
                .HasMany(p => p.Projects)
                .WithOne(p => p.BusinessProfile)
                .HasForeignKey(p => p.BusinessProfileId);

            //Profile user
            builder.Entity<User>().ToTable("Users");
            builder.Entity<User>().HasKey(p => p.Id);
            builder.Entity<User>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<User>().Property(p => p.FirstName).IsRequired();
            builder.Entity<User>().Property(p => p.LastName).IsRequired();
            builder.Entity<User>().Property(p => p.Image);
            builder.Entity<User>().Property(p => p.Gender).IsRequired();
            builder.Entity<User>().Property(p => p.RegistrationDate);
            builder.Entity<User>().Property(p => p.LastLogin);
            builder.Entity<User>().Property(p => p.AccountStatus).IsRequired();

            // Users
            builder.Entity<Account>().ToTable("Accounts");
            builder.Entity<Account>().HasKey(p => p.Id);
            builder.Entity<Account>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Account>().Property(p => p.EmailAddress).IsRequired();
            builder.Entity<Account>().Property(p => p.PasswordHash).IsRequired();
            builder.Entity<Account>().Property(p => p.IsActive);
            builder.Entity<Account>().Property(p => p.DateCreate);
            builder.Entity<Account>().Property(p => p.LastLogin);
            builder.Entity<Account>().Property(p => p.Role);
            builder.Entity<Account>().Property(p => p.UserId);
            builder.Entity<Account>().Property(p => p.BusinessProfileId);
        
            builder.Entity<Account>()
                .HasOne(e => e.BusinessProfile)
                .WithOne(d => d.Account)
                .HasForeignKey<BusinessProfile>(d => d.AccountId);
            builder.Entity<Account>()
                .HasOne(e => e.User)
                .WithOne(d => d.Account)
                .HasForeignKey<User>(d => d.AccountId);
        
            // Apply Naming Conventions
            builder.UseSnakeCaseNamingConvention();
        }
    }
}