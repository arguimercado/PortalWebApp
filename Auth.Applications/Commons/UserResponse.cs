using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.Applications.Commons;

public class UserResponse
{
    public string EmployeeCode { get; set; }
    public string EmployeeName { get; set; }
    public string Token { get; set; }
}