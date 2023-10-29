namespace WebApp.Client.Pages.PMV.Assets.Models;

public class AssignedOperatorModel
{
    public string EmpCode { get; set; } = string.Empty;
    public string EmpName { get; set; } = string.Empty;
    public string RPNo { get; set; } = string.Empty;
    public string MobilePhone { get; set; } = string.Empty;
    public string Company { get; set; } = string.Empty;
    public string Photo { get; set; } = string.Empty;
}

public class AssignedAssetModel
{
    public string AssetCode { get; set; } = string.Empty;
    public string AssetDesc { get; set; } = string.Empty;
    public string PlateNo { get; set; } = string.Empty;

    public IEnumerable<AssignedOperatorModel> Operators { get; set; } = new List<AssignedOperatorModel>();

}
