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
    public DbSet<ProfileUser> Profiles { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<Message> Messages { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        //Profile user
        builder.Entity<ProfileUser>().ToTable("ProfileUsers");
        builder.Entity<ProfileUser>().HasKey(p => p.Id);
        builder.Entity<ProfileUser>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<ProfileUser>().Property(p => p.Name).IsRequired();
        builder.Entity<ProfileUser>().Property(p => p.NameBusiness).IsRequired();
        builder.Entity<ProfileUser>().Property(p => p.LocationBusiness).IsRequired();
        builder.Entity<ProfileUser>().Property(p => p.Image).IsRequired();
        builder.Entity<ProfileUser>().Property(p => p.ImageBusiness).IsRequired();
        builder.Entity<ProfileUser>().Property(p => p.PhoneNumber).IsRequired();

        // Users
        builder.Entity<User>().ToTable("Users");
        builder.Entity<User>().HasKey(p => p.Id);
        builder.Entity<User>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<User>().Property(p => p.EmailAddress).IsRequired();

        builder.Entity<User>()
            .HasMany(p => p.Orders)
            .WithOne(p => p.User)
            .HasForeignKey(p => p.UserId);
        builder.Entity<User>()
            .HasMany(p => p.Posts)
            .WithOne(p => p.User)
            .HasForeignKey(p => p.UserId);
        
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
        
        builder.Entity<User>()
            .HasMany(p => p.Notifications)
            .WithOne(p => p.User)
            .HasForeignKey(p => p.UserId);
        
        builder.Entity<Notification>().ToTable("Notifications");
        builder.Entity<Notification>().HasKey(p => p.Id);
        builder.Entity<Notification>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Notification>().Property(p => p.Title).IsRequired().HasMaxLength(50);
        builder.Entity<Notification>().Property(p => p.Description).IsRequired().HasMaxLength(200);
        
        builder.Entity<User>()
            .HasMany(p => p.Contacts)
            .WithOne(p => p.User)
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