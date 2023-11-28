using Module.PMV.Core.Assets.Contracts.Assets;
using Module.PMV.Core.Assets.Features.DTOs.Assets.Request;
using WebApp.SharedServer.Errors;

namespace Module.PMV.Core.Assets.Features.Commands.Assets;

public static class UpdateOperator
{
    public record Command(OperatorDriverRequest Request, int Id) : IRequest<Result<Unit>>;
    
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
                var operatorDriver = await _dataService.FindOperatorAsync(request.Id);
                if (operatorDriver is null)
                {
                    throw new NotFoundException("Operator or driver");
                }
                string vendorCode = "";
                string brandCode = "";
                if (request.Request.InternalExternal == 1)
                {
                    var asset = await _assetDataService.GetInternalByAssetCode(request.Request.AssetCode);
                    vendorCode = asset.VendorCode;
                    brandCode = asset.BrandCode;

                }
                else
                {
                    var asset = await _assetDataService.GetExternalAsset(request.Request.AssetCode);
                    vendorCode = asset.VendorCode;
                    brandCode = "";
                }

                operatorDriver.Update(request.Request.AssetCode,
                        request.Request.AssetTypeCode,
                        request.Request.Division,
                        brandCode,
                        request.Request.EmpCode,
                        request.Request.EmpType,
                        request.Request.Name ?? "",
                        request.Request.RPNo,
                        request.Request.Company,
                        request.Request.MobileNo,
                        request.Request.Department,
                        request.Request.AssetLocation,
                        vendorCode,
                        request.Request.InternalExternal,
                        request.Request.AssignedAt,
                        request.Request.ReturnedAt,
                        request.Request.DcsSlNo);

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
