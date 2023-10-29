using Auth.Applications.Menus.DTO;
using Auth.Applications.Menus.Interfaces;

namespace Auth.Applications.Menus;

public static class CreateMenu
{
    public record Command(MenuRegister Menu) : IRequest<Result<Unit>>;

    public class CommandHandler : IRequestHandler<Command, Result<Unit>>
    {
        private readonly IMenuManagement _menu;

        public CommandHandler(IMenuManagement menu)
        {
            _menu = menu;
        }
        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            try {
                await _menu.Create(request.Menu);
                return Result.Ok(Unit.Value);
            }
            catch(Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }
    }

}
