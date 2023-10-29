using WebApp.Client.Pages.PMV.PMS.GroupAlerts.Models;
using WebApp.Service.Http;

namespace WebApp.Client.Pages.PMV.PMS.GroupAlerts.Data;

public interface IGroupAlertService
{
    Task<GroupAlertContainerModel> New(bool isPostBack = false);
    Task<GroupAlertContainerModel> Edit(string id, bool isPostBack = false);
    Task<GroupAlertContainerModel> GetAll();
    Task CreateUpdate(GroupAlertModel model);
    Task<GroupAlertModel?> GetInstanceAlert(string alertId);

    Task<IEnumerable<ServiceAlertModel>?> GetServiceAlerts();

    Task CreateServiceAlert(ServiceAlertModel alert);

    Task ApplyToAsset(string groupAlertId, string subCategory, int type = 0);
}

public class GroupAlertService : IGroupAlertService
{

    private readonly HttpService _httpService;

    public GroupAlertService(HttpService httpService)
    {
        _httpService = httpService;
    }

    public async Task<GroupAlertContainerModel> New(bool isPostBack = false) =>
        await _httpService.GetAsync<GroupAlertContainerModel>($"pmv/groupalert/new?isPostBack={isPostBack}") ?? new();

    public async Task<GroupAlertContainerModel> Edit(string id, bool isPostBack = false) =>
        await _httpService.GetAsync<GroupAlertContainerModel>($"pmv/groupalert/edit?id={id}&isPostBack={isPostBack}") ?? new();

    public async Task<GroupAlertContainerModel> GetAll() =>
        await _httpService.GetAsync<GroupAlertContainerModel>($"pmv/groupalert") ?? new();

    public async Task CreateUpdate(GroupAlertModel model) =>
        await _httpService.PostAsync("pmv/groupalert", model);

    public async Task<GroupAlertModel?> GetInstanceAlert(string alertId) =>
        await _httpService.GetAsync<GroupAlertModel>($"pmv/groupalert/instance/{alertId}");

    public async Task<IEnumerable<ServiceAlertModel>?> GetServiceAlerts()
    {
        var response = await _httpService.GetAsync<IEnumerable<ServiceAlertModel>>("pmv/serviceAlert");
        return response;
    }

    public async Task CreateServiceAlert(ServiceAlertModel alert) =>
        await _httpService.PostAsync("pmv/serviceAlert", alert);

    public async Task ApplyToAsset(string groupAlertId, string subCategory, int type = 0)
        => await _httpService.PostAsync("pmv/GroupAlert/assetUpdate", new { GroupAlertId = groupAlertId, SubCategory = subCategory, Type = type });
}
