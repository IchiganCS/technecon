using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Technecon;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
IMvcBuilder mvcBuilder= builder.Services.AddRazorPages();

string host = string.Empty;
string password = string.Empty;
string user = "postgres";

if (builder.Environment.IsDevelopment()) {
    host = "technecon.de";
    password = builder.Configuration["dbPass"];
}

else if (builder.Environment.IsProduction()) {
    host = "localhost";
    password = builder.Configuration["dbPass"];
}

builder.Services.AddDbContext<ApplicationDbContext>(x => x.UseNpgsql($"Host={host};Database=technecon;UserId={user};Password={password}",
    options =>
    options.EnableRetryOnFailure(
        maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30),
        errorCodesToAdd: null)));

builder.Services.AddServerSideBlazor();

var app = builder.Build();

DbContextOptionsBuilder<ApplicationDbContext> contextOptionsBuilder = new();


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

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
