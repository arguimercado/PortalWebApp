using Asset.Core.Contracts.Assets;
using Asset.Core.Models.Assets.Entities;

namespace Asset.Core.Infrastructures.Services.Assets;

internal sealed class DocumentDataService : IDocumentDataService
{
    private readonly ISqlQuery _sqlQuery;

    public DocumentDataService(ISqlQuery sqlQuery)
    {
        _sqlQuery = sqlQuery;
    }
    public async Task CreateDocument(AssetDocument document)
    {
        var sql = @"INSERT INTO HLMPMV.AssetDocument 
                    (Id,AssetId,Title,Description,DocumentType,FileName,DocumentReferenceNo,DocumentPath) 
                    VALUES (@id,@assetId,@title,@description, @documentType,@fileName,@docRefNo,@docPath)";

        await _sqlQuery.DynamicExecute(sql, new
        {
            id = document.Id,
            assetId = document.AssetId,
            title = document.Title,
            description = document.Description,
            documentType = document.DocumentType,
            fileName = document.FileName,
            docRefNo = document.DocumentReferenceNo,
            docPath = document.DocumentPath,
        });
    }

    public Task DeleteDocument(string Id)
    {
        throw new NotImplementedException();
    }

    public async Task<AssetDocument?> GetAssetDocument(string documentId)
    {
        var sql = "SELECT * FROM HLMPMV.AssetDocument WHERE Id=@documentId";
        var results = await _sqlQuery.DynamicQuery<AssetDocument>(sql,new {documentId = documentId});

        return results.FirstOrDefault();
    }

    public async Task UpdateDocument(AssetDocument document)
    {
        var sql = "UPDATE HLMPMV.AssetDocument " +
                  "SET Title = @title," +
                  "Description = @description, " +
                  "DocumentType = @documentType " +
                  "WHERE Id=@id";
        await _sqlQuery.DynamicExecute(sql, new
        {
            title = document.Title,
            description = document.Description,
            documentType = document.DocumentType,
            id = document.Id
        });

    }
}
