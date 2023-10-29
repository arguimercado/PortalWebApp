using Auth.Applications.Commons;
using Auth.Applications.Features.Users.Interfaces;

namespace Auth.Applications.Features.Users.Queries;

public class Login
{
    public record Query(string EmpCode, string Password, string? Module = "") : IRequest<Result<AuthResult>>;

    public class QueryHandler : IRequestHandler<Query, Result<AuthResult>>
    {
        private readonly IAuthManagement _authManagement;

        public QueryHandler(IAuthManagement authManagement)
        {
            _authManagement = authManagement;
        }

        public async Task<Result<AuthResult>> Handle(Query request, CancellationToken cancellationToken)
        {
            try
            {
                var authResult = await _authManagement.Login(request.EmpCode, request.Password,request.Module ?? "pmv");
                if (authResult is null) {
                    throw new Exception("Password is incorrect or user not found");
                }
                return Result.Ok(authResult);
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }

        }
    }
}
