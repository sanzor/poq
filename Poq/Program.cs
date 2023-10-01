using Poq;
using Poq.Services;
using HealthChecks.UI.Client;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Serilog;

var builder = WebApplication.CreateBuilder(args);


Serilog.Log.Logger = new LoggerConfiguration()
               .WriteTo.Console()
               .CreateLogger();
// Add services to the container.
builder.Services.AddControllers()
                .AddControllersAsServices();
builder.Services.AddPoqServices(builder.Configuration);
builder.Services.AddValidators();



builder.Services.AddHttpClient();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting()
   .UseEndpoints(ep =>
{
    ep.MapControllers();
});
app.UseAuthorization();
app.Run();
