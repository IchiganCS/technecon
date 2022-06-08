using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddRazorPages();


builder.WebHost.UseKestrel(
    serverOptions => {
        serverOptions.ConfigureHttpsDefaults(
            httpsAdapter => {
                httpsAdapter.ServerCertificate = new("/etc/technecon/cert.cer");
            }
        );
    }
);


var app = builder.Build();

DbContextOptionsBuilder<ApplicationDbContext> contextOptionsBuilder = new();

string host = string.Empty;
string password = string.Empty;
string user = "postgres";

if (app.Environment.IsDevelopment()) {
    host = "technecon.de";
    password = builder.Configuration["dbPass"];
}

else if (app.Environment.IsProduction()) {
    //TODO
    host = "localhost"; 
    Console.Write($"Database password for user {user}: " );
    password = Console.ReadLine()!;
    Console.Clear();
}

contextOptionsBuilder.UseNpgsql($"Host={host};Database=technecon;Username={user};Password={password}");
ApplicationDbContext.StandardOptions = contextOptionsBuilder.Options;

// Configure the HTTP request pipeline.
if (app.Environment.IsProduction())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
