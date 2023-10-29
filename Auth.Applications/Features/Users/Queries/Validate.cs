using Auth.Applications.Commons;
using Auth.Applications.Features.Users.Interfaces;

namespace Auth.Applications.Features.Users.Queries;
public static class Validate
{
    public record Query(string EmpCode) : IRequest<Result<AuthResult>>;

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
                var authResult = await _authManagement.ValidateThruPortal(request.EmpCode);
                if (authResult is null) {
                    throw new Exception("User not registered");
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
