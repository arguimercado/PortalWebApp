using Microsoft.AspNetCore.Components.Authorization;
using Portal.WebClient.Pages.Accounts.ViewModels;
using Portal.WebClient.Pages.Fuels.Finance.Data;
using Portal.WebClient.Pages.Fuels.Finance.ViewModels;
using Portal.WebClient.Pages.Fuels.FuelEffeciency.Data;
using Portal.WebClient.Pages.Fuels.FuelEffeciency.ViewModels;
using Portal.WebClient.Pages.Fuels.FuelTracking.Data;
using Portal.WebClient.Pages.Fuels.FuelTracking.ViewModels;
using Radzen;
using WebApp.Client.Pages.Authentication.Data;
using WebApp.Client.Pages.PMV.Assets.Data;
using WebApp.Client.Pages.PMV.Assets.ViewModels;
using WebApp.Client.Pages.PMV.Fuels.Dashboard.Data;
using WebApp.Client.Pages.PMV.Fuels.Dashboard.ViewModels;
using WebApp.Client.Pages.PMV.Fuels.FuelEntry.Data;
using WebApp.Client.Pages.PMV.Fuels.FuelEntry.ViewModels;
using WebApp.Client.Pages.PMV.Fuels.FuelManage.Data;
using WebApp.Client.Pages.PMV.Fuels.FuelManage.ViewModels;
using WebApp.Client.Pages.PMV.Fuels.Stations.Data;
using WebApp.Client.Pages.PMV.Fuels.Stations.ViewModels;
using WebApp.Client.Pages.PMV.PMS.GroupAlerts.Components.Entry.ViewModels;
using WebApp.Client.Pages.PMV.PMS.GroupAlerts.Components.Manage.ViewModels;
using WebApp.Client.Pages.PMV.PMS.GroupAlerts.Data;
using WebApp.Client.Providers;
using WebApp.UILibrary;
using WebApp.UILibrary.Components.Common.Spinners;
using WebApp.UILibrary.Components.Navigation;
using WebApp.UILibrary.Providers;

namespace WebApp.Client.Extensions
{
    public static class AppExtension
    {
        public static IServiceCollection AddAppExtension(this IServiceCollection services)
        {
            services.AddScoped<DialogService>();
            services.AddScoped<NotificationService>();
            services.AddScoped<TooltipService>();
            services.AddScoped<PageNavigation>();
            services.AddScoped<CustomSpinnerViewModel>();


            services.AddScoped<FuelLogEntryViewModel>();
            services.AddScoped<FuelLogListViewModel>();
            services.AddScoped<FuelDashboardViewModel>();
            services.AddScoped<StationPageViewModel>();
            services.AddScoped<FuelTrackingViewModel>();
            services.AddScoped<FuelLedgerViewModel>();
            services.AddScoped<FuelLogEfficiencyViewModel>();


            services.AddScoped<AssetViewModel>();
            services.AddScoped<AssetListViewModel>();
            services.AddScoped<AssetDocumentViewModel>();
            services.AddScoped<AssetServiceViewModel>();
            services.AddScoped<AssetReadViewModel>();
            services.AddScoped<AssetExportViewModel>();
            services.AddScoped<AssetDashboardViewModel>();
            

            services.AddScoped<CostReportViewModel>();
            services.AddScoped<AssignedDriverViewModel>();



            services.AddScoped<GroupAlertListViewModel>();
            services.AddScoped<GroupAlertViewModel>();
            services.AddScoped<ServiceAlertViewModel>();
            
            services.AddScoped<LoginViewModel>();




            return services;

        }

        public static IServiceCollection AddProviderExtension(this IServiceCollection services)
        {
            
            services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
            services.AddScoped<JSInterOpProvider>();
            return services;
        }

        public static IServiceCollection AddServiceExtension(this IServiceCollection services)
        {
            services.AddScoped<IGroupAlertService,GroupAlertService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();

            services.AddScoped<IFuelTrackingService, FuelTrackingService>();
            services.AddScoped<IFuelEntryService, FuelEntryService>();
            services.AddScoped<IStationService, StationService>();
            services.AddScoped<ILogListService, LogListService>();
            services.AddScoped<IEffeciencyService, EffeciencyService>();
            services.AddScoped<IFuelLedgerService, FuelLedgerService>();
            services.AddScoped<IFuelLogDashboardService,FuelLogDashboardService>();
            services.AddScoped<IAssetService, AssetService>();


            return services;
        }
    }
}
