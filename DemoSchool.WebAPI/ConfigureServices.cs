namespace DemoSchool.WebAPI;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddScoped<IApplicationConnection>(s => new ApplicationConnection(null, configuration.GetConnectionString("DefaultConnection")));
        services.AddScoped<IStudentRepository, StudentRepository>();
        services.AddScoped<IStudentAddressRepository, StudentAddressRepository>();

        return services;
    }
}
