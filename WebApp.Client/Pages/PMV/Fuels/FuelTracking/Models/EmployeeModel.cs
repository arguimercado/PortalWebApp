namespace WebApp.Client.Pages.PMV.Fuels.FuelTracking.Models;

public class EmployeeModel
{
    public string EmpCode { get; set; } = string.Empty;
    public string EmpName { get; set; } = string.Empty;
    public string EmpPasswd { get; set; } = string.Empty;
    public string RPNo { get; set; } = string.Empty;
    public string Designation { get; set; } = string.Empty;
    public string MobilePhone { get; set; } = string.Empty;
    public string LocationOfWork { get; set; } = string.Empty;
    public string VisaDesignation { get; set; } = string.Empty;
    public string? PresentEmail { get; set; }
    public string FullName => $"{EmpCode} - {EmpName}";
    public string Photo { get; set; }
}
