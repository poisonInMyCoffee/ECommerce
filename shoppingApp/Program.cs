using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ShoppingApp;
using ShoppingApp.DataAccess.Data;
using ShoppingApp.DataAccess.DbInitializer;
using ShoppingApp.DataAccess.Repository;
using ShoppingApp.DataAccess.Repository.IRepository;
using ShoppingApp.Utilities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    options.EnableSensitiveDataLogging();
});
 //This line establishes connection between Ef Core and SQL Server Database

//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ApplicationDbContext>();
//Adds user along with the identity
builder.Services.AddIdentity<IdentityUser,IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders(); //soo that we can go ahead with any email id, even if it doesn't exist

builder.Services.ConfigureApplicationCookie(options => {
    options.LoginPath = $"/Identity/Account/LogIn";
    options.LogoutPath = $"/Identity/Account/LogOut";
    options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
}); //to view access denied when you you visiting restricted url


//Adding fb for login
builder.Services.AddAuthentication().AddFacebook(option =>
{
    option.AppId = "2511509435875644";
    option.AppSecret = "9e75d2f393a83775102271767cbeb357";
});

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(100);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddScoped<IDbInitializer, DbInitializer>();
builder.Services.AddRazorPages();
//builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IEmailSender, EmailSender>();


 
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
app.UseAuthentication(); //if username and password is valid
app.UseAuthorization();
app.UseSession();
Seeddatabase();
app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{area=customer}/{controller=Home}/{action=Index}/{id?}");

app.Run();

void Seeddatabase()
{
    using(var scope = app.Services.CreateScope())
    {
        var DbInitializer=scope.ServiceProvider.GetRequiredService<IDbInitializer>();
        DbInitializer.Initialize();
    }
}
