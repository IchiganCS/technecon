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

    #pragma warning disable CS8618
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> contextOptions)
        : base(contextOptions) {
        
    }
    #pragma warning restore CS8618

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
    }

    public DbSet<Artist> Artists { get; set; }
}