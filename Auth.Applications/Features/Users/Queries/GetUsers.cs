using Auth.Applications.Commons.Interfaces;
using Auth.Applications.Features.Users.DTO;
using Microsoft.EntityFrameworkCore;

namespace Auth.Applications.Features.Users.Queries
{
    public class GetUsers
    {
        public record Query : IRequest<Result<IEnumerable<UserResult>>>;


        public class QueryHandler : IRequestHandler<Query, Result<IEnumerable<UserResult>>>
        {
            private readonly IAuthContext _context;
            public QueryHandler(IAuthContext context)
            {
                _context = context;
            }
            public async Task<Result<IEnumerable<UserResult>>> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    IEnumerable<UserResult> result = await _context.Users
                                                        .Select(u => new UserResult
                                                        {
                                                            Id = u.Id.ToString(),
                                                            UserName = u.UserName,
                                                            FullName = u.FullName,
                                                            Company = u.Company,
                                                            Designation = u.Designation,
                                                            DateJoined = u.DateJoined,
                                                            Location = u.Location,
                                                        }).ToListAsync();

                    return Result.Ok(result);
                }
                catch (Exception ex)
                {
                    return Result.Fail(ex.Message);
                }
            }
        }
    }
}
