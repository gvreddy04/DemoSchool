using System.Reflection;

namespace DemoSchool.WebUI;

public static class ConfigureServices
{
    public static IServiceCollection AddWebUIServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddHttpClient<StudentApiClient>(client => client.BaseAddress = new("https://demoschool.webapi"));

        return services;
    }
}
