using Auth.Applications.Features.Users.DTO;
using Auth.Applications.Features.Users.Interfaces;

namespace Auth.Applications.Features.Users.Commands
{
    public static class CreateUpdateUserSettings
    {
        //create a command query
        public record Command(UserSettingsDTO UserSettings) : IRequest<Result<Unit>>;

        public class CommandHandler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly IUserManagement _userManagement;
            public CommandHandler(IUserManagement userManagement)
            {
                _userManagement = userManagement;
            }



            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {



                    await _userManagement.UpdateUserSettings(request.UserSettings);
                    return Result.Ok(Unit.Value);
                }
                catch (Exception ex)
                {
                    return Result.Fail(ex.Message);
                }
            }
        }
    }
}
