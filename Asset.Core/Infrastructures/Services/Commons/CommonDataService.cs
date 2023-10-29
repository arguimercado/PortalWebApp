using Applications.Interfaces;
using Asset.Core.Contracts.Commons;
using WebApp.SharedServer.Commons;

namespace Asset.Core.Infrastructures.Services.Commons;

public class CommonDataService : ICommonDataService
{
    private readonly ISqlQuery _sqlQuery;

    public CommonDataService(ISqlQuery sqlQuery)
    {
        _sqlQuery = sqlQuery;
    }

    public async Task<IEnumerable<SelectItem>> GetSelections(string type)
    {
        var strSQL = "";
        if (type == "statuses")
        {
            var statuses = new List<SelectItem> {
                new SelectItem { Value = "1",Text = "Active"},
                new SelectItem { Value = "2",Text = "Sold"},
                new SelectItem { Value = "3",Text = "Traded-In"},
                new SelectItem { Value = "4",Text = "Scrapped"}
            };

            return await Task.FromResult(statuses);
        }
        else if (type == "companies")
        {
            strSQL = "SELECT CompanyCode Value,companyName Text FROM [dbo].[CompanyMaster] ORDER BY companyName";
        }
        else if (type == "brands")
        {
            strSQL = "SELECT BrandCode Value,BrandName Text FROM [dbo].[PRC_Brands] WHERE pmvFlag = 1 ORDER BY BrandName";
        }
        else if (type == "plateTypes")
        {
            strSQL = "SELECT Distinct plateType Value,plateType Text from  PMV_ExternalAssets order by plateType";
        }
        else if (type == "hireunder")
        {
            strSQL = "select distinct p.CompanyCode Value, c.companyName Text from PMV_ExternalAssets p ,CompanyMaster c where p.CompanyCode=c.companyCode order by c.companyName";
        }
        else if (type == "vendors")
        {
            strSQL = "select distinct p.VendorCode Value, v.CompanyName as Text from PMV_ExternalAssets p ,PRC_VendorList v where p.VendorCode=v.VendorCode order by v.CompanyName";
        }
        else if (type == "vendorlist")
        {
            strSQL = "select VendorCode Value, CompanyName Text from PRC_VendorList order by CompanyName";
        }
        else if (type == "accounts")
        {
            strSQL = "select categoryCode Value,categoryName Text from PMV_AccountCategory order by CategoryName ";
        }
        else if (type == "categories")
        {
            strSQL = "SELECT cid Value,cname Text FROM [dbo].[PMV_Category] ORDER BY cname";
        }
        else if (type == "subcategories")
        {
            strSQL = "SELECT SubCatCode Value, SubCatName Text,cid Type FROM PMV_SubCategory ORDER BY SubCatCode";
        }
        else if (type == "hire")
        {
            var hireMethods = new List<SelectItem> {
                new SelectItem { Value = "Hire",Text = "Hire"},
                new SelectItem { Value = "SubContract",Text = "SubContract"},
            };

            return await Task.FromResult(hireMethods);
        }
        else if (type == "assetTypes")
        {
            var assetTypes = new List<SelectItem> {
                new SelectItem { Value = "PMV", Text = "PMV" },
                new SelectItem { Value = "NONPMV", Text = "NON-PMV" },
            };

            return await Task.FromResult(assetTypes);
        }
        else if (type == "rentownes")
        {
            var rentOwned = new List<SelectItem> {
                new SelectItem { Value = "#N/A", Text = "#N/A" },
                new SelectItem { Value = "OWNED", Text = "OWNED" },
                new SelectItem { Value = "RENTABLE", Text = "RENTABLE" },
            };

            return await Task.FromResult(rentOwned);
        }
        else if (type == "hiremethods")
        {
            var hireMethods = new List<SelectItem> {
                new SelectItem { Value = "Hourly", Text = "Hourly" },
                new SelectItem { Value = "Monthly", Text = "Monthly" },
            };

            return await Task.FromResult(hireMethods);
        }

        var results = await _sqlQuery.DynamicQuery<SelectItem>(strSQL, null);

        return results;
    }
}
