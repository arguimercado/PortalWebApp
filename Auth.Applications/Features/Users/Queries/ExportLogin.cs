using Auth.Applications.Commons;
using Auth.Applications.Features.Users.Interfaces;

namespace Auth.Applications.Features.Users.Queries;

public record ExportLoginQuery(string EmpCode,int Valid) : IRequest<Result<AuthResult>>;

public class ExportLoginQueryHandler : IRequestHandler<ExportLoginQuery, Result<AuthResult>>
{
    private readonly IAuthManagement _authManagement;

    public ExportLoginQueryHandler(IAuthManagement authManagement)
    {
        _authManagement = authManagement;
    }

    public async Task<Result<AuthResult>> Handle(ExportLoginQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var authResult = await _authManagement.ExportLogin(request.EmpCode,request.Valid);
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



