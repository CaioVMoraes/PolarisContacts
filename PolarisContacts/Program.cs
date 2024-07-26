using PolarisContacts.CrossCutting.DependencyInjection;
using PolarisContacts.Filters;
using Prometheus;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.RegisterServices();
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add(new AuthenticationFilterAttribute()); // Adicionar o filtro globalmente
});

// Add session services
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Tempo de expira��o da sess�o
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

app.UseSession(); // Adiciona o middleware de sess�o antes do UseAuthorization

// Adiciona o middleware do Prometheus para m�tricas
app.UseHttpMetrics();

// M�tricas personalizadas
var requestCounter = Metrics.CreateCounter("myapp_http_requests_total", "Total number of HTTP requests received");
var responseLatency = Metrics.CreateHistogram("myapp_http_response_latency_seconds", "HTTP response latency in seconds");

// M�tricas de uso de CPU e mem�ria
var cpuUsageGauge = Metrics.CreateGauge("myapp_cpu_usage_total", "Total CPU usage of the application");
var memoryUsageGauge = Metrics.CreateGauge("myapp_memory_usage_bytes", "Total memory usage of the application in bytes");

// Atualizar m�tricas de CPU e mem�ria a cada requisi��o
var process = Process.GetCurrentProcess();

app.Use(async (context, next) =>
{
    // Incrementar o contador de requisi��es
    requestCounter.Inc();

    // Iniciar a medi��o de lat�ncia
    var stopwatch = Stopwatch.StartNew();

    // Executar a pr�xima parte do pipeline
    await next.Invoke();

    // Parar a medi��o de lat�ncia
    stopwatch.Stop();
    responseLatency.Observe(stopwatch.Elapsed.TotalSeconds);

    // Atualizar m�tricas de uso de CPU e mem�ria
    cpuUsageGauge.Set(process.TotalProcessorTime.TotalMilliseconds / Environment.ProcessorCount);
    memoryUsageGauge.Set(process.WorkingSet64);
});

app.UseAuthorization();

app.MapMetrics(); // Rota para as m�tricas do Prometheus

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
