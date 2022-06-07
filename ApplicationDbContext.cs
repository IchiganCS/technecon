using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext {
    public static DbContextOptions<ApplicationDbContext>? StandardOptions { get; set; }

    /// <summary>
    /// Default constructor with standard options
    /// </summary>
    /// <typeparam name="ApplicationDbContext"></typeparam>
    /// <returns></returns>
    public static ApplicationDbContext CreateDefault() {
        return new(StandardOptions!);
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> contextOptions)
        : base(contextOptions) {
        
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
    }

    public DbSet<Artist> Artists { get; set; }
}