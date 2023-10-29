using Microsoft.JSInterop;

namespace WebApp.UILibrary.Providers;

public class JSInterOpProvider : IAsyncDisposable
{
    private readonly Lazy<Task<IJSObjectReference>> moduleTask;
    public JSInterOpProvider(IJSRuntime jsRuntime) {
        moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
           "import", "./_content/WebApp.UILibrary/jsInterop.js").AsTask());
    }

    public async ValueTask<string> Show(string args)
    {
        var module = await moduleTask.Value;
        return await module.InvokeAsync<string>("ShowBlank", args); //new object[] { args,"_blank" }
    }

    public async ValueTask DisposeAsync()
    {
        if (moduleTask.IsValueCreated)
        {
            var module = await moduleTask.Value;
            await module.DisposeAsync();
        }
    }
}
