using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Data.SqlClient;
using System.Data;
using WebAPI.Repository.Interfaces;

namespace WebAPI.Repository;

public class ApplicationConnection : IApplicationConnection
{
    private readonly ILogger<ApplicationConnection> _logger;
    private readonly string _connectionString;

    public ApplicationConnection(ILogger<ApplicationConnection> logger, string connectionString)
    {
        this._logger = logger;
        this._connectionString = connectionString;
    }

    public IDbConnection GetConnection()
    {
        try
        {
            // EnableRetryLogic
            AppContext.SetSwitch("Switch.Microsoft.Data.SqlClient.EnableRetryLogic", true);
            SqlRetryLogicOption retryLogicOption = new SqlRetryLogicOption()
            {
                NumberOfTries = 6,
                DeltaTime = TimeSpan.FromSeconds(15),
                MaxTimeInterval = TimeSpan.FromMinutes(2),
                TransientErrors = new[] { 10928, 10929, 10053, 10054, 10060, 40197, 40540, 40613, 40615, 40143, 233, 64, 53, -2, 11001 }
            };

            SqlRetryLogicBaseProvider retryLogicBaseProvider = SqlConfigurableRetryFactory.CreateFixedRetryProvider(retryLogicOption);
            var connection = new SqlConnection(_connectionString)
            {
                RetryLogicProvider = retryLogicBaseProvider
            };
            connection.RetryLogicProvider.Retrying += (object sender, SqlRetryingEventArgs args) =>
            {
                if (args != null && args.Exceptions != null && args.Exceptions.Count > 0)
                {
                    _logger?.LogError(args.Exceptions[0], $"ErrorMessage={args.Exceptions[0].Message}, Source=ApplicationConnection.GetConnection(), Retry Count: {args.RetryCount}, Delay: {args.Delay}");
                }
                else
                {
                    _logger?.LogInformation($"Source=ApplicationConnection.GetConnection(), Retry Count: {args.RetryCount}, Delay: {args.Delay}");
                }
            };
            // NOTE: below two lines not required if we use sql authentication
            //var azureServiceTokenProvider = new AzureServiceTokenProvider();
            //connection.AccessToken = azureServiceTokenProvider.GetAccessTokenAsync("https://database.windows.net/").Result;
            return connection;
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, $"ErrorMessage={ex.Message}, Source=ApplicationConnection.GetConnection()");
            throw;
        }
    }
}
