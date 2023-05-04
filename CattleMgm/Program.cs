using CattleMgm.Data;
using CattleMgm.Data.Entities;
using CattleMgm.Helpers;
using CattleMgm.Models;
using CattleMgm.Repository.Cattles;
using CattleMgm.Repository.CattleTemperature;
using CattleMgm.Repository.Humidity;
using CattleMgm.Repository.Farm;
using CattleMgm.Repository.General;
using CattleMgm.Repository.Position;
using CattleMgm.Repository.Media;
using CattleMgm.Repository.Milk;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;

using Microsoft.EntityFrameworkCore;
using CattleMgm.Repository.CattleBloodPressures;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.AspNetCore.Mvc;
using CattleMgm.Repository.Log;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection"); 
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDbContext<praktikadbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true).AddRoles<ApplicationRole>()
  .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddRazorPages();
builder.Services.AddMvc().AddRazorRuntimeCompilation();
builder.Services.AddControllersWithViews();
builder.Services.AddSession();
builder.Services.AddSingleton<IAuthorizationPolicyProvider, AuthorizationPolicyProvider>();
builder.Services.AddScoped<IMediaRepository, MediaRepository>();
builder.Services.AddScoped<ICattleRepository, CattleRepository>();
builder.Services.AddScoped<IFunctionRepository, FunctionRepository>();
builder.Services.AddScoped<IFarmRepository, FarmRepository>();
builder.Services.AddScoped<IPositionRepository, PositionRepository>();
builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.AddScoped<IMilkRepository, MilkRepository>();
builder.Services.AddScoped<ICattleTempRepository, CattleTempRepository>();
builder.Services.AddScoped<ICattleBloodPressureRepository, CattleBloodPressureRepository>();
builder.Services.AddScoped<IHumidityRepository, HumidityRepository>();
builder.Services.AddScoped<ILogRepository, LogRepository>();

var app = builder.Build();

//using (var scope = app.Services.CreateScope())
//{
//    var serviceProvider = scope.ServiceProvider;
//    var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
//    UserAndRoleDataInitializer.SeedData(userManager);
//}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
