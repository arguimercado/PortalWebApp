namespace WebApp.Client.Providers.Exports;

public class ExportResult
{
  
    public ExportResult(byte[] rawData, string fileName, DateTime dateCreated, string contentType)
    {
        RawData = rawData;
        FileName = fileName;
        DateCreated = dateCreated;
        ContentType = contentType;
    }

    public byte[] RawData { get; }
    public string FileName { get; }
    public DateTime DateCreated { get; }
    public string ContentType { get; }
}
