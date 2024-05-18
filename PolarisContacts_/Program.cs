using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using PolarisContacts.CrossCutting.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.RegisterServices();
builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseExceptionHandler("/Home/Error");
app.UseHsts();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();