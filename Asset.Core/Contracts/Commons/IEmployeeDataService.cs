using Asset.Core.Models.Employees;

namespace Asset.Core.Contracts.Commons;

public interface IEmployeeDataService
{
    Task<IEnumerable<Employee>> GetActiveEmployees();

    Task<Employee> GetEmployee(string EmpCode);
}
