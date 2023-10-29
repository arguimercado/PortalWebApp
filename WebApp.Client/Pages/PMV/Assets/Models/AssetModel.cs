using System.Net.Http.Headers;
using WebApp.UILibrary.Commons;

namespace WebApp.Client.Pages.PMV.Assets.Models;


public class AssetContainerModel
{

    public static AssetContainerModel Create()
    {
        return new AssetContainerModel();
    }

    public static InternalAssetModel CreateInternal(InternalAssetModel asset)
    {
        return new InternalAssetModel
        {
            SlNo = asset.SlNo,
            AssetCode = asset.AssetCode,
            Cid = asset.Cid,
            SubCatCode = asset.SubCatCode,
            AssetDesc = asset.AssetDesc,
            Model = asset.Model,
            Year = asset.Year,
            BrandCode = asset.BrandCode,
            Color = asset.Color,
            EngineNo = asset.EngineNo,
            ChasisNo = asset.ChasisNo,
            AccountCategory = asset.AccountCategory,
            AccountDepreciation = asset.AccountDepreciation,
            AssetNo = asset.AssetNo,
            DeliveryNote = asset.DeliveryNote,
            FirstRegDate = asset.FirstRegDate,
            KmPerHr = asset.KmPerHr,
            ManagedBy = asset.ManagedBy,
            NetValue = asset.NetValue,
            PlateNo = asset.PlateNo,
            PurchaseDate = asset.PurchaseDate,
            Status = asset.Status,
            LPONo = asset.LPONo,
            CompanyCode = asset.CompanyCode,
            ConditionRank = asset.ConditionRank,
            ModifiedAsset = asset.ModifiedAsset,
            ParkingArea = asset.ParkingArea,
            TankCapacity = asset.TankCapacity,
            PurchaseAmount = asset.PurchaseAmount,
            Rate = asset.Rate,
            RateOfDepreciation = asset.RateOfDepreciation,
            RateType = asset.RateType,
            VendorCode = asset.VendorCode,
            SoldAmount = asset.SoldAmount,
            RentOrOwned = asset.RentOrOwned,
            Remarks = asset.Remarks,
            Additional = new AssetAdditionalModel
            {
                AssetCode = asset.AssetCode,
                AssetType = asset.Additional.AssetType,
                Dimension = asset.Additional.Dimension,
                HireMethod = asset.Additional.HireMethod,
                Insurance = asset.Additional.Insurance,
                InsuranceName = asset.Additional.InsuranceName,
                InsurancePath = asset.Additional.InsurancePath,
                OperatedBy = asset.Additional.OperatedBy,
                Owner = asset.Additional.Owner,
                RegistrationExpiry = asset.Additional.RegistrationExpiry,
                RegName = asset.Additional.RegName,
                RegPath = asset.Additional.RegPath,
                Warranty = asset.Additional.Warranty,
                WarrantyName = asset.Additional.WarrantyName,
                WarrantyPath = asset.Additional.WarrantyPath
            }
        };
    }


    public InternalAssetModel InternalAsset { get; set; } = new();
    public ExternalAssetModel ExternalAsset { get; set; } = new();

    public IEnumerable<SelectItem> Categories { get; set; } = new List<SelectItem>();
    public IEnumerable<SelectItem> SubCategories { get; set; } = new List<SelectItem>();
    public IEnumerable<SelectItem> Companies { get; set; } = new List<SelectItem>();
    public IEnumerable<SelectItem> Brands { get; set; } = new List<SelectItem>();

    public IEnumerable<SelectItem> Vendors { get; set; } = new List<SelectItem>();

    public IEnumerable<SelectItem> RentOwnes { get; set; } = new List<SelectItem>();

    public IEnumerable<SelectItem> HireMethods { get; set; } = new List<SelectItem>();

    public IEnumerable<SelectItem> AssetTypes { get; set; } = new List<SelectItem>();
    public IEnumerable<SelectItem> Statuses { get; set; } = new List<SelectItem>();

    public IEnumerable<SelectItem> Accounts { get; set; } = new List<SelectItem>();

    public IEnumerable<SelectItem> ServiceGroups { get; set; } = new List<SelectItem>();

    public IEnumerable<SelectItem> PlateTypes { get; set; } = new List<SelectItem>();

    public IEnumerable<OperatorDriver> Drivers { get; set; } = new List<OperatorDriver>();

    public IEnumerable<SelectItem> GetSubCategoryByCat
    {
        get
        {
            var sub = SubCategories.Where(s => s.Type == InternalAsset.Cid.ToString()).ToList();
            if (sub is null)
                return SubCategories.ToList();
            return sub;
        }
    }
}


public class InternalAssetModel
{
    public int SlNo { get; set; }
    public int Cid { get; set; }
    public string SubCatCode { get; set; } = string.Empty;
    public int AssetNo { get; set; }
    public string AssetCode { get; set; } = string.Empty;
    public string? AssetDesc { get; set; } = null;
    public string BrandCode { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public int Year { get; set; }
    public string? Color { get; set; }
    public string? PlateNo { get; set; }
    public string? EngineNo { get; set; }
    public string? ChasisNo { get; set; }
    public DateTime? FirstRegDate { get; set; }
    public DateTime? PurchaseDate { get; set; }
    public float NetValue { get; set; }
    public string VendorCode { get; set; } = string.Empty;
    public string CompanyCode { get; set; } = string.Empty;
    public string ManagedBy { get; set; } = string.Empty;
    public string LPONo { get; set; } = string.Empty;
    public string? DeliveryNote { get; set; } = string.Empty;
    public int KmPerHr { get; set; }
    public int ConditionRank { get; set; }
    public int Status { get; set; } = 1;

    public string RentOrOwned { get; set; } = string.Empty;

    public string RateType { get; set; } = string.Empty;
    public float Rate { get; set; }


    public bool? ModifiedAsset { get; set; }

    public string Remarks { get; set; } = string.Empty;

    public string AccountCategory { get; set; } = string.Empty;

    public float RateOfDepreciation { get; set; }

    public float AccountDepreciation { get; set; }


    public string ItemCode { get; set; } = string.Empty;

    public float PurchaseAmount { get; set; }

    public DateTime? DateOfSelling { get; set; }

    public float SoldAmount { get; set; }

    public float ParkingArea { get; set; } = 0;

    public int TankCapacity { get; set; } = 0;

    public AssetAdditionalModel Additional { get; set; } = new();
    public List<ServiceDueEntryModel> Dues { get; set; } = new();
    public List<AssetDocument> Documents { get; set; } = new();

    public List<AssignedDriver> Drivers { get; set; } = new();


}

public class ServiceDueEntryModel
{
    public string Id { get; set; } = string.Empty;
    public string GroupId { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public DateTime? LastServiceDate { get; set; }
    public int LastSMUReading { get; set; }
    public int CurrentSMUReading { get; set; }
    public int KmAlert { get; set; }
    public int AlertDue { get; set; }
    public int KmInterval { get; set; }

    public string SMU { get; set; } = string.Empty;
    public int IntervalDue { get; set; }
    public string Status { get; set; } = string.Empty;
    public string Source { get; set; } = string.Empty;
    public bool MarkDeleted { get; set; }

    public string Remarks { get; set; }

    public void Recalculate()
    {
        AlertDue = LastSMUReading + KmAlert;
        IntervalDue = AlertDue + KmInterval;
    }
}

public class AssetDocument
{


    
    private StreamContent? _content;

    public string Id { get; set; } = string.Empty;
    public string DocumentId { get; set; } = string.Empty;
    public int AssetId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string DocumentType { get; set; } = string.Empty;
    public string FileName { get; set; } = string.Empty;
    public string DocumentReferenceNo { get; set; } = string.Empty;
    public string DocumentPath { get; set; } = string.Empty;
    public string Tracker { get; set; } = "";

    public bool IsContentEmpty()
    {
        return string.IsNullOrEmpty(FileName);
    }

    public void SetStream(Stream stream, string contentType, string fileName)
    {
        _content = new StreamContent(stream);
        _content.Headers.ContentType = MediaTypeHeaderValue.Parse(contentType);
        FileName = fileName;
    }

    public MultipartFormDataContent GetFormData()
    {

        var formData = new MultipartFormDataContent();
        if (_content is not null)
        {
            formData.Add(_content, "\"Content\"", FileName);
        }
        formData.Add(new StringContent(Id), "\"Id\"");
        formData.Add(new StringContent(AssetId.ToString()), "\"AssetId\"");
        formData.Add(new StringContent(Title), "\"Title\"");
        formData.Add(new StringContent(Description), "\"Description\"");
        formData.Add(new StringContent(DocumentReferenceNo.ToString()), "\"DocumentReferenceNo\"");
        formData.Add(new StringContent(DocumentType), "\"DocumentType\"");
        formData.Add(new StringContent(Tracker), "\"Tracker\"");

        return formData;
    }


}
public class AssetAdditionalModel
{
    public string AssetCode { get; set; } = string.Empty;

    public string Owner { get; set; } = string.Empty;

    public string OperatedBy { get; set; } = string.Empty;

    public string HireMethod { get; set; } = string.Empty;

    public string Dimension { get; set; } = string.Empty;

    public DateTime? Warranty { get; set; }

    public DateTime? RegistrationExpiry { get; set; }

    public DateTime? Insurance { get; set; }

    public string WarrantyPath { get; set; } = string.Empty;

    public string RegPath { get; set; } = string.Empty;

    public string InsurancePath { get; set; } = string.Empty;

    public string WarrantyName { get; set; } = string.Empty;

    public string RegName { get; set; } = string.Empty;

    public string InsuranceName { get; set; } = string.Empty;

    public string AssetType { get; set; } = string.Empty;
}

public class AssignedDriver
{
    public int Id { get; set; } = 0;
    public string AssetCode { get; set; }
    public string? AssetTypeCode { get; set; }
    public string? Division { get; set; }
    public string? BrandCode { get; set; }
    public string EmpCode { get; set; } = string.Empty;
    public string? EmpName { get; set; }
    public string EmpType { get; set; } = "";
    public string RPNo { get; set; } = "";
    public string Company { get; set; } = "";
    public string MobileNo { get; set; } = "";
    public string? Department { get; set; } = string.Empty;
    public string? AssetLocation { get; set; } = string.Empty;
    public string? VendorCode { get; set; }
    public int InternalExternal { get; set; }
    public string ConvertedInternalExternal => InternalExternal == 1 ? "Internal" : "External";
    public DateTime? AssignedAt { get; set; }
    public DateTime? ReturnedAt { get; set; }
    public int? DcsSlNo { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? CreatedAt { get; set; }

}

public class ExternalAssetModel
{
    public string AssetCode { get; set; } = "";
    public string AssetDesc { get; set; } = "";
    public string PlateType { get; set; } = "";
    public string PlateNum { get; set; } = "";
    public string VendorCode { get; set; } = "";
    public string CompanyCode { get; set; } = "";
    public string HireSub { get; set; } = "";
    public string CreatedBy { get; set; } = "";
    public DateTime? CreatedAt { get; set; }

    public float FuelTankCapacity { get; set; }

    public List<AssignedDriver> Drivers { get; set; } = new();

}


