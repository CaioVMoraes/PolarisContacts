using PolarisContacts.CrossCutting.DependencyInjection;
using PolarisContacts.Filters;

var builder = WebApplication.CreateBuilder(args);

// Adiciona configuração para o ambiente de teste
builder.Host.ConfigureAppConfiguration((context, config) =>
{
    var env = context.HostingEnvironment;

    config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
          .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);
});

// Add services to the container.
builder.Services.RegisterServices();
builder.Services.AddControllersWithViews(options =>
{
    // Adiciona o filtro globalmente, exceto em ambientes de teste
    if (!builder.Environment.IsEnvironment("Test"))
    {
        options.Filters.Add(new AuthenticationFilterAttribute());
    }
});

// Add session services
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Tempo de expiração da sessão
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession(); // Adiciona o middleware de sessão antes do UseAuthorization

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

public partial class Program { }
