using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ModelHouse.Security.Authorization.Handlers.Implementations;
using ModelHouse.Security.Authorization.Handlers.Interfaces;
using ModelHouse.Security.Authorization.Middleware;
using ModelHouse.Security.Authorization.Settings;
using ModelHouse.Security.Domain.Repositories;
using ModelHouse.Security.Domain.Services;
using ModelHouse.Security.Persistence.Repositories;
using ModelHouse.Security.Services;
using ModelHouse.ServiceManagement.Domain.Repositories;
using ModelHouse.ServiceManagement.Domain.Services;
using ModelHouse.ServiceManagement.Persistence.Repositories;
using ModelHouse.ServiceManagement.Services;
using ModelHouse.Shared.Domain.Repositories;
using ModelHouse.Shared.Persistence.Contexts;
using ModelHouse.Shared.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);
var  MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "ACME Model House API",
        Description = "ACME Model House Web Services",
        Contact = new OpenApiContact
        {
            Name = "ACME.studio",
            Url = new Uri("https://acme.studio")
        }
    });
    options.EnableAnnotations();
    options.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "Jwt",
        Description = "Jwt Authorization header using Bearer scheme."
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "bearerAuth"
                }
            },
            Array.Empty<string>()

        }
    });
});

// Add Database Connection
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(
    options => options.UseMySQL(connectionString)
        .LogTo(Console.WriteLine, LogLevel.Information)
        .EnableSensitiveDataLogging()
        .EnableDetailedErrors());



// Add lower case routes
builder.Services.AddRouting(
    options => options.LowercaseUrls = true);


// CORS Service addition

builder.Services.AddCors();

// Dependency Injection Configuration

// Shared Injection Configuration

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Learning Injection Configuration
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IRequestRepository, RequestRepository>();
builder.Services.AddScoped<IRequestService, RequestService>();
builder.Services.AddScoped<IFavoritesRepository, FavoriteRepository>();
builder.Services.AddScoped<IFavoriteService, FavoriteService>();
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddHttpContextAccessor();

// Security Injection Configuration

builder.Services.AddScoped<IJwtHandler, JwtHandler>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IBusinessProfileRepository, BusinessProfileRepository>();
builder.Services.AddScoped<IBusinessProfileService, BusinessProfileService>();


// AutoMapper Configuration

builder.Services.AddAutoMapper(
    typeof(ModelHouse.Security.Mapping.ModelToResourceProfile), 
    typeof(ModelHouse.Security.Mapping.ResourceToModelProfile),
    typeof(ModelHouse.ServiceManagement.Mapping.ModelToResourceProfile),
    typeof(ModelHouse.ServiceManagement.Mapping.ResourceToModelProfile)

    );

var app = builder.Build();

using (var scope = app.Services.CreateScope())
using (var context = scope.ServiceProvider.GetService<AppDbContext>())
{
    context.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure CORS

app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseMiddleware<JwtMiddleware>();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();