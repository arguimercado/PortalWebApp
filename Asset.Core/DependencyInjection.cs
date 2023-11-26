using Asset.Core.Contracts.Assets;
using Asset.Core.Infrastructures.Context;
using Asset.Core.Infrastructures.Services.Assets;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Asset.Core;

public static class DependencyInjection
{
    public static IServiceCollection AddAssetCore(this IServiceCollection services,IConfiguration configuration)
    {
        
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddDbContext<AssetDataContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("DbHelm")));
        
        services.AddScoped<IAssetDataService, AssetDataService>();
        services.AddScoped<IDocumentDataService, DocumentDataService>();
        services.AddScoped<IOperatorDataService, OperatorDataService>();

        return services;
    }

}
