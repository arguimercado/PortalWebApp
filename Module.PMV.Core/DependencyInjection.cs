using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Module.PMV.Core.Assets.Contracts.Assets;
using Module.PMV.Core.Assets.Contracts.Commons;
using Module.PMV.Core.Assets.Contracts.GroupAlerts;
using Module.PMV.Core.Assets.Infrastructures.Context;
using Module.PMV.Core.Assets.Infrastructures.Services.Assets;
using Module.PMV.Core.Assets.Infrastructures.Services.Commons;
using Module.PMV.Core.Assets.Infrastructures.Services.GroupAlerts;
using System.Reflection;

namespace Module.PMV.Core;

public static class DependencyInjection
{
    public static IServiceCollection AddPMVCore(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddDbContext<AssetDataContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("HelmDb")));
        services.AddSingleton<ISqlQuery, SqlQuery>(opt => new SqlQuery(configuration.GetConnectionString("PortalDb") ?? ""));
        services.AddScoped<IAppNotificationPublisher, AppNotificationPublisher>();

        services.AddScoped(opt => new DocFileManager(new FileOption
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
