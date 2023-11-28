using MediatR;
using Module.PMV.Core.Assets.Features.Commands.Assets;
using Module.PMV.Core.Assets.Features.DTOs.Assets.Response;
using Module.PMV.Core.Assets.Features.Queries.Assets;
using WebApp.Client.Pages.PMV.Assets.Models;
using WebApp.Service.Http;

namespace WebApp.Client.Pages.PMV.Assets.Data;

public class AssetDataService : IAssetService
{
    private readonly IMediator _mediator;
    private readonly HttpService _httpService;

    public AssetDataService(IMediator mediator,HttpService httpService)
    {
        _mediator = mediator;
        _httpService = httpService;
    }

   

    public Task<IEnumerable<ServiceDueEntryModel>> CreateServiceDue(int assetId, string groupAlertId)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAssignedDriver(int assignedId)
    {
        throw new NotImplementedException();
    }

    public Task DeleteDocument(string documentId)
    {
        throw new NotImplementedException();
    }

    public Task DeleteServiceDue(string serviceId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<OperatorDriverModel>> GetAllAssignedDrivers(string assetCode)
    {
        throw new NotImplementedException();
    }

    public async Task<AssetContainerModel> CreateNew(string assetType, bool isPostBack = false)
    {
        var result = await _mediator.Send(new CreateNewAsset.Query(assetType));
        if (!result.IsSuccess) {
            throw new Exception(result.Errors[0].Message);
        }

        return new AssetContainerModel
        {
            Accounts = result.Value.Accounts?.ToList() ?? new(),
            Brands = result.Value.Brands?.ToList() ?? new(),
            AssetTypes = result.Value.AssetTypes?.ToList() ?? new(),
            Categories = result.Value.Categories?.ToList() ?? new(),
            Companies = result.Value.Companies?.ToList() ?? new(),
            HireMethods = result.Value.HireMethods?.ToList() ?? new(),
            RentOwnes = result.Value.RentOwnes?.ToList() ?? new(),
            PlateTypes = result.Value.PlateTypes?.ToList() ?? new(),
            Statuses = result.Value.Statuses?.ToList() ?? new(),
            Vendors = result.Value.Vendors?.ToList() ?? new(),
            ServiceGroups = result.Value.ServiceGroups?.ToList() ?? new(),
        };
    }

    public Task<AssetContainerModel?> GetAsset(string searchValue, string searchType, string assetType, bool IsPostBack)
    {
        throw new NotImplementedException();
    }

    public Task<AssignedAssetModel> GetAssetDetailExpress(string assetCode)
    {
        throw new NotImplementedException();
    }

    public async Task<string> GetAssetNo(string subCategory)
    {
        var result = await _mediator.Send(new GetNewAssetNo.Query(subCategory));
        if (!result.IsSuccess)
        {
            throw new Exception(result.Errors[0].Message);
        }

        return result.Value;
    }

    public async Task<AssetContainerResponse> GetAssets(FilterAssetModel filterAssetParam) {

        var results = await _mediator.Send(new FilterAssets.Query(filterAssetParam.ToRequest(), 
            filterAssetParam.AssetType,filterAssetParam.IsPostBack,filterAssetParam.IsRefresh));
        
        return results.Value;
    }

    public Task<AssetDashboardContainer> GetCategoryCount()
    {
        throw new NotImplementedException();
    }

    public Task<CostReportModel> GetMaintenanceCost(MCRequest request)
    {
        throw new NotImplementedException();
    }

    public async Task SaveInternal(InternalAssetModel model,string userId)
    {
        var result = await _mediator.Send(new AssetCreate.Command(model.ToRequest(), null, userId));
        if (!result.IsSuccess)
        {
            throw new Exception(result.Errors[0].Message);
        }
    }

    public async Task SaveExternal(ExternalAssetModel model, string userId)
    {
        var result = await _mediator.Send(new AssetCreate.Command(null, model.ToRequest(), userId));
        if (!result.IsSuccess)
        {
            throw new Exception(result.Errors[0].Message);
        }
    }

    public Task UpdateAssignedDriver(OperatorDriverModel assignedDriver, string assetType)
    {
        throw new NotImplementedException();
    }

    public Task UpdateServiceDue(int assetId, ServiceDueEntryModel serviceEntry)
    {
        throw new NotImplementedException();
    }

    public Task<AssetDocumentModel?> UploadDocument(AssetDocumentModel document)
    {
        throw new NotImplementedException();
    }

    public Task<AssetReadModel?> ViewAsset(int id)
    {
        throw new NotImplementedException();
    }
}
