using Asset.Core.Models.Assets.Entities;

namespace Asset.Core.Contracts.Assets;

public interface IDocumentDataService
{
    Task CreateDocument(AssetDocument document);
    Task DeleteDocument(string Id);
    Task UpdateDocument(AssetDocument document);
    Task<AssetDocument?> GetAssetDocument(string documentId);
}
