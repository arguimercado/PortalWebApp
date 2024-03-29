﻿
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
                var assetTypeCode = "";
                var vendorCode = "";
                var brandCode = "";
                if(request.InternalExternal == 1)
                {
                    var asset = await _assetDataService.GetInternal(request.AssetCode);
                    assetTypeCode = asset.SubCatCode;
                    vendorCode = asset.VendorCode;
                    brandCode = asset.BrandCode;
                    
                }
                else
                {
                    var asset = await _assetDataService.GetExternalAsset(request.AssetCode);
                    assetTypeCode = asset.PlateType;
                    vendorCode = asset.VendorCode;
                    brandCode = "N/A";

                }

                var operatorDriver = OperatorDriver.Create(request.AssetCode, assetTypeCode,
                    request.Division, brandCode, request.EmpCode, request.EmpType, request.Name, request.RPNo,
                    request.Company, request.MobileNo, request.AssetLocation,
                    request.VendorCode, request.InternalExternal,
                    request.AssignedAt, request.ReturnedAt,
                    request.DcsSlNo,
                    request.CreatedBy);
                
                await _dataService.SaveOperator(operatorDriver);
                return Result.Ok(Unit.Value);
               
            }
            catch(Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }
    }
}



