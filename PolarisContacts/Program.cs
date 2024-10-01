using PolarisContacts.CrossCutting.DependencyInjection;
using PolarisContacts.Filters;
using Prometheus;
using System.Diagnostics;

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

app.UseStaticFiles();

app.UseRouting();

app.UseSession(); // Adiciona o middleware de sessão antes do UseAuthorization

// Adiciona o middleware do Prometheus para métricas
app.UseHttpMetrics();

// Métricas personalizadas
var requestCounter = Metrics.CreateCounter("myapp_http_requests_total", "Total number of HTTP requests received");
var responseLatency = Metrics.CreateHistogram("myapp_http_response_latency_seconds", "HTTP response latency in seconds");

// Métricas de uso de CPU e memória
var cpuUsageGauge = Metrics.CreateGauge("myapp_cpu_usage_total", "Total CPU usage of the application");
var memoryUsageGauge = Metrics.CreateGauge("myapp_memory_usage_bytes", "Total memory usage of the application in bytes");

// Atualizar métricas de CPU e memória a cada requisição
var process = Process.GetCurrentProcess();

app.Use(async (context, next) =>
{
    // Incrementar o contador de requisições
    requestCounter.Inc();

    // Iniciar a medição de latência
    var stopwatch = Stopwatch.StartNew();

    // Executar a próxima parte do pipeline
    await next.Invoke();

    // Parar a medição de latência
    stopwatch.Stop();
    responseLatency.Observe(stopwatch.Elapsed.TotalSeconds);

    // Atualizar métricas de uso de CPU e memória
    cpuUsageGauge.Set(process.TotalProcessorTime.TotalMilliseconds / Environment.ProcessorCount);
    memoryUsageGauge.Set(process.WorkingSet64);
});

app.UseAuthorization();

app.MapMetrics(); // Rota para as métricas do Prometheus

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

public partial class Program { }
