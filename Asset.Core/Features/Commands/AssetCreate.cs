using Asset.Core.Contracts.Assets;
using Asset.Core.Contracts.Commons;
using Asset.Core.DTOs.Assets;
using Asset.Core.Models.Assets;


namespace Asset.Core.Features.Commands;

public static class AssetCreate {
    public record Command(AssetContainerRequest Request,string AssetType,string UserId) : IRequest<Result<Unit>>;

    public class CommandHandler : IRequestHandler<Command, Result<Unit>>
    {
        private readonly IAssetDataService _dataService;
        private readonly ICommonDataService _commonService;

        public CommandHandler(IAssetDataService dataService,ICommonDataService commonService)
        {
            _dataService = dataService;
            _commonService = commonService;
        }
        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.AssetType.ToLower() == "external")
                {
                    var externalData = request.Request.External;
                    //transfer data from dto to entity
                    var external = new ExternalAsset(
                        externalData.AssetCode,
                        externalData.AssetDesc,
                        externalData.PlateType,
                        externalData.PlateNum,
                        externalData.VendorCode,
                        externalData.CompanyCode,
                        externalData.HireSub,
                        request.UserId);

                    external.FuelTankCapacity = externalData.FuelTankCapacity;

                    await _dataService.CreateUpdateExternalAssets(external);
                }
                else
                {
                    var asset = await GetInstance(request.Request.Internal!,request.UserId);
                    await _dataService.CreateUpdateInternal(asset);
                }

                return Result.Ok(Unit.Value);
            }
            catch(Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }

        private async Task<InternalAsset> GetInstance(InternalAssetRequest asset,string userId)
        {
            //generate asset no
            var assetNo = await _dataService.CreateInternalAssetNo(asset.SubCatCode);
            
            var selections = await _commonService.GetSelections("statuses");

            var returnAsset = InternalAsset.Create(
                asset.Cid,
                asset.SubCatCode,
                assetNo,
                asset.AssetDesc ?? "",
                asset.BrandCode ?? "",
                asset.Model ?? "",
                asset.Year,
                asset.Color ?? "",
                asset.PlateNo ?? "",
                asset.EngineNo ?? "",
                asset.ChasisNo ?? "",
                asset.FirstRegDate,
                asset.PurchaseDate,
                asset.NetValue,
                asset.VendorCode ?? "",
                asset.CompanyCode ?? "",
                asset.ManagedBy ?? "",
                asset.LPONo ?? "",
                asset.KmPerHr,
                asset.ConditionRank,
                asset.Status,
                asset.RentOrOwned,
                asset.RateType ?? "",
                asset.Rate,
                asset.ModifiedAsset,
                asset.Remarks ?? "",
                asset.AccountCategory,
                asset.RateOfDepreciation,
                asset.ItemCode,
                asset.PurchaseAmount,
                asset.DateOfSelling,
                asset.SoldAmount,
                asset.ParkingArea,
                userId);

            returnAsset.InsertAdditional(asset.Additional.Owner,
                asset.Additional.OperatedBy,
                asset.Additional.HireMethod,
                asset.Additional.Dimension,
                asset.Additional.Warranty,
                asset.Additional.RegistrationExpiry,
                asset.Additional.Insurance,
                asset.Additional.WarrantyPath,
                asset.Additional.RegPath,
                asset.Additional.InsurancePath,
                asset.Additional.WarrantyName,
                asset.Additional.RegName,
                asset.Additional.InsuranceName,
                asset.Additional.AssetType);

            var assetStatus = selections.FirstOrDefault(s => s.Value == asset.Status.ToString());

            returnAsset.DeliveryNote = string.IsNullOrEmpty(asset.DeliveryNote) ? "N/A" : asset.DeliveryNote;
            returnAsset.TankCapacity = asset.TankCapacity;

            if (assetStatus != null)
            {
                returnAsset.StatusDescription = assetStatus.Text;
            }

            return returnAsset;

        }
    }
}
