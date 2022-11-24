using KUSYS_Demo.DbSeeds;
using KUSYS_Demo.Models.Domain;
using KUSYS_Demo.Repositories;
using KUSYS_Demo.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ConnStr")));

// For Identity  
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

//builder.Services.ConfigureApplicationCookie(options => options.LoginPath = "/Authenticate/Login");
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.LoginPath = "/Authenticate/Login";
});




// For Dependency Injection

builder.Services.AddScoped<IAuthenticationUserService, AuthenticationUserService>();
builder.Services.AddScoped<IService<KUSYS_Demo.Models.Domain.ApplicationUser>, StudentService>();
builder.Services.AddScoped<IService<KUSYS_Demo.Models.DTO.Courses>, CourseService>();


// For RazorPages

//  builder.Services.AddRazorPages();


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


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<ApplicationDbContext>();
    context.Database.EnsureCreated();
    DbSeed.Initialize(context);

}


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Authenticate}/{action=Login}/{id?}");


app.MapControllerRoute(
    name: "Student",
    pattern: "{controller=Student}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "Course",
    pattern: "{controller=Course}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "Logout",
    pattern: "{controller=Authenticate}/{action=Logout}/{id?}");

app.Run();
