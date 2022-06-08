using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.EnvironmentVariables;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();



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
    host = "localhost";
    password = builder.Configuration["dbPass"];
}

contextOptionsBuilder.UseNpgsql($"Host={host};Database=technecon;Username={user};Password={password}");
ApplicationDbContext.StandardOptions = contextOptionsBuilder.Options;

// Configure the HTTP request pipeline.
if (app.Environment.IsProduction()) {
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
