using Applications.Interfaces;
using Asset.Core.Contracts.Assets;
using Asset.Core.DTOs.Assets;
using Asset.Core.Models.Assets;
using Asset.Core.Models.Assets.Entities;

namespace Asset.Core.Infrastructures.Services.Assets;

public class AssetDataService : IAssetDataService
{
    private readonly ISqlQuery _sqlQuery;

    public AssetDataService(ISqlQuery sqlQuery)
    {
        _sqlQuery = sqlQuery;
    }
    public async Task<int> CreateInternalAssetNo(string subCatCode)
    {

        string query = "SELECT AssetNo FROM PMV WHERE SubCatCode = @subCatCode ORDER BY AssetNo DESC";
        var results = await _sqlQuery.DynamicQueryScalar<int>(query, new { subCatCode = subCatCode });
        return results;
    }

    public async Task CreateUpdateInternal(InternalAsset asset, bool onlyDetail = false)
    {
        DynamicParameters dynamicParameters = new DynamicParameters();

        dynamicParameters.Add("@AssetId", asset.SlNo);
        dynamicParameters.Add("@Cid", asset.Cid);
        dynamicParameters.Add("@SubCatCode", asset.SubCatCode);
        dynamicParameters.Add("@AssetCode", asset.AssetCode);
        dynamicParameters.Add("@AssetNo", asset.AssetNo);
        dynamicParameters.Add("@AssetDesc", asset.AssetDesc);
        dynamicParameters.Add("@BrandCode", asset.BrandCode);
        dynamicParameters.Add("@Model", asset.Model);
        dynamicParameters.Add("@Year", asset.Year);
        dynamicParameters.Add("@Color", asset.Color);
        dynamicParameters.Add("@PlateNo", asset.PlateNo);
        dynamicParameters.Add("@EngineNo", asset.EngineNo);
        dynamicParameters.Add("@ChasisNo", asset.ChasisNo);
        dynamicParameters.Add("@FirstRegDate", asset.FirstRegDate);
        dynamicParameters.Add("@PurchaseDate", asset.PurchaseDate);
        dynamicParameters.Add("@NetValue", asset.NetValue);
        dynamicParameters.Add("@VendorCode", asset.VendorCode);
        dynamicParameters.Add("@CompanyCode", asset.CompanyCode);
        dynamicParameters.Add("@ManagedBy", asset.ManagedBy);
        dynamicParameters.Add("@LpoNO", asset.LPONo);
        dynamicParameters.Add("@DeliveryNote", asset.DeliveryNote);
        dynamicParameters.Add("@KmHR", asset.KmPerHr);
        dynamicParameters.Add("@ConditionRank", asset.ConditionRank);
        dynamicParameters.Add("@Status", asset.Status);
        dynamicParameters.Add("@StatusDesc", asset.StatusDescription);
        dynamicParameters.Add("@CreatedBy", asset.CreatedBy);
        dynamicParameters.Add("@CreatedAt", asset.CreatedAt);
        dynamicParameters.Add("@RentOwned", asset.RentOrOwned);
        dynamicParameters.Add("@RateType", asset.RateType);
        dynamicParameters.Add("@Rate", asset.Rate);
        dynamicParameters.Add("@ModifiedAsset", asset.ModifiedAsset);
        dynamicParameters.Add("@Remarks", asset.Remarks);
        dynamicParameters.Add("@AccountCategory", asset.AccountCategory);
        dynamicParameters.Add("@RateOfDepreciation", asset.RateOfDepreciation);
        dynamicParameters.Add("@AccountDepreciation", asset.AccountDepreciation);
        dynamicParameters.Add("@ItemCode", asset.ItemCode);
        dynamicParameters.Add("@PurchaseAmount", asset.PurchaseAmount);
        dynamicParameters.Add("@DateOfSelling", asset.DateOfSelling);
        dynamicParameters.Add("@SoldAmount", asset.SoldAmount);
        dynamicParameters.Add("@ParkingArea", asset.ParkingArea);
        dynamicParameters.Add("@TankCapacity", asset.TankCapacity);
        dynamicParameters.Add("@Owner", asset.AssetAdditional.Owner);
        dynamicParameters.Add("@OperatedBy", asset.AssetAdditional.OperatedBy);
        dynamicParameters.Add("@HireMethod", asset.AssetAdditional.HireMethod);
        dynamicParameters.Add("@Warranty", asset.AssetAdditional.Warranty);
        dynamicParameters.Add("@RegistrationExpiryDate", asset.AssetAdditional.RegistrationExpiry);
        dynamicParameters.Add("@Insurance", asset.AssetAdditional.Insurance);
        dynamicParameters.Add("@WarrantyPath", asset.AssetAdditional.WarrantyPath);
        dynamicParameters.Add("@RegPath", asset.AssetAdditional.RegPath);
        dynamicParameters.Add("@InsurancePath", asset.AssetAdditional.InsurancePath);
        dynamicParameters.Add("@WarrantyName", asset.AssetAdditional.WarrantyName);
        dynamicParameters.Add("@RegName", asset.AssetAdditional.RegName);
        dynamicParameters.Add("@InsuranceName", asset.AssetAdditional.InsuranceName);
        dynamicParameters.Add("@AssetType", asset.AssetAdditional.AssetType);
        dynamicParameters.Add("@PMVDimension", asset.AssetAdditional.Dimension);

        await _sqlQuery.DynamicExecute("sp_InsertUpdateAsset", dynamicParameters, System.Data.CommandType.StoredProcedure);
    }

    public async Task<InternalAsset> GetInternal(object value)
    {
        var strSQL = @"SELECT 
					pmv.slno SlNo,
					pmv.cid Cid,
					pmv.SubCatCode SubCatCode,
					pmv.AssetCode,
					pmv.AssetNo,
					pmv.AssetDesc,
					pmv.BrandCode,
					pmv.Model,
					pmv.Year,
					pmv.Color,
					pmv.PlateNo,
					pmv.EngineNo,
					pmv.ChasisNo, 
					pmv.FirstRegDate,
					pmv.PurchaseDate,
					pmv.NetValue,
					pmv.VendorCode,
					pmv.CompanyCode,
					pmv.managedBy ManagedBy,
					pmv.LpoNo LPONo,
					pmv.KmHR KmPerHr,
					pmv.CondtionRank ConditionRank,
					pmv.status Status,
					pmv.statusDesc StatusDesc,
					pmv.createdBy CreatedBy,
					pmv.createdAt CreatedAt,
					pmv.rentOwned RentOrOwned,
					pmv.Rate_Type RateType,
					pmv.Rate,
					pmv.modifiedAsset ModifiedAsset,
					pmv.remarks Remarks,
					pmv.accountcategory AccountCategory,
					pmv.rateofdepreciation RateOfDepreciation,
					pmv.accountdepreciation AccountDepreciation,
					pmv.ParkingFieldArea ParkingArea,
					pmv.DeliveryNote,
					pmv.Completed,
					pmv.TankCapacity,
					ad.assetCode AssetCode2,
					ad.hiremethod HireMethod,
					ad.assetType AssetType,
					ad.warantee Warranty,
					ad.registrationExpiry RegistrationExpiry,
					ad.insurance Insurance,
					ad.Owner Owner,
					ad.operatedby OperatedBy,
					doc.Id,
					doc.AssetId,
					doc.DocumentType,
					doc.FileName,
					doc.DocumentReferenceNo,
					doc.DocumentPath,
					doc.Title,
					doc.Description
				FROM PMV pmv
				LEFT JOIN PMV_AssetAdditional ad ON ad.assetCode = pmv.AssetCode
				LEFT JOIN HLMPMV.AssetDocument doc ON doc.AssetId = pmv.slno
				WHERE (pmv.slno = @id)";

        var results = await _sqlQuery.DynamicQuery<InternalAsset, AssetAdditional, AssetDocument, InternalAsset>(strSQL,
        new { id = value },
        (asset, additional, document) => {
            asset.AddAdditional(additional);
            if (document is not null) {
                asset.AddDocument(document);
            }
            return asset;
        },
        splitOn: "AssetCode2,Id");

        var asset = results.FirstOrDefault();
        if (asset is not null)
        {
            foreach (var currentAsset in results)
            {
                var document = currentAsset.Documents.FirstOrDefault();
                if (document is not null)
                {
                    if (!asset.Documents.Any(d => d.Id == currentAsset.Documents.FirstOrDefault().Id))
                    {
                        asset.AddDocuments(currentAsset.Documents.FirstOrDefault());
                    }
                }
            }
        }

        return asset ?? new();
    }

    public Task<InternalAsset> GetInternalByAssetCode(string assetCode)
    {
        var strSQL = @"SELECT 
					pmv.slno SlNo,
					pmv.cid Cid,
					pmv.SubCatCode SubCatCode,
					pmv.AssetCode,
					pmv.AssetNo,
					pmv.AssetDesc,
					pmv.BrandCode,
					pmv.Model,
					pmv.Year,
					pmv.Color,
					pmv.PlateNo,
					pmv.EngineNo,
					pmv.ChasisNo, 
					pmv.FirstRegDate,
					pmv.PurchaseDate,
					pmv.NetValue,
					pmv.VendorCode,
					pmv.CompanyCode,
					pmv.managedBy ManagedBy,
					pmv.LpoNo LPONo,
					pmv.KmHR KmPerHr,
					pmv.CondtionRank ConditionRank,
					pmv.status Status,
					pmv.statusDesc StatusDesc,
					pmv.createdBy CreatedBy,
					pmv.createdAt CreatedAt,
					pmv.rentOwned RentOrOwned,
					pmv.Rate_Type RateType,
					pmv.Rate,
					pmv.modifiedAsset ModifiedAsset,
					pmv.remarks Remarks,
					pmv.accountcategory AccountCategory,
					pmv.rateofdepreciation RateOfDepreciation,
					pmv.accountdepreciation AccountDepreciation,
					pmv.ParkingFieldArea ParkingArea,
					pmv.DeliveryNote,
					pmv.Completed,
					pmv.TankCapacity,
					ad.assetCode AssetCode2,
					ad.hiremethod HireMethod,
					ad.assetType AssetType,
					ad.warantee Warranty,
					ad.registrationExpiry RegistrationExpiry,
					ad.insurance Insurance,
					ad.Owner Owner,
					ad.operatedby OperatedBy,
					doc.Id,
					doc.AssetId,
					doc.DocumentType,
					doc.FileName,
					doc.DocumentReferenceNo,
					doc.DocumentPath,
					doc.Title,
					doc.Description
				FROM PMV pmv
				LEFT JOIN PMV_AssetAdditional ad ON ad.assetCode = pmv.AssetCode
				LEFT JOIN HLMPMV.AssetDocument doc ON doc.AssetId = pmv.slno
				WHERE (pmv.slno = @id)";

        var results = await _sqlQuery.DynamicQuery<InternalAsset, AssetAdditional, AssetDocument, InternalAsset>(strSQL,
        new { id = value },
        (asset, additional, document) => {
            asset.AddAdditional(additional);
            if (document is not null)
            {
                asset.AddDocument(document);
            }
            return asset;
        },
        splitOn: "AssetCode2,Id");

        var asset = results.FirstOrDefault();
        if (asset is not null)
        {
            foreach (var currentAsset in results)
            {
                var document = currentAsset.Documents.FirstOrDefault();
                if (document is not null)
                {
                    if (!asset.Documents.Any(d => d.Id == currentAsset.Documents.FirstOrDefault().Id))
                    {
                        asset.AddDocuments(currentAsset.Documents.FirstOrDefault());
                    }
                }
            }
        }

        return asset ?? new();
    }

    public async Task<object> GetInternalDynamics(string assetCode = "", string categoryIds = "", string subCategory = "", string brand = "", string companyCode = "", string status = "", string fields = "")
    {
        Dictionary<string, string> fieldOrigins = new Dictionary<string, string>();

        fieldOrigins.Add("id", "p.slno Id");
        fieldOrigins.Add("code", "p.AssetCode");
        fieldOrigins.Add("description", "p.AssetDesc");
        fieldOrigins.Add("category", "c.cname Category");
        fieldOrigins.Add("subcategory", "p.SubCatCode");
        fieldOrigins.Add("make", "b.BrandName");
        fieldOrigins.Add("model", "p.Model");
        fieldOrigins.Add("year", "p.Year");
        fieldOrigins.Add("plateNo", "p.PlateNo");
        fieldOrigins.Add("color", "p.Color");
        fieldOrigins.Add("chasisNo", "p.ChasisNo");
        fieldOrigins.Add("engineNo", "p.EngineNo");
        fieldOrigins.Add("registrationUnder", "p.CompanyCode");
        fieldOrigins.Add("purchaseDate", "p.PurchaseDate");
        fieldOrigins.Add("vendor", "v.CompanyName Vendor");
        fieldOrigins.Add("conditionRank", "p.CondtionRank");
        fieldOrigins.Add("onHireTo", "(SELECT TOP 1 c.companyName FROM  PMV_Allocation a " +
            " INNER JOIN CompanyMaster c ON c.companyCode = a.onHireTo  WHERE assetCode = p.AssetCode AND a.currAloc = 1 ORDER BY createdAt DESC ) OnHireTo");
        fieldOrigins.Add("location", "(SELECT TOP 1 location FROM  PMV_Allocation WHERE assetCode = p.AssetCode AND currAloc = 1 ORDER BY createdAt DESC ) Location");
        fieldOrigins.Add("netValue", "p.NetValue");
        fieldOrigins.Add("kmhr", "p.KmHR");
        fieldOrigins.Add("parkingField", "p.ParkingFieldArea");
        fieldOrigins.Add("status", "p.Status");

        //utilities
        var arrayFields = fields.Split(',');
        string concatField = "";

        foreach (var field in arrayFields)
        {
            if (fieldOrigins.ContainsKey(field))
            {
                concatField += $"{fieldOrigins[field]},";
            }
        }

        string query = $"SELECT {concatField.Substring(0, concatField.Length - 1)} " +
                    $"FROM [dbo].[PMV] p " +
                    "LEFT JOIN [PMV_AssetAdditional] aa on aa.assetCode = p.AssetCode " +
                    $"INNER JOIN PMV_Category c ON c.cid = p.cid " +
                    $"LEFT JOIN PRC_Brands b ON b.BrandCode = p.BrandCode " +
                    $"LEFT JOIN [PRC_VendorList] v on v.VendorCode = p.VendorCode " +
                    "WHERE (@category IS NULL OR c.cid in (SELECT Value FROM spSplit(@category))) " +
                    "AND(@subCategory IS NULL OR p.SubCatCode LIKE @subCategory + '%') " +
                    "AND(@assetCode IS NULL OR p.AssetCode LIKE @assetCode + '%') " +
                    "AND(@brand IS NULL OR p.BrandCode LIKE @brand + '%') " +
                    "AND(@companyCode IS NULL OR p.CompanyCode LIKE @companyCode + '%') " +
                    "AND (@status Is NULL OR p.status = @status)";

        var results = await _sqlQuery.DynamicQuery<object>(query, new
        {
            assetCode = !string.IsNullOrEmpty(assetCode) ? assetCode : null,
            category = !string.IsNullOrEmpty(categoryIds) ? categoryIds : null,
            subCategory = !string.IsNullOrEmpty(subCategory) ? subCategory : null,
            brand = !string.IsNullOrEmpty(brand) ? brand : null,
            companyCode = !string.IsNullOrEmpty(companyCode) ? companyCode : null,
            status = !string.IsNullOrEmpty(status) ? status : null
        });

        return results;
    }

    public async Task<InternalAssetResponse> GetInternalForViewing(int assetId)
    {
        var results = await _sqlQuery.DynamicQuery<InternalAssetResponse, ServiceAlertResponse, AssetDocumentResponse, InternalAssetResponse>("sp_ViewAsset",
        new { AssetId = assetId },
        (asset, service, document) =>
        {

            if (service is not null)
            {

                var currentSMUReading = asset.KmHr;

                ServiceAlert serviceDue = new ServiceAlert(Guid.Parse(service.Id), asset.SlNo, service.GroupId,
                    service.Code, service.Name, service.LastSMUReading, currentSMUReading,
                    service.KmAlert, service.KmInterval, "");

                service.Status = serviceDue.Status;
                service.AlertDue = serviceDue.AlertDue;
                service.IntervalDue = serviceDue.IntervalDue;
                service.CurrentSMUReading = currentSMUReading;

                asset.ServiceDues.Add(service);
            }
            if (document is not null)
            {
                asset.Documents.Add(document);
            }

            return asset;
        },
        splitOn: "GroupId,DocumentId",
        commandType: System.Data.CommandType.StoredProcedure);

        if (results is not null && results.Count() > 0)
        {
            var asset = results.FirstOrDefault();
            foreach (var result in results)
            {
                var serviceDue = result.ServiceDues.FirstOrDefault();
                var document = result.Documents.FirstOrDefault();
                if (serviceDue is not null)
                {
                    if (!asset.ServiceDues.Any(s => s.GroupId == serviceDue.GroupId))
                    {
                        asset.ServiceDues.Add(serviceDue);
                    }
                }

                if (document is not null)
                {
                    if (!asset.Documents.Any(d => d.DocumentId == document.DocumentId))
                    {
                        asset.Documents.Add(document);
                    }
                }
            }

            asset.ServiceDues = asset.ServiceDues.OrderBy(s => s.Name).ToList();

            return asset;
        }

        return new();
    }

    public async Task<IEnumerable<InternalAsset>> GetInternals(string assetCode = "",
        string categoryIds = "",
        string subCategory = "",
        string brand = "",
        string companyCode = "",
        string status = "")
    {
        var results = await _sqlQuery.DynamicQuery<InternalAsset, AssetCategory, OperatorDriver, InternalAsset>("sp_GetAssets",
        new
        {
            assetCode = string.IsNullOrEmpty(assetCode) ? null : assetCode,
            category = string.IsNullOrEmpty(categoryIds) ? null : categoryIds,
            subCategory = string.IsNullOrEmpty(subCategory) ? null : subCategory,
            brand = string.IsNullOrEmpty(brand) ? null : brand,
            companyCode = string.IsNullOrEmpty(companyCode) ? null : companyCode,
            status = string.IsNullOrEmpty(status) ? null : status
        },
        (asset, category, assigned) =>
        {

            asset.Category = new AssetCategory { Cid = category.Cid, CatName = category.CatName };
            if (assigned is not null)
            {
                asset.Drivers.Add(new OperatorDriver { EmpCode = assigned.EmpCode, Name = assigned.Name, AssignedAt = assigned.AssignedAt });
            }

            return asset;

        },
         splitOn: "Cid,EmpCode",
         commandType: System.Data.CommandType.StoredProcedure);

        var returnValues = new List<InternalAsset>();

        foreach (var asset in results)
        {

            if (!returnValues.Any(a => a.SlNo == asset.SlNo))
            {
                returnValues.Add(asset);
            }
            else
            {
                var currentAsset = returnValues.Where(s => s.SlNo == asset.SlNo).FirstOrDefault();
                var assigned = asset.Drivers.FirstOrDefault();
                if (assigned != null)
                {
                    if (!currentAsset.Drivers.Any(d => d.Id == assigned.Id))
                    {
                        currentAsset.Drivers.Add(assigned);
                    }
                }
            }
        }

        return returnValues;
    }

    public async Task UpdateInternalFields(Dictionary<string, object> values, string assetCode, string assetType)
    {
        string condition = "";
        var dynParameter = new DynamicParameters();

        foreach (var field in values)
        {
            condition += $"{field.Key}=@{field.Key}";
            dynParameter.Add(field.Key, field.Value);
        }

        dynParameter.Add("assetCode", assetCode);

        string sql = $@"UPDATE PMV SET {condition}
                           WHERE AssetCode=@assetCode";

        await _sqlQuery.DynamicExecute(sql,dynParameter);
        
    }
    public async Task CreateUpdateExternalAssets(ExternalAsset asset)
    {
        await _sqlQuery.DynamicExecute("sp_CreateUpdateExternalAsset",
                new
                {
                    assetCode = asset.AssetCode,
                    assetDesc = asset.AssetDesc,
                    plateType = asset.PlateType,
                    plateNum = asset.PlateNum,
                    vendorCode = asset.VendorCode,
                    companyCode = asset.CompanyCode,
                    hireSub = asset.HireSub,
                    createdBy = asset.CreatedBy,
                    createdAt = asset.CreatedAt,
                },
            commandType: System.Data.CommandType.StoredProcedure);

    }

    public async Task<ExternalAsset> GetExternalAsset(string assetCode)
    {
        var strSQL = @"SELECT 
					        AssetCode,
					        AssetDesc,
					        plateType PlateType, 
					        plateNum PlateNum, 
					        VendorCode,
					        CompanyCode,
					        hireSub HireSub,
					        FuelTankCapacity,
                            createdBy CreatedBy,
                            createdAt CreatedAt
				        FROM  PMV_ExternalAssets
                        WHERE AssetCode = @assetCode";

        var results = await _sqlQuery.DynamicQuery<ExternalAsset>(strSQL,
               new { assetType = "external", assetCode = assetCode });

        return results.FirstOrDefault() ?? new ExternalAsset();
    }

    public async Task<IEnumerable<ExternalAsset>> GetExternalAssets(string plateType = "", string plateNum = "", string hiredUnder = "", string vendor = "", string hireSubContract = "")
    {
        var results = await _sqlQuery.DynamicQuery<ExternalAsset>("sp_GetExternalAssets", new
                            {
                                plateType = string.IsNullOrEmpty(plateType) ? null : plateType,
                                plateNum = string.IsNullOrEmpty(plateNum) ? null : plateNum,
                                hiredUnder = string.IsNullOrEmpty(hiredUnder) ? null : hiredUnder,
                                vendor = string.IsNullOrEmpty(vendor) ? null : vendor,
                                hireSubContract = string.IsNullOrEmpty(hireSubContract) ? null : hireSubContract
                            },
                           commandType: System.Data.CommandType.StoredProcedure);

        return results;
    }

    
}
