using Microsoft.EntityFrameworkCore;
using UnitOfWork.Domain.Models.Security;

namespace UnitOfWork.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration) : DbContext(options)
{
    private readonly IConfiguration Configuration = configuration;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string ConnectionString = Configuration.GetConnectionString("DefaultConnection");
        optionsBuilder.UseSqlite(ConnectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasKey(e => e.Id);
        modelBuilder.Entity<Profile>().HasKey(e => e.Id);
        
        base.OnModelCreating(modelBuilder);
    }

    public virtual DbSet<User> User { get; set; }    
    public virtual DbSet<Profile> Profile { get; set; }    
}