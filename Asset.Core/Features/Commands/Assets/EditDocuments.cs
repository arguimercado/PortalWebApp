﻿using Asset.Core.Contracts.Assets;
using Asset.Core.DTOs.Assets;
using WebApp.SharedServer.Utilities.Uploads;

namespace Asset.Core.Features.Commands.Assets;

public static class EditDocuments
{
    public record Query(DocumentRequest Request, string Id) : IRequest<Result<Unit>>;

    public class QueryHandler : IRequestHandler<Query, Result<Unit>>
    {
        private readonly DocFileManager _documentUpload;
        private readonly IDocumentDataService _service;

        public QueryHandler(DocFileManager documentUpload, IDocumentDataService service)
        {
            _documentUpload = documentUpload;
            _service = service;
        }

        public async Task<Result<Unit>> Handle(Query request, CancellationToken cancellationToken)
        {
            try
            {
                var assetDocument = await _service.GetAssetDocument(request.Id);
                FileResult docFileResult = new();

                if (request.Request.Content != null)
                {
                    //upload documents in the server
                    docFileResult = await _documentUpload.UploadDocument(request.Request.Content, "Documents");
                }

                assetDocument.Update(
                    request.Request.Title ?? "",
                    request.Request.Description ?? "",
                    request.Request.DocumentType ?? "",
                    request.Request.DocumentReferenceNo ?? "",
                    docFileResult.FilePath, request.Request.FileName ?? "");

                await _service.UpdateDocument(assetDocument);

                return Result.Ok(Unit.Value);
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }

        }
    }
}
