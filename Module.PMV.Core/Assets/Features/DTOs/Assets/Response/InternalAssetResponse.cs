using Module.PMV.Core.Assets.Models.Assets;
using Module.PMV.Core.Assets.Models.Assets.Entities;

namespace Module.PMV.Core.Assets.Features.DTOs.Assets.Response;

public class InternalAssetResponse
{
    public static InternalAssetResponse MapToResponse(InternalAsset model) {
        
        var response = new InternalAssetResponse
        {
            SlNo = model.SlNo,
            Cid = model.Cid,
            SubCatCode = model.SubCatCode,
            AssetCode = model.AssetCode,
            AssetDesc = model.AssetDesc,
            BrandCode = model.BrandCode,
            Model = model.Model,
            Year = model.Year,
            PlateNo = model.PlateNo,
            EngineNo = model.EngineNo,
            ChasisNo = model.ChasisNo,
            Color = model.Color,
            FirstRegDate = model.FirstRegDate,
            PurchaseDate = model.PurchaseDate,
            DispositionDate = model.DispositionDate,
            NetValue = model.NetValue,
            OriginalPurchasePrice = model.OriginalPurchasePrice,
            VendorCode = model.VendorCode,
            CompanyCode = model.CompanyCode,
            LPONo = model.LPONo,
            ManagedBy = model.ManagedBy,
            DeliveryNote = model.DeliveryNote,
            KmHr = model.KmPerHr,
            ConditionRank = model.ConditionRank,
            Status = model.Status,
            RentOrOwned = model.RentOrOwned,
            RateType = model.RateType,
            Rate = model.Rate,
            RateOfDepreciation = model.RateOfDepreciation,
            ModifiedAsset = model.ModifiedAsset,
            Remarks = model.Remarks,
            AccountCategory = model.AccountCategory,
            AccountDepreciation = model.AccountDepreciation,
            Completed = model.Completed,
            TankCapacity = model.TankCapacity,
            ParkingArea = model.ParkingArea,
        };

        if(model.AssetAdditional is not null) {

            response.Additional = new AssetAdditionalResponse {
                AssetType = model.AssetAdditional.AssetType,
                Dimension = model.AssetAdditional.Dimension,
                HireMethod = model.AssetAdditional?.HireMethod,
                RegName = model.AssetAdditional?.RegName,
                RegistrationExpiry = model.AssetAdditional?.RegistrationExpiry ?? null,
                Owner = model.AssetAdditional?.Owner,
                OperatedBy = model.AssetAdditional?.OperatedBy,
            };
        }

        return response;
    }
    public int SlNo { get; set; }
    public int Cid { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public string SubCatCode { get; set; } = string.Empty;
    public int AssetNo { get; set; }
    public string? AssetCode { get; set; }
    public string? AssetDesc { get; set; } = null;
    public string? BrandCode { get; set; }
    public string? Model { get; set; }
    public int Year { get; set; }
    public string? Color { get; set; }
    public string? PlateNo { get; set; }
    public string? EngineNo { get; set; }
    public string? ChasisNo { get; set; }
    public DateTime? FirstRegDate { get; set; }
    public DateTime? PurchaseDate { get; set; }
    public float NetValue { get; set; }
    public float OriginalPurchasePrice { get; set; }
    public string? VendorCode { get; set; }
    public string? CompanyCode { get; set; }
    public string? ManagedBy { get; set; }
    public string? LPONo { get; set; }
    public string? DeliveryNote { get; set; }
    public int KmHr { get; set; }
    public int ConditionRank { get; set; }
    public int Status { get; set; } = 1;
    public string StatusDesc { get; set; } = "";
    public string? RentOrOwned { get; set; }
    public string? RateType { get; set; }
    public float Rate { get; set; }
    public bool? ModifiedAsset { get; set; }
    public string? Remarks { get; set; }
    public string? AccountCategory { get; set; }
    public float RateOfDepreciation { get; set; }
    public float AccountDepreciation { get; set; }
    public string? ItemCode { get; set; }
    public float PurchaseAmount { get; set; }
    public DateTime? DateOfSelling { get; set; }
    public float SoldAmount { get; set; }
    public bool Completed { get; set; }
    public AssetAdditionalResponse? Additional { get; set; } = new();
    public float ParkingArea { get; set; }
    public DateTime? DispositionDate { get; set; }
    public int TankCapacity { get; set; } = 0;
    public string? CreatedBy { get; set; }
    public DateTime? CreatedDate { get; set; }

    public class AssetAdditionalResponse
    {
        public string? AssetCode { get; set; }

        public string? Owner { get; set; }

        public string? OperatedBy { get; set; }

        public string? HireMethod { get; set; }

        public string? Dimension { get; set; }

        public DateTime? Warranty { get; set; }

        public DateTime? RegistrationExpiry { get; set; }

        public DateTime? Insurance { get; set; }

        public string? WarrantyPath { get; set; }

        public string? RegPath { get; set; }

        public string? InsurancePath { get; set; }

        public string? WarrantyName { get; set; }

        public string? RegName { get; set; }

        public string? InsuranceName { get; set; }

        public string? AssetType { get; set; }
    }

    public IEnumerable<OperatorDriverResponse> Drivers { get; set; } = new List<OperatorDriverResponse>();
    public List<ServiceAlertResponse> ServiceDues { get; set; } = new();
    public List<AssetDocumentResponse> Documents { get; set; } = new();
}

public class OperatorDriverResponse
{
    public int Id { get; set; } = 0;
    public string AssetCode { get; set; } = string.Empty;
    public string? AssetTypeCode { get; set; }
    public string? Division { get; set; }
    public string? BrandCode { get; set; }
    public string EmpCode { get; set; } = string.Empty;
    public string EmpType { get; set; } = string.Empty;
    public string EmpName { get; set; } = string.Empty;
    public string? RPNo { get; set; }
    public string? Name { get; set; }
    public string? Company { get; set; }
    public string? MobileNo { get; set; }
    public string? Department { get; set; }
    public string? AssetLocation { get; set; } = string.Empty;
    public string? VendorCode { get; set; }
    public int InternalExternal { get; set; }
    public DateTime? AssignedAt { get; set; }
    public DateTime? ReturnedAt { get; set; }
    public int? DcsSlNo { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? CreatedAt { get; set; }
}

public class AssetDocumentResponse
{
    public int AssetId { get; set; }
    public Guid? DocumentId { get; set; }
    public string? Title { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public string? DocumentType { get; set; } = string.Empty;
    public string? FileName { get; set; } = string.Empty;
    public string? DocumentPath { get; set; }
    public string? DocumentReferenceNo { get; set; } = string.Empty;

}
