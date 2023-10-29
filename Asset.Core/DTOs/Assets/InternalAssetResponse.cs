using Microsoft.AspNetCore.Http;

namespace Asset.Core.DTOs.Assets;

public class InternalAssetResponse
{
    public int SlNo { get; set; }
    public string AssetCode { get; set; } = string.Empty;
    public string AssetDesc { get; set; } = string.Empty;
    public string CompanyCode { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string SubCatName { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
    public string ChasisNo { get; set; } = string.Empty;
    public int KmHr { get; set; }

    public int TankCapacity { get; set; } = 0;
    public int CurrentSMUReading { get; set; }
    public string VendorCode { get; set; } = string.Empty;
    public string LpoNo { get; set; } = string.Empty;
    public string DeliveryNote { get; set; } = string.Empty;
    public string RateType { get; set; } = string.Empty;
    public string AccountCategory { get; set; } = string.Empty;
    public int ConditionRank { get; set; }
    public DateTime? FirstRegDate { get; set; }
    public string EngineNo { get; set; } = string.Empty;
    public string PlateNo { get; set; } = string.Empty;

    public float NetValue { get; set; }

    public string RentOwned { get; set; } = string.Empty;

    public int ModifiedAsset { get; set; }

    public float Rate { get; set; }
    public float RateOfDepreciation { get; set; }
    public string YearMakeModel { get; set; } = string.Empty;
    public string HireMethod { get; set; } = string.Empty;
    public string Dimension { get; set; } = string.Empty;

    public DateTime? RegistrationExpiry { get; set; }
    public DateTime? Warranty { get; set; }

    public DateTime? Insurance { get; set; }

    public string Owner { get; set; } = string.Empty;
    public string OperatedBy { get; set; } = string.Empty;
    public string EmpName { get; set; } = string.Empty;
    public DateTime? CreatedAt { get; set; }
    public string CreatedBy { get; set; } = "";
    public int Status { get; set; }
    public string StatusDescription
    {
        get
        {
            var StatusDesc = "";

            if (Status == 0)
            {
                StatusDesc = "Inactive";
            }
            else if (Status == 1)
            {
                StatusDesc = "Active";
            }
            else if (Status == 2)
            {
                StatusDesc = "Sold";
            }
            else if (Status == 3)
            {
                StatusDesc = "Traded-In";
            }
            else if (Status == 4)
            {
                StatusDesc = "Scrapped";
            }
            else if (Status == 5)
            {
                StatusDesc = "Retired";
            }

            return StatusDesc;
        }
    }

    public bool Completed { get; set; }
    public IEnumerable<AssignedDriverResponse> Drivers { get; set; } = new List<AssignedDriverResponse>();
    public List<ServiceDueResponse> ServiceDues { get; set; } = new();
    public List<AssetDocumentResponse> Documents { get; set; } = new();
}

public class AssignedDriverResponse {
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
    public IFormFile? Content { get; set; } = null!;
    public string? Tracker { get; set; } = "";
}

public class ServiceDueResponse
{
    public string GroupId { get; set; } = string.Empty;
    public Guid Id { get; set; } = Guid.Empty;
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public DateTime? LastServiceDate { get; set; }
    public int LastSMUReading { get; set; }
    public int CurrentSMUReading { get; set; }
    public int KmAlert { get; set; }
    public int AlertDue { get; set; }
    public int KmInterval { get; set; }
    public string SMU { get; set; }
    public int IntervalDue { get; set; }
    public string Status { get; set; } = string.Empty;
}