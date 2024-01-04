using Hanssens.Net;
using HR.LeaveManagement.MVC.Middleware;
using HR.LeaveManagment.MVC.Contracts;
using HR.LeaveManagment.MVC.Services;
using HR.LeaveManagment.MVC.Services.Base;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient<IClient, Client>(el => el.BaseAddress = new Uri("https://localhost:7034/"));
builder.Services.AddHttpContextAccessor();
builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.MinimumSameSitePolicy = SameSiteMode.None;
});
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
builder.Services.AddTransient<IAuthenticationService,AuthenticationService>();
//Add services 
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddScoped<ILeaveTypeService, LeaveTypeService>();
builder.Services.AddScoped<ILeaveAllocationService,LeaveAllocationService>();
builder.Services.AddScoped<ILeaveRequestService,LeaveRequestService>();
builder.Services.AddSingleton<ILocalStorageServices, LocalStorageService>();  
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseCookiePolicy();
app.UseAuthentication();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseMiddleware<RequestMiddleware>();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
