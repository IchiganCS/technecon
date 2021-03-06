using Microsoft.EntityFrameworkCore;
using Technecon.Data;

namespace Technecon;

public class ApplicationDbContext : DbContext {

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> contextOptions)
        : base(contextOptions) {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
    }

    public DbSet<Artist> Artists { get; set; }
    public DbSet<Opus> Opera { get; set; }
}