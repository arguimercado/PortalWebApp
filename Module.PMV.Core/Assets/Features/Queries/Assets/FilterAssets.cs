using Module.PMV.Core.Assets.Contracts.Assets;
using Module.PMV.Core.Assets.Contracts.Commons;
using Module.PMV.Core.Assets.Features.DTOs.Assets.Request;
using Module.PMV.Core.Assets.Features.DTOs.Assets.Response;

namespace Module.PMV.Core.Assets.Features.Queries.Assets;

public static class FilterAssets
{
    public record Query(FilterAssetRequest Request, string AssetType, bool IsPostBack, bool IsRefresh) : IRequest<Result<AssetContainerResponse>>;

    public class QueryHandler : IRequestHandler<Query, Result<AssetContainerResponse>>
    {
        private readonly IAssetDataService _assetDataService;
        private readonly ICommonDataService _commonService;

        public QueryHandler(IAssetDataService assetDataService, ICommonDataService commonService)
        {
            _assetDataService = assetDataService;
            _commonService = commonService;
        }

        public async Task<Result<AssetContainerResponse>> Handle(Query request, CancellationToken cancellationToken)
        {

            try
            {
                var assetContainer = new AssetContainerResponse();
                if (request.IsPostBack)
                {

                    if (request.AssetType == "internal")
                    {
                        var data = await _assetDataService.GetInternals(
                            request.Request.AssetCode,
                            request.Request.Category,
                            request.Request.SubCategory,
                            request.Request.Brand,
                            request.Request.CompanyCode,
                            string.IsNullOrEmpty(request.Request.Status) ? null : request.Request.Status);

                        if (data is null) throw new Exception("No asset record found");

                        assetContainer.InternalAssets = data.Select(d => new InternalAssetResponse
                        {
                            AssetCode = d.AssetCode ?? "",
                            AssetDesc = d.AssetDesc ?? "",
                            BrandCode = d.BrandCode,
                            CategoryName = d.Category.CatName ?? "",
                            CompanyCode = d.CompanyCode ?? "",
                            PlateNo = d.PlateNo!,
                            VendorCode = d.VendorCode,
                            Color = d.Color!,
                            Model = d.Model ?? "",
                            SubCatCode = d.SubCatCode ?? "",
                            Year = d.Year,
                            PurchaseDate = d.PurchaseDate,
                            SlNo = d.SlNo,
                            StatusDesc = d.GetStatusDesc(),
                            LPONo = d.LPONo,
                            DeliveryNote = d.DeliveryNote,
                            KmHr = d.KmPerHr,
                            TankCapacity = d.TankCapacity,
                            Drivers = d.Drivers.Select(d => new OperatorDriverResponse
                            {
                                EmpCode = d.EmpCode,
                                Name = d.EmpName,
                                AssignedAt = d.AssignedAt
                            }),
                        });
                    }
                    else
                    {
                        var data = await _assetDataService.GetExternalAssets(request.Request.PlateType,
                           request.Request.AssetCode, request.Request.CompanyCode,
                           request.Request.VendorCode, request.Request.HireOrSubContract);

                        assetContainer.ExternalAssets = data.Select(p => new ExternalAssetResponse
                        {
                            AssetCode = p.AssetCode,
                            CreatedAt = p.CreatedAt,
                            Description = p.AssetDesc,
                            PlateNum = p.PlateNum,
                            PlateType = p.PlateType,
                            HireOrSubContract = p.HireSub,
                            Vendor = p.VendorCode,
                            HireUnder = p.CompanyCode,
                            FuelTankCapacity = p.FuelTankCapacity,
                        });
                    }
                }

                if (!request.IsPostBack || request.IsRefresh)
                {

                    assetContainer.Brands = (await _commonService.GetSelections("brands")).ToList();
                    assetContainer.Companies = (await _commonService.GetSelections("companies")).ToList();
                    assetContainer.Categories = (await _commonService.GetSelections("categories")).ToList();

                    assetContainer.SubCategories = (await _commonService.GetSelections("subcategories")).ToList();

                    assetContainer.Statuses = (await _commonService.GetSelections("statuses")).ToList();

                    assetContainer.Vendors = (await _commonService.GetSelections("vendors")).ToList();
                    assetContainer.PlateTypes = (await _commonService.GetSelections("plateTypes")).ToList();
                    assetContainer.HireSub = (await _commonService.GetSelections("hire")).ToList();
                }

                return Result.Ok(assetContainer);
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }
    }
}



