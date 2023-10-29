using Blazored.LocalStorage;
using System.Text;
using System.Text.Json;

namespace WebApp.Service.Sessions;

public class LocalStorage : IStorage
{
    private readonly ILocalStorageService _localStorage;

    public LocalStorage(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
    }
    public async Task<TResponse?> ReadAsync<TResponse>(string key)
    {
        if (await _localStorage.ContainKeyAsync(key))
        {
            var base64Json = await _localStorage.GetItemAsync<string>(key);
            var itemJsonBytes = Convert.FromBase64String(base64Json);
            var itemJson = Encoding.UTF8.GetString(itemJsonBytes);
            return JsonSerializer.Deserialize<TResponse>(itemJson);
        }

        return default;
    }

    public async Task RemoveAsync(string key)
    {
        await _localStorage.RemoveItemAsync(key);
    }

    public async Task SaveAsync<TRequest>(string key, TRequest item)
    {
        //clear the key if it exists
        await RemoveAsync(key);

        var itemJson = JsonSerializer.Serialize(item);
        var itemJsonBytes = Encoding.UTF8.GetBytes(itemJson);
        var base64Json = Convert.ToBase64String(itemJsonBytes);

        await _localStorage.SetItemAsync(key, base64Json);
    }
}
