namespace WebApp.Client.Pages.PMV.Fuels.FuelEntry.Models;


public class FuelLogKeyResponse
{
    public string Id { get; set; } = string.Empty;
    public int DocumentNo { get; set; }
}

public class FuelerSelection
{

    public string EmpCode { get; set; } = string.Empty;
    public string EmpName { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
}
