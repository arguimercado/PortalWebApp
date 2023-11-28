using System.Data;

namespace WebApp.Client.Providers.Exports;


public interface IExportObjectManager
{
    Task<ExportResult> GenerateExcel<T>(List<T> results, string fileName);


    Task<ExportResult> GenerateExcelWithDSet(DataSet ds, string fileName);

    Task<ExportResult?> GenerateExcelDynamic(List<dynamic> results, string fileName);
}
public class ExportObjectManager : IExportObjectManager
{
    private readonly IGeneralExport _export;

    public ExportObjectManager(IGeneralExport export)
    {
        _export = export;
    }


    public async Task<ExportResult?> GenerateExcelDynamic(List<dynamic> results, string fileName)
    {
        DataSet ds = new DataSet();
        DataTable dataTable = new DataTable();

        //add properties
        var data = results.ToArray();
        if (data.Length == 0) return null;
        foreach (var pair in (IDictionary<string, object>)data[0])
        {
            dataTable.Columns.Add(pair.Key, (pair.Value ?? string.Empty).GetType());
        }

        foreach (var d in data)
        {
            dataTable.Rows.Add(((IDictionary<string, object>)d).Values.ToArray());
        }

        ds.Tables.Add(dataTable);

        return await _export.GetExcelReport(ds, fileName);
    }


    public async Task<ExportResult> GenerateExcel<T>(List<T> results, string fileName)
    {

        DataSet ds = new DataSet();

        DataTable dataTable = new DataTable();

        //add properties
        if (results.Count > 0)
        {
            foreach (T result in results)
            {
                var type = result!.GetType();
                var propertyInfos = type.GetProperties();

                DataRow dataRow = dataTable.NewRow();

                //store each value
                foreach (var propinfo in propertyInfos)
                {

                    var value = propinfo.GetValue(result);

                    if (!dataTable.Columns.Contains(propinfo.Name))
                    {
                        dataTable.Columns.Add(propinfo.Name);
                    }
                    dataRow[propinfo.Name] = value;
                }

                dataTable.Rows.Add(dataRow);
            }
        }
        else
        {
            dataTable.Columns.Add("Column1");

        }

        ds.Tables.Add(dataTable);
        return await _export.GetExcelReport(ds, fileName);

    }

    public async Task<ExportResult> GenerateExcelWithDSet(DataSet ds, string fileName)
    {
        return await _export.GetExcelReport(ds, fileName);
    }


}
