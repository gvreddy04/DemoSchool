namespace WebUI.Services;

public class BaseService
{
    private readonly IConfiguration _configuration;
    private readonly ILogger _logger;

    protected string BaseUrl { get; init; }

    public BaseService(IConfiguration configuration, ILogger logger)
    {
        this._configuration = configuration;
        this._logger = logger;

        this.BaseUrl = $"{_configuration.GetValue<string>(Constants.ApiBaseUrl)}";
    }
}
