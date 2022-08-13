using Microsoft.Net.Http.Headers;
using WebAPI;

var builder = WebApplication.CreateBuilder(args);
{
    // Add services to the container.
    builder.Services.AddApplicationServices(builder.Configuration);
    builder.Services.AddApplicationInsightsTelemetry(); // TODO: Add application insights instrumentation key in appsettings.json

    // CORS
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("CorsPolicy",
                        builder =>
                        {
                            builder
                            .WithOrigins("https://localhost:7279")
                            .WithHeaders(HeaderNames.ContentType, HeaderNames.Authorization)
                            .AllowAnyMethod();
                        });
    });

    builder.Services.AddControllers();

    if (builder.Environment.IsDevelopment())
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
    }
}

var app = builder.Build();
{
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseRouting(); // new

    app.UseCors("CorsPolicy");

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
