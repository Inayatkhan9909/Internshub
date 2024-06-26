using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Internshub.Data;
using Internshub.Models;
using CloudinaryDotNet;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// adding db context
builder.Services.AddDbContext<RegistrationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MainDbConnection")));

builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("AuthDbConnection")));

builder.Services.AddDbContext<EnrollDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("MainDbConnection")));

builder.Services.AddDbContext<AddInternshipDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("MainDbConnection")));

//builder.Services.AddIdentity<IdentityUser, IdentityRole>()
//    .AddEntityFrameworkStores<AuthDbContext>()
//    .AddDefaultTokenProviders();


builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();






//builder.Services.AddDbContext<AuthDbContext>(options =>
//  options.UseSqlServer(builder.Configuration.GetConnectionString("AuthDbConnection")));

Account account = new Account(
  "dogad6vvx",
  "589848595814615",
  "I2J3UdRH5EWPoDEC__GLimQSjE0");

Cloudinary cloudinary = new Cloudinary(account);

builder.Services.AddSingleton<Cloudinary>(cloudinary);

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
