using Microsoft.EntityFrameworkCore;
using ModelHouse.Profile.Domain.Models;
using ModelHouse.Security.Domain.Models;
using ModelHouse.Shared.Extensions;

namespace ModelHouse.Shared.Persistence.Contexts;

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
    public DbSet<Post> Posts { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<Message> Messages { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

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

        builder.Entity<Account>()
            .HasMany(p => p.Orders)
            .WithOne(p => p.Account)
            .HasForeignKey(p => p.AccountId);
        builder.Entity<Account>()
            .HasMany(p => p.Posts)
            .WithOne(p => p.Account)
            .HasForeignKey(p => p.AccountId);
        
        builder.Entity<Post>()
            .HasMany(p => p.Orders)
            .WithOne(p => p.Post)
            .HasForeignKey(p => p.PostId);
        
        builder.Entity<Order>().ToTable("Orders");
        builder.Entity<Order>().HasKey(p => p.Id);
        builder.Entity<Order>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Order>().Property(p => p.Title).IsRequired().HasMaxLength(30);
        builder.Entity<Order>().Property(p => p.Description).IsRequired().HasMaxLength(200);

        builder.Entity<Post>().ToTable("Posts");
        builder.Entity<Post>().HasKey(p => p.Id);
        builder.Entity<Post>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Post>().Property(p => p.Price).IsRequired();
        builder.Entity<Post>().Property(p => p.Title).IsRequired().HasMaxLength(50);
        builder.Entity<Post>().Property(p => p.Category).IsRequired().HasMaxLength(100);
        builder.Entity<Post>().Property(p => p.Location).IsRequired().HasMaxLength(100);
        builder.Entity<Post>().Property(p => p.Description).IsRequired().HasMaxLength(200);;
        builder.Entity<Post>().Property(p => p.Foto).IsRequired();
        
        builder.Entity<Account>()
            .HasMany(p => p.Notifications)
            .WithOne(p => p.Account)
            .HasForeignKey(p => p.AccountId);
        
        builder.Entity<Notification>().ToTable("Notifications");
        builder.Entity<Notification>().HasKey(p => p.Id);
        builder.Entity<Notification>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Notification>().Property(p => p.Title).IsRequired().HasMaxLength(50);
        builder.Entity<Notification>().Property(p => p.Description).IsRequired().HasMaxLength(200);
        
        builder.Entity<Account>()
            .HasMany(p => p.Contacts)
            .WithOne(p => p.Account)
            .HasForeignKey(p => p.ContactId);
        
        builder.Entity<Contact>().ToTable("Contacts");
        builder.Entity<Contact>().HasKey(p => p.Id);
        builder.Entity<Contact>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Contact>().Property(p => p.UserId).IsRequired();
        builder.Entity<Contact>().Property(p => p.ContactId).IsRequired();
        
        builder.Entity<Contact>()
            .HasMany(p => p.Messages)
            .WithOne(p => p.Contact)
            .HasForeignKey(p => p.ContactId);
        
        builder.Entity<Message>().ToTable("Messages");
        builder.Entity<Message>().HasKey(p => p.Id);
        builder.Entity<Message>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Message>().Property(p => p.Content).IsRequired();
        builder.Entity<Message>().Property(p => p.ShippingTime).IsRequired();
        builder.Entity<Message>().Property(p => p.isMe).IsRequired();
        builder.Entity<Message>().Property(p => p.UserId).IsRequired();
        builder.Entity<Message>().Property(p => p.ContactId).IsRequired();
        
        // Apply Naming Conventions
        builder.UseSnakeCaseNamingConvention();
    }
}