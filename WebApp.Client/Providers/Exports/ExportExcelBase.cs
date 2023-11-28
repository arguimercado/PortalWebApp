using OfficeOpenXml;
using System.Data;

namespace WebApp.Client.Providers.Exports;


public abstract class ExcelExportBase<T> 
{
    public static string ExcelContentType => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

    protected abstract DataTable ListToDataTable(List<T> results);

    public async Task<byte[]> ExportExcel(List<T> data)
    {
        return await ExportExcel(ListToDataTable(data), false);
    }

    public async Task<byte[]?> ExportExcelDataset(DataTable dataTable, bool showHeader = false)
    {
        return await ExportExcel(dataTable, showHeader);
    }


    protected async Task<byte[]> ExportExcel(
        DataTable dataTable,
        bool heading,
        string headerName = "Import")
    {

        byte[] result = null;

        using (ExcelPackage package = new ExcelPackage())
        {

            ExcelWorksheet workSheet = package.Workbook.Worksheets.Add(string.Format(headerName, heading));

            int startRowFrom = heading ? 2 : 1;

            var totalRow = dataTable.Rows.Count;

            workSheet.Cells["A" + startRowFrom].LoadFromDataTable(dataTable, true);

            result = await package.GetAsByteArrayAsync();

            return result;
        }
    }
}
