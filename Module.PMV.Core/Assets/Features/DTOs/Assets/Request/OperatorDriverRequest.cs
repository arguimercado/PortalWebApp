namespace Module.PMV.Core.Assets.Features.DTOs.Assets.Request;

public class OperatorDriverRequest
{ 
    public OperatorDriverRequest(string assetCode,
                              string? division,
                              string empCode,
                              string empType,
                              string? rPNo,
                              string? name,
                              string? company,
                              string? mobileNo,
                              string? department,
                              int internalExternal,
                              DateTime? assignedAt,
                              DateTime? returnedAt,
                              int? dcsSlNo,
                              string? createdBy)
        {
            
            AssetCode = assetCode;
            Division = division;
            EmpCode = empCode;
            EmpType = empType;
            RPNo = rPNo;
            Name = name;
            Company = company;
            MobileNo = mobileNo;
            Department = department;
            InternalExternal = internalExternal;
            AssignedAt = assignedAt;
            ReturnedAt = returnedAt;
            DcsSlNo = dcsSlNo;
            CreatedBy = createdBy;
        }

        public string AssetCode { get; }
        public string? AssetTypeCode { get; }
        public string? Division { get; }
        public string? BrandCode { get; }
        public string EmpCode { get; }
        public string EmpType { get; }
        public string? Name { get; }
        public string? RPNo { get; }
        public string? Company { get; }
        public string? MobileNo { get; }
        public string? Department { get; }
        public string? AssetLocation { get; }
        public string? VendorCode { get; }
        public int InternalExternal { get; }
        public DateTime? AssignedAt { get; }
        public DateTime? ReturnedAt { get; }
        public int? DcsSlNo { get; }
        public string? CreatedBy { get; }
        public DateTime? CreatedAt { get; }
    
}
