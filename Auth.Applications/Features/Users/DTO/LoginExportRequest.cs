using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Applications.Features.Users.DTO
{
    public class LoginExportRequest
    {
        public string UserName { get; set; } = string.Empty;
        public string? Password { get; set; }

        public int Valid { get; set; }

        public string Sql { get; set; } = string.Empty;
    }
}
