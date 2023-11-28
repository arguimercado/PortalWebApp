using Module.PMV.Core.Assets.Contracts.Assets;

namespace Module.PMV.Core.Assets.Features.Commands.Assets;

public static class DeleteDocument
{
    public record Command(string DocumentId) : IRequest<Result<Unit>>;

    public class CommandHandler : IRequestHandler<Command, Result<Unit>>
    {
        private readonly IDocumentDataService _documentService;

        public CommandHandler(IDocumentDataService documentService)
        {
            _documentService = documentService;
        }
        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            try
            {
                await _documentService.DeleteDocument(request.DocumentId);
                return Result.Ok(Unit.Value);
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }
    }
}
