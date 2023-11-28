using Module.PMV.Core.Assets.Models.Employees;

namespace Module.PMV.Core.Assets.Contracts.Commons;

public interface IEmployeeDataService
{
    Task<IEnumerable<Employee>> GetActiveEmployees();

    Task<Employee> GetEmployee(string EmpCode);
}
