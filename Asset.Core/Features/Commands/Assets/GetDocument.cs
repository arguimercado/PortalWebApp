using Asset.Core.Contracts.Assets;
using WebApp.SharedServer.Utilities.Uploads;

namespace Asset.Core.Features.Commands.Assets;

public static class GetDocument
{

    public record Query(string DocumentId) : IRequest<Result<FileDownloadResult>>;

    public class QueryHandler : IRequestHandler<Query, Result<FileDownloadResult>>
    {
        private readonly IDocumentDataService _dataService;
        private readonly DocFileManager _documentUpload;

        public QueryHandler(IDocumentDataService dataService, DocFileManager documentUpload)
        {
            _dataService = dataService;
            _documentUpload = documentUpload;
        }
        public async Task<Result<FileDownloadResult>> Handle(Query request, CancellationToken cancellationToken)
        {
            try
            {

                var assetDocument = await _dataService.GetAssetDocument(request.DocumentId);

                if (assetDocument is null)
                    throw new Exception("Document is not exist or already removed");

                var result = await _documentUpload.DownloadFile(assetDocument.FileName, assetDocument.DocumentPath);

                return Result.Ok(result);

            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }
    }
}
