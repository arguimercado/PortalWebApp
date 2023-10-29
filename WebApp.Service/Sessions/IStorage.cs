namespace WebApp.Service.Sessions;

public interface IStorage
{
    Task SaveAsync<TRequest>(string key, TRequest item);
    Task<TResponse?> ReadAsync<TResponse>(string key);

    Task RemoveAsync(string key);


}
