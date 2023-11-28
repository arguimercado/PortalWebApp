using Module.PMV.Core.Assets.Models.Assets.Entities;

namespace Module.PMV.Core.Assets.Contracts.Assets;

public interface IDocumentDataService
{
    Task CreateDocument(AssetDocument document);
    Task DeleteDocument(string Id);
    Task UpdateDocument(AssetDocument document);
    Task<AssetDocument?> GetAssetDocument(string documentId);
}
