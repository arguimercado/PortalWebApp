using Module.PMV.Core.Assets.Contracts.Assets;
using Module.PMV.Core.Assets.Features.DTOs.Assets.Request;
using Module.PMV.Core.Assets.Models.Assets.Entities;

namespace Module.PMV.Core.Assets.Features.Commands.Assets;

public static class CreateOperator
{
    public record Command(OperatorDriverRequest Request) : IRequest<Result<Unit>>;
    

    public class CommandHandler : IRequestHandler<Command, Result<Unit>>
    {
        private readonly IOperatorDataService _dataService;
        private readonly IAssetDataService _assetDataService;

        public CommandHandler(IOperatorDataService dataService, IAssetDataService assetDataService)
        {
            _dataService = dataService;
            _assetDataService = assetDataService;
        }

        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            try
            {
                var assetTypeCode = "";
                var vendorCode = "";
                var brandCode = "";
                if (request.Request.InternalExternal == 1)
                {
                    var asset = await _assetDataService.GetInternal(request.Request.AssetCode);
                    assetTypeCode = asset.SubCatCode;
                    vendorCode = asset.VendorCode;
                    brandCode = asset.BrandCode;

                }
                else
                {
                    var asset = await _assetDataService.GetExternalAsset(request.Request.AssetCode);
                    assetTypeCode = asset.PlateType;
                    vendorCode = asset.VendorCode;
                    brandCode = "N/A";

                }

                var operatorDriver = OperatorDriver.Create(request.Request.AssetCode, assetTypeCode,
                    request.Request.Division, brandCode, request.Request.EmpCode, request.Request.EmpType, request.Request.Name ?? "", request.Request.RPNo,
                    request.Request.Company, request.Request.MobileNo, request.Request.AssetLocation,
                    request.Request.VendorCode, request.Request.InternalExternal,
                    request.Request.AssignedAt, request.Request.ReturnedAt,
                    request.Request.DcsSlNo,
                    request.Request.CreatedBy);

                await _dataService.SaveOperator(operatorDriver);
                return Result.Ok(Unit.Value);

            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }
    }
}



