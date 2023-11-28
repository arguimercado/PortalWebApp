using Applications.UseCases.PMV.GroupAlerts.Interfaces;
using Asset.Core.Contracts.Assets;
using Asset.Core.Contracts.Commons;
using Asset.Core.Infrastructures.Context;
using Asset.Core.Infrastructures.Services.Assets;
using Asset.Core.Infrastructures.Services.Commons;
using Asset.Core.Infrastructures.Services.GroupAlerts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using WebApp.SharedServer.Notifications;
using WebApp.SharedServer.Utilities.Uploads;

namespace Asset.Core;

public static class DependencyInjection
{
    public static IServiceCollection AddAssetCore(this IServiceCollection services,IConfiguration configuration)
    {
        
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddDbContext<AssetDataContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("HelmDb")));
        services.AddSingleton<ISqlQuery, SqlQuery>(opt => new SqlQuery(configuration.GetConnectionString("PortalDb") ?? ""));
        services.AddScoped<IAppNotificationPublisher, AppNotificationPublisher>();

        services.AddScoped<DocFileManager>(opt => new DocFileManager(new FileOption
        {
            Directory = configuration["DocumentFile:Directory"]!.ToString(),
            Document = configuration["DocumentFile:Document"]!.ToString()
        }));
        

        return services;
    }

    public static IServiceCollection AddAssetService(this IServiceCollection services)
    {
        services.AddScoped<IAssetDataService, AssetDataService>();
        services.AddScoped<IDocumentDataService, DocumentDataService>();
        services.AddScoped<IOperatorDataService, OperatorDataService>();
        services.AddScoped<IServiceAlertDataService, ServiceAlertDataService>();
        services.AddScoped<ICommonDataService, CommonDataService>();
        services.AddScoped<IGroupAlertRepository, GroupAlertRepository>();
        return services;
    }

}
