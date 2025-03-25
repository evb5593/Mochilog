using Mochilog.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Mochilog.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false).AddEntityFrameworkStores<ApplicationDbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();

    using (var adminScope = app.Services.CreateScope())
    {
        var services = adminScope.ServiceProvider;
        var userManager = services.GetRequiredService<UserManager<IdentityUser>>();

        string? adminEmail = Environment.GetEnvironmentVariable("ADMIN_EMAIL");
        string? adminPassword = Environment.GetEnvironmentVariable("ADMIN_PASSWORD");

        if (!string.IsNullOrWhiteSpace(adminEmail) && !string.IsNullOrWhiteSpace(adminPassword))
        {
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                var user = new IdentityUser { UserName = adminEmail, Email = adminEmail, EmailConfirmed = true };
                var result = await userManager.CreateAsync(user, adminPassword);
                if (!result.Succeeded)
                {
                    Console.WriteLine("Admin user creation failed:");
                    foreach (var error in result.Errors)
                    {
                        Console.WriteLine($" - {error.Code}: {error.Description}");
                    }
                }
                else
                {
                    Console.WriteLine("Admin user created successfully.");
                }
            }
        }
        else
        {
            Console.WriteLine("ADMIN_EMAIL or ADMIN_PASSWORD environment variable not set. Admin user was not created.");
        }
    }
}

app.Run();
