namespace DemoSchool.WebUI.Common.Handler;

public class CustomHttpMessageHandler : DelegatingHandler
{
    private readonly NavigationManager _navManager;

    public CustomHttpMessageHandler(NavigationManager navManager)
    {
        _navManager = navManager;
    }

    protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var response = await base.SendAsync(request, cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            var statusCode = response.StatusCode;
            switch (statusCode)
            {
                case HttpStatusCode.NotFound:
                    _navManager.NavigateTo("/404");
                    break;
                case HttpStatusCode.Unauthorized:
                    _navManager.NavigateTo("/unauthorized");
                    break;
                case HttpStatusCode.Forbidden:
                    _navManager.NavigateTo("/forbidden");
                    break;
                default:
                    _navManager.NavigateTo("/500");
                    break;
            }
        }

        return response;
    }
}
