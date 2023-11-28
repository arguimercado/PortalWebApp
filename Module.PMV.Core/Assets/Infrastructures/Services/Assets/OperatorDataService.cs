using Module.PMV.Core.Assets.Contracts.Assets;
using Module.PMV.Core.Assets.Models.Assets.Entities;

namespace Module.PMV.Core.Assets.Infrastructures.Services.Assets;

internal sealed class OperatorDataService : IOperatorDataService
{
    private readonly ISqlQuery _sqlQuery;

    public OperatorDataService(ISqlQuery sqlQuery)
    {
        _sqlQuery = sqlQuery;
    }
    public Task<OperatorDriver?> FindOperatorAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<OperatorDriver>> GetOperators(string assetCode)
    {
        var sql = "SELECT " +
                    "a.assetId Id, " +
                    "a.assetCode AssetCode," +
                    "a.assetTypeCode AssetTypeCode, " +
                    "a.division Division, " +
                    "a.brandCode BrandCode, " +
                    "a.EmpType, " +
                    "a.EmpCode, " +
                    "a.EmpName, " +
                    "a.RPNo RPNo," +
                    "a.MobileNo," +
                    "a.Company, " +
                    "a.assetLocation AssetLocation," +
                    "a.vendorCode VendorCode," +
                    "a.Remarks," +
                    "a.dcsSlno DcsSlNo," +
                    "intExt InternalExternal," +
                    "assignedAt AssignedAt," +
                    "returnedAt ReturnedAt " +
            "FROM EmpAssets a " +
            "WHERE assetCode = @assetCode ";

        var results = await _sqlQuery.DynamicQuery<OperatorDriver>(sql, new { assetCode });
        return results;
    }

    public async Task SaveOperator(OperatorDriver driver)
    {

        DynamicParameters prmts = new DynamicParameters();
        prmts.Add("@empType", driver.EmpType);
        prmts.Add("@empCode", driver.EmpCode);
        prmts.Add("@empName", driver.EmpName);
        prmts.Add("@rpNo", driver.RPNo);
        prmts.Add("@company", driver.Company);
        prmts.Add("@mobileNo", driver.MobileNo);
        prmts.Add("@assetId", driver.Id);
        prmts.Add("@assetCode", driver.AssetCode);
        prmts.Add("@assetTypeCode", driver.AssetTypeCode);
        prmts.Add("@division", driver.Division);
        prmts.Add("@brandCode", driver.BrandCode);
        prmts.Add("@assetLocation", driver.AssetLocation);
        prmts.Add("@vendorCode", driver.VendorCode);
        prmts.Add("@createdBy", driver.CreatedBy);
        prmts.Add("@assignedAt", driver.AssignedAt);
        prmts.Add("@returnedAt", driver.ReturnedAt);
        prmts.Add("@dcsSlno", driver.DcsSlNo);

        await _sqlQuery.DynamicExecute("sp_CreateEmployeeAsset", prmts,
           commandType: CommandType.StoredProcedure);

    }
}
