namespace WebApp.SharedServer.Utilities.Uploads;

public class FileResult
{
    public string FilePath { get; set; } = string.Empty;
    public string FileName { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string FullFileName => $"{FileName}";
}

public class FileDownloadResult
{
    public string FileName { get; set; } = "";
    public byte[]? DocumentFile { get; set; } 
    public string ContentType { get; set; } = "";
    public int Size => DocumentFile is not null ? DocumentFile.Length : 0;
}
