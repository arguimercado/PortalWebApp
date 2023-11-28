using Module.PMV.Core.Assets.Contracts.Assets;
using Module.PMV.Core.Assets.Contracts.Commons;
using Module.PMV.Core.Assets.Features.DTOs.Assets.Response;

namespace Module.PMV.Core.Assets.Features.Queries.Assets;

public static class EditAsset
{
    public record Query(string SearchValue,string SearchType,string AssetType,bool IsPostBack = false) : IRequest<Result<AssetContainerResponse>>;

    public class QueryHandler : IRequestHandler<Query, Result<AssetContainerResponse>>
    {
        private readonly IAssetDataService _assetDataService;
        private readonly ICommonDataService _commonDataService;

        public QueryHandler(IAssetDataService assetDataService,ICommonDataService commonDataService)
        {
            _assetDataService = assetDataService;
            _commonDataService = commonDataService;
        }


        public async Task<Result<AssetContainerResponse>> Handle(Query request, CancellationToken cancellationToken)
        {
            try
            {
                var response = new AssetContainerResponse();

                if (request.AssetType.ToLower() == "internal")
                {
                    response.Companies = (await _commonDataService.GetSelections("companies")).ToList();
                    response.Categories = (await _commonDataService.GetSelections("categories")).ToList();
                    response.Companies = (await _commonDataService.GetSelections("companies")).ToList();
                    response.Brands = (await _commonDataService.GetSelections("brands")).ToList();
                    response.RentOwnes = (await _commonDataService.GetSelections("rentownes")).ToList();
                    response.Statuses = (await _commonDataService.GetSelections("statuses")).ToList();
                    response.HireMethods = await _commonDataService.GetSelections("hiremethods");
                    response.AssetTypes = await _commonDataService.GetSelections("assetTypes");
                    response.Vendors = (await _commonDataService.GetSelections("vendorlist")).ToList();
                    response.Accounts = await _commonDataService.GetSelections("accounts");
                }
                else
                {
                    response.PlateTypes = (await _commonDataService.GetSelections("plateTypes")).ToList();
                    response.Companies = (await _commonDataService.GetSelections("companies")).ToList();
                    response.Vendors = (await _commonDataService.GetSelections("vendorlist")).ToList();
                    response.HireMethods = await _commonDataService.GetSelections("hire");
                }

                if (request.AssetType.ToLower() == "internal") {

                    var internalAsset = await _assetDataService.GetInternal(request.SearchValue, request.SearchType);

                }
            }
            catch(Exception ex)
            {

            }
        }
    }
}
