using Articles.Initializer;
using Articles.Models;
using Articles.Models.DbModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

IServiceCollection services = builder.Services;

string connection = builder.Configuration.GetConnectionString("DefaultConnection");
services.AddDbContext<ArticlesContext>(opts =>
{
    opts.UseSqlServer(connection);
    opts.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<ArticlesContext>();
services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(opts => opts.LoginPath = "/Account/Register"
);

services.AddRazorPages().AddRazorRuntimeCompilation();



WebApplication app = builder.Build();


if (app.Environment.IsDevelopment())
    app.UseDeveloperExceptionPage();
else
    app.UseHsts();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

using (IServiceScope serviceScope = app.Services.CreateScope())
{
    IServiceProvider serviceProvider = serviceScope.ServiceProvider;
    var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    await RoleInitializer.InitializeAsync(userManager, roleManager);
}

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();

