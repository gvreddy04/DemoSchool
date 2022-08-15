var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Add services to the container
// AddHttpClient is an extension in Microsoft.Extensions.Http
builder.Services.AddHttpClient(
    name: "WebApi",
    configureClient: c =>
    {
        c.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
    })
.AddHttpMessageHandler<CustomHttpMessageHandler>(); // Message Handler / Interceptor

builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("WebApi"));

// Add the handlers to the service collection
builder.Services.AddTransient<CustomHttpMessageHandler>();

builder.Services.AddWebUIServices(); // WebUI Services
builder.Services.AddBlazorBootstrap(); // Blazor.Bootstrap

await builder.Build().RunAsync();