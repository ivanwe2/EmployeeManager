using DataAccessLayer;
using DataAccessLayer.Repositories;
using BusinessLayer;
using Microsoft.EntityFrameworkCore;
using BusinessLayer.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using BusinessLayer.Services.EntityServices;
using DataAccessLayer.Entities;
using API.Models;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

//Dependency injection
// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<EmployeeManagerAssingmentDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});


//Transient objects are always different; a new instance is provided to every controller and every service.
//Scoped objects are the same within a request, but different across different requests.
//Singleton objects are the same for every object and every request.


builder.Services.AddTransient(typeof(IRepository<>),typeof(Repository<>));

builder.Services.AddTransient(typeof(IEntityService<>),typeof(EntityService<>));

//Register groups of services with extension methods
//instances of these will be used in the controllers
builder.Services.AddScoped<EmployeeService>();
builder.Services.AddScoped<EmployerService>();
builder.Services.AddScoped<TaskService>();


//login

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
    .AddCookie(options =>
    {
        options.LoginPath = "/Login";
        options.AccessDeniedPath = "/Denied";
        options.LogoutPath = "/Logout";
    });

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


app.Run();
