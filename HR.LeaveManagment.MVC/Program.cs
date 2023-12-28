using Hanssens.Net;
using HR.LeaveManagment.MVC.Contracts;
using HR.LeaveManagment.MVC.Services;
using HR.LeaveManagment.MVC.Services.Base;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient<IClient, Client>(el => el.BaseAddress = new Uri("https://localhost:7034/"));
//Add services 
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddScoped<ILeaveTypeService, LeaveTypeService>();
builder.Services.AddSingleton<ILocalStorageServices, LocalStorageService>();  
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
