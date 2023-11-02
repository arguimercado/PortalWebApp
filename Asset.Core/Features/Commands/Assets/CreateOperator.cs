
using Asset.Core.Contracts.Assets;
using Asset.Core.Models.Assets.Entities;

namespace Asset.Core.Features.Commands.Assets;

public static class  CreateOperator
{
    public record Command : IRequest<Result<Unit>>
    {
        public Command(string assetCode,
                              string? assetTypeCode,
                              string? division,
                              string? brandCode,
                              string empCode,
                              string empType,
                              string? rPNo,
                              string? name,
                              string? company,
                              string? mobileNo,
                              string? department,
                              string? assetLocation,
                              string? vendorCode,
                              int internalExternal,
                              DateTime? assignedAt,
                              DateTime? returnedAt,
                              int? dcsSlNo,
                              string? createdBy,
                              DateTime? createdAt)
        {
            AssetCode = assetCode;
            AssetTypeCode = assetTypeCode;
            Division = division;
            BrandCode = brandCode;
            EmpCode = empCode;
            EmpType = empType;
            RPNo = rPNo;
            Name = name;
            Company = company;
            MobileNo = mobileNo;
            Department = department;
            AssetLocation = assetLocation;
            VendorCode = vendorCode;
            InternalExternal = internalExternal;
            AssignedAt = assignedAt;
            ReturnedAt = returnedAt;
            DcsSlNo = dcsSlNo;
            CreatedBy = createdBy;
            CreatedAt = createdAt;
        }

        public string AssetCode { get; }
        public string? AssetTypeCode { get; }
        public string? Division { get; }
        public string? BrandCode { get; }
        public string EmpCode { get; }
        public string EmpType { get; }
        public string Name { get; }
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

    public class CommandHandler : IRequestHandler<Command, Result<Unit>>
    {
        private readonly IOperatorDataService _dataService;
        private readonly IAssetDataService _assetDataService;

        public CommandHandler(IOperatorDataService dataService,IAssetDataService assetDataService)
        {
            _dataService = dataService;
            _assetDataService = assetDataService;
        }

        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            try
            {


                var operatorDriver = new OperatorDriver
                {
                    EmpType = request.EmpType,
                    EmpCode = request.EmpType == "employee" ? request.EmpCode : request.RPNo,
                    EmpName = request.Name,
                    RPNo = request.RPNo,
                    Company = request.Company,
                    MobileNo = request.MobileNo,
                    AssetLocation = request.AssetLocation,
                    CreatedBy = request.CreatedBy,
                    Division = "PMV",
                    AssignedAt = request.AssignedAt,
                    BrandCode = request.BrandCode,
                    InternalExternal = request.InternalExternal,
                    DcsSlNo = request.DcsSlNo
                };

                if(request.AssetTypeCode == "internal")
                {
                    var internalAsset = _assetDataService.GetInternal(request.AssetCode);
                }

            }
            catch(Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }
    }
}



