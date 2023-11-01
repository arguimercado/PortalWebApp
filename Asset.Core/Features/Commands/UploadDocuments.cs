using Asset.Core.Contracts.Assets;
using Asset.Core.DTOs.Assets;
using Asset.Core.Models.Assets.Entities;
using WebApp.SharedServer.Utilities.Uploads;

namespace Asset.Core.Features.Commands;

public static class UploadDocuments
{
    public record Command(DocumentRequest AssetDocument) : IRequest<Result<AssetDocumentResponse>>;

    public class CommandHandler : IRequestHandler<Command, Result<AssetDocumentResponse>>
    {
        private readonly DocFileManager _documentUpload;
        private readonly IDocumentDataService _service;

        public CommandHandler(DocFileManager documentUpload, IDocumentDataService service)
        {
            _documentUpload = documentUpload;
            _service = service;
        }

        public async Task<Result<AssetDocumentResponse>> Handle(Command request, CancellationToken cancellationToken)
        {

            try
            {
                FileResult docFileResult = new();

                if (request.AssetDocument.Content != null) {
                    //upload documents in the server
                    docFileResult = await _documentUpload.UploadDocument(request.AssetDocument.Content,"Documents");
                }

                var guid = string.IsNullOrEmpty(request.AssetDocument.Id) ? Guid.NewGuid() : Guid.Parse(request.AssetDocument.Id);

                var assetDoc = AssetDocument.Instance(request.AssetDocument.AssetId, guid,
                        request.AssetDocument.Title ?? "",
                        request.AssetDocument.Description ?? "",
                        request.AssetDocument.DocumentType ?? "",
                        request.AssetDocument.DocumentReferenceNo ?? "",
                        docFileResult.FilePath,
                        docFileResult.FileName);
                
                await _service.CreateDocument(assetDoc);

                return Result.Ok(new AssetDocumentResponse
                {
                    
                    DocumentId = assetDoc.Id,
                    AssetId = assetDoc.AssetId,
                    Title = assetDoc.Title,
                    Description = assetDoc.Description,
                    DocumentType = assetDoc.DocumentType,
                    FileName = assetDoc.FileName,
                    DocumentPath = assetDoc.DocumentPath,
                    DocumentReferenceNo = assetDoc.DocumentReferenceNo
                });
            }
            catch (Exception ex) {
                return Result.Fail(ex.Message);
            }
        }
    }
}
