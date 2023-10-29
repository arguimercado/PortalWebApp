using BaseEntityPack.Core;

namespace Asset.Core.Models.Assets;




public class ExternalAsset : AggregateRoot<Guid>
{

    public ExternalAsset() : base(Guid.NewGuid())
    {

    }


    public ExternalAsset(
        string assetCode,
        string assetDesc,
        string plateType,
        string plateNum,
        string vendorCode,
        string companyCode,
        string hireSub,
        string employeeCode) : base(Guid.NewGuid())
    {
        if (string.IsNullOrEmpty(assetCode))
        {
            AssetCode = $"{plateType}{plateNum}";
        }
        else
        {
            AssetCode = assetCode;
        }
        CompanyCode = companyCode;
        AssetDesc = assetDesc;
        PlateType = plateType;
        PlateNum = plateNum;
        VendorCode = vendorCode;
        HireSub = hireSub;
        CreatedAt = DateTime.Now;
        CreatedBy = employeeCode;
    }

    public string AssetCode { get; set; } = "";
    public string AssetDesc { get; set; } = "";
    public string PlateType { get; set; } = "";
    public string PlateNum { get; set; } = "";
    public string CompanyCode { get; set; } = "";
    public string VendorCode { get; set; } = "";
    public string HireSub { get; set; } = "";
    public float FuelTankCapacity { get; set; } = 0;
    public bool IsActive { get; set; } = true;

}
