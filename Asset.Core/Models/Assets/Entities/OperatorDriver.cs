using System.Runtime.InteropServices;

namespace Asset.Core.Models.Assets.Entities;

public class OperatorDriver
{
    public static OperatorDriver Create(string assetCode,
        string? assetTypeCode, 
        string? division, 
        string? brandCode, 
        string empCode, 
        string empType, 
        string empName, 
        string? rPNo, 
        string? company, 
        string? mobileNo, 
        string? assetLocation, 
        string? vendorCode, 
        int internalExternal, 
        DateTime? assignedAt, 
        DateTime? returnedAt, 
        int? dcsSlNo,
        string? createdBy)
    {
        return new OperatorDriver
        {
            EmpType = empType,
            EmpCode = empType == "employee" ? empCode : rPNo,
            EmpName = empName,
            RPNo = rPNo,
            Company = company,
            MobileNo = mobileNo,
            AssetLocation = assetLocation,
            CreatedBy = createdBy,
            Division = "PMV",
            AssignedAt = assignedAt,
            BrandCode = brandCode,
            InternalExternal = internalExternal,
            DcsSlNo = dcsSlNo
        };
    }
    public void Update(string assetCode, 
        string? assetTypeCode, 
        string? division, 
        string? brandCode, 
        string empCode, 
        string empType, 
        string empName, 
        string? rPNo, string? company, string? mobileNo, string? department, string? assetLocation, string? vendorCode, int internalExternal, DateTime? assignedAt, DateTime? returnedAt, int? dcsSlNo)
    {
       
        AssetCode = assetCode;
        AssetTypeCode = assetTypeCode;
        Division = division;
        BrandCode = brandCode;
        EmpCode = empCode;
        EmpType = empType;
        EmpName = empName;
        RPNo = rPNo;
       
        Company = company;
        MobileNo = mobileNo;
        Department = department;
        AssetLocation = assetLocation;
        VendorCode = vendorCode;
        InternalExternal = internalExternal;
        AssignedAt = assignedAt;
        ReturnedAt = returnedAt;
        DcsSlNo = dcsSlNo;
    }

    public int Id { get; set; } = 0;
    public string AssetCode { get; set; } = "";
    public string? AssetTypeCode { get; set; }
    public string? Division { get; set; }
    public string? BrandCode { get; set; }
    public string EmpCode { get; set; } = string.Empty;
    public string EmpType { get; set; } = string.Empty;
    public string EmpName { get; set; } = string.Empty;
    public string? RPNo { get; set; }
   
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
