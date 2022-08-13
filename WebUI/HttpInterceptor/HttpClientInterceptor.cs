namespace WebUI.HttpInterceptor;

public class HttpClientInterceptor : IHttpClientInterceptor
{
    public event EventHandler<HttpClientInterceptorEventArgs> BeforeSend;
    public event HttpClientInterceptorEventHandler BeforeSendAsync;
    public event EventHandler<HttpClientInterceptorEventArgs> AfterSend;
    public event HttpClientInterceptorEventHandler AfterSendAsync;

    internal HttpClientInterceptor() { }

    internal async Task InvokeBeforeSendAsync(HttpClientInterceptorEventArgs args)
    {
        this.BeforeSend?.Invoke(this, args);
        await InvokeAsync(this.BeforeSendAsync, args);
    }

    internal async Task InvokeAfterSendAsync(HttpClientInterceptorEventArgs args)
    {
        this.AfterSend?.Invoke(this, args);
        await InvokeAsync(this.AfterSendAsync, args);
    }

    private async Task InvokeAsync(HttpClientInterceptorEventHandler asyncEventHandler, HttpClientInterceptorEventArgs args)
    {
        if (asyncEventHandler == null) return;

        var asyncHandlerTasks = asyncEventHandler.GetInvocationList()
            .Cast<HttpClientInterceptorEventHandler>()
            .Select(handler => handler.Invoke(this, args))
            .ToArray();

        await Task.WhenAll(asyncHandlerTasks);
    }
}
