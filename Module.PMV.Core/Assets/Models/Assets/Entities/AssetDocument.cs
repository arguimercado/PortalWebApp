namespace Module.PMV.Core.Assets.Models.Assets.Entities;

public class AssetDocument : Entity<Guid>
{
    public static AssetDocument Instance(
        int assetId,
        Guid id,
        string title,
        string description,
        string documentType,
        string documentReferenceNo,
        string documentPath,
        string fileName)
        => new(id, assetId, title, description, documentType, documentReferenceNo, documentPath, fileName);

    private AssetDocument() : base(Guid.NewGuid())
    {

    }
    private AssetDocument(Guid id, int assetId, string title, string description, string documentType, string documentReferenceNo, string documentPath, string fileName) : base(id)
    {
        DocumentType = documentType;
        DocumentReferenceNo = documentReferenceNo;
        DocumentPath = documentPath;
        AssetId = assetId;
        FileName = fileName;
        Title = title;
        DocumentType = documentType;
        Description = description;
    }

    public int AssetId { get; set; }
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public string DocumentType { get; set; } = "";
    public string FileName { get; set; } = "";
    public string? DocumentReferenceNo { get; set; }
    public string DocumentPath { get; set; } = "";

    public void Update(string title, string description, string documentType, string documentReferenceNo, string documentPath, string fileName)
    {

    }


}
