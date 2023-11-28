using Module.PMV.Core.Assets.Contracts.Assets;
using Module.PMV.Core.Assets.Contracts.Commons;
using Module.PMV.Core.Assets.Features.DTOs.Assets.Request;
using Module.PMV.Core.Assets.Models.Assets;
using WebApp.SharedServer.Notifications;

namespace Module.PMV.Core.Assets.Features.Commands.Assets;

public static class AssetUpdate
{
    public record Command(AssetContainerRequest Request, string Key, string AssetType, string UserId) : IRequest<Result<Unit>>;

    public class CommandHandler : IRequestHandler<Command, Result<Unit>>
    {
        private readonly IAssetDataService _dataService;
        private readonly ICommonDataService _commonService;
        private readonly IAppNotificationPublisher _notification;

        public CommandHandler(
            IAssetDataService dataService,
            ICommonDataService commonService,
            IAppNotificationPublisher notification
            )
        {
            _dataService = dataService;
            _commonService = commonService;
            _notification = notification;
        }
        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.AssetType.ToLower() == "external")
                {

                    var externalData = await _dataService.GetExternalAsset(request.Key);
                    var externalRequest = request.Request.External;

                    //transfer data from dto to entity
                    externalData.Update(
                        externalRequest!.AssetDesc,
                        externalRequest.PlateType,
                        externalRequest.PlateNum,
                        externalRequest.VendorCode,
                        externalRequest.CompanyCode,
                        externalRequest.HireSub,
                        externalRequest.FuelTankCapacity,
                        request.UserId);

                    await _dataService.CreateUpdateExternalAssets(externalData);
                }
                else
                {
                    var internalData = await _dataService.GetInternal(int.Parse(request.Key));
                    await UpdateAsset(internalData, request.Request.Internal!, request.UserId);
                    await _dataService.CreateUpdateInternal(internalData);
                }

                return Result.Ok(Unit.Value);
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }

        private async Task UpdateAsset(InternalAsset internalAsset, InternalAssetRequest request, string userId)
        {

            var selections = await _commonService.GetSelections("statuses");

            internalAsset.Update(
                request.Cid,
                request.SubCatCode,
                request.AssetDesc ?? "",
                request.BrandCode ?? "",
                request.Model ?? "",
                request.Year,
                request.Color ?? "",
                request.PlateNo ?? "",
                request.EngineNo ?? "",
                request.ChasisNo ?? "",
                request.FirstRegDate,
                request.PurchaseDate,
                request.NetValue,
                request.VendorCode ?? "",
                request.CompanyCode ?? "",
                request.ManagedBy ?? "",
                request.LPONo ?? "",
                request.KmPerHr,
                request.ConditionRank,
                request.Status,
                request.RentOrOwned,
                request.RateType ?? "",
                request.Rate,
                request.ModifiedAsset,
                request.Remarks ?? "",
                request.AccountCategory,
                request.RateOfDepreciation,
                request.ItemCode,
                request.PurchaseAmount,
                request.DateOfSelling,
                request.SoldAmount,
                request.ParkingArea,
                userId);

            internalAsset.InsertAdditional(request.Additional.Owner,
                request.Additional.OperatedBy,
                request.Additional.HireMethod,
                request.Additional.Dimension,
                request.Additional.Warranty,
                request.Additional.RegistrationExpiry,
                request.Additional.Insurance,
                request.Additional.WarrantyPath,
                request.Additional.RegPath,
                request.Additional.InsurancePath,
                request.Additional.WarrantyName,
                request.Additional.RegName,
                request.Additional.InsuranceName,
                request.Additional.AssetType);

            var assetStatus = selections.FirstOrDefault(s => s.Value == request.Status.ToString());

            internalAsset.DeliveryNote = string.IsNullOrEmpty(request.DeliveryNote) ? "N/A" : request.DeliveryNote;
            internalAsset.TankCapacity = request.TankCapacity;

            if (assetStatus is not null)
            {
                internalAsset.StatusDescription = assetStatus.Text;
            }
        }


    }
}

