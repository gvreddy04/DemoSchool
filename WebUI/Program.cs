using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using WebUI;
using WebUI.HttpInterceptor;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Add services to the container
//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddHttpClientInterceptor();
builder.Services.AddScoped<HttpInterceptorService>();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) }.EnableIntercept(sp));

builder.Services.AddWebUIServices(); // WebUI Services
builder.Services.AddBlazorBootstrap(); // Blazor.Bootstrap

await builder.Build().RunAsync();