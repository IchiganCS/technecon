using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext {
    /// <summary>
    /// Default constructor with standard options
    /// </summary>
    /// <typeparam name="ApplicationDbContext"></typeparam>
    /// <returns></returns>
    public ApplicationDbContext() :
        this(new DbContextOptionsBuilder<ApplicationDbContext>().Options) {}
        
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> contextOptions)
        : base(contextOptions) {

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        optionsBuilder.UseNpgsql("Host=technecon.de;Database=technecon;Username=postgres;Password=localDebug");
    }

    public DbSet<Artist> Artists { get; set; }
}