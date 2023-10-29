using Blazored.LocalStorage;
using Blazored.SessionStorage;
using Microsoft.Extensions.DependencyInjection;
using WebApp.Service.Http;
using WebApp.Service.Sessions;

namespace WebApp.Service;

public static class DependencyInjection
{
    public static IServiceCollection AddService(this IServiceCollection services, string baseAddress)
    {    
        services.AddScoped(
            sp => new HttpClient
            {   
                BaseAddress = new Uri(baseAddress)
            });
        services.AddBlazoredSessionStorage();
        services.AddScoped<HttpService>();
        services.AddScoped<IStorage, SessionStorage>();
        return services;
    }

}
