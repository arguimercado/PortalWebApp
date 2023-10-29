using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using System.Text.RegularExpressions;

namespace WebApp.SharedServer.Utilities.Uploads;

public class DocFileManager
{
    private readonly FileOption _docOption;

    public DocFileManager(FileOption docOption)
    {
        _docOption = docOption;
    }

    public async Task<bool> DeleteFileAsync(string fileName)
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(), _docOption.Document, fileName);
        if (File.Exists(path))
        {
            File.Delete(path);
        }

        return await Task.FromResult(true);
    }

    public async Task<FileDownloadResult> GetEmployeeImage(string empCode)
    {

        var result = new FileDownloadResult();
        string[] extensions = new string[] { "jpg", "jpeg", "png" };
        string path = "";

        foreach (var extension in extensions)
        {
            path = Path.Combine(_docOption.PortalDirectory, $"{empCode}.{extension}");
            if (File.Exists(path))
                break;
        }
        var provider = new FileExtensionContentTypeProvider();
        if (!provider.TryGetContentType(path, out var contentType))
        {
            contentType = "application/octet-stream";
        }

        result.DocumentFile = await File.ReadAllBytesAsync(path);
        result.ContentType = contentType;

        return result;

    }

    public async Task<FileDownloadResult> DownloadFile(string fileName, string filePath)
    {
        try
        {
            var result = new FileDownloadResult();
            var path = Path.Combine(Directory.GetCurrentDirectory(), _docOption.Document, filePath);

            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(path, out var contentType))
            {
                contentType = "application/octet-stream";
            }

            result.DocumentFile = await File.ReadAllBytesAsync(path);
            result.ContentType = contentType;



            return result;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }

    //convert the string containing image data and return stream;
    public Stream GenerateStreamFromString(string content)
    {

        MemoryStream stream = new MemoryStream();
        StreamWriter writer = new StreamWriter(stream);
        writer.Write(content);
        writer.Flush();
        stream.Position = 0;
        return stream;
    }

    public async Task<FileResult> UploadFile(Stream content, string fileName, string directoryName)
    {

        //write file from stream 
        var path = Path.Combine(Directory.GetCurrentDirectory(), directoryName, fileName);

        using (var fileStream = new FileStream(path, FileMode.Create, FileAccess.Write))
        {
            await content.CopyToAsync(fileStream);
        }

        return new FileResult
        {
            FilePath = $"{fileName}",
            FileName = fileName,
            Type = "jpg/png"
        };

    }

    public async Task<FileDownloadResult> ViewImage(string fileName)
    {

        var path = Path.Combine(Directory.GetCurrentDirectory(), _docOption.FuelImage, fileName);
        if (!File.Exists(path))
        {
            throw new FileNotFoundException();
        }

        byte[] b = File.ReadAllBytes(path);

        return await Task.FromResult(new FileDownloadResult { DocumentFile = b, });

    }


    public async Task<FileResult> UploadFile(IFormFile formFile)
    {
        try
        {

            long size = formFile.Length;
            if (size <= 0) throw new FileNotFoundException(nameof(formFile));


            string fileName = formFile.FileName;

            var path = Path.Combine(Directory.GetCurrentDirectory(), _docOption.Document, fileName);

            await CopyStream(formFile, path);

            return new FileResult
            {
                FilePath = $"{formFile.FileName}",
                FileName = formFile.FileName,
                Type = formFile.ContentType
            };
        }
        catch (Exception ex)
        {
            throw new Exception();
        }

    }

    public async Task<FileResult> UploadFileImage(string content, string fileName)
    {
        var base64Data = Regex.Match(content, @"data:image/(?<type>.+?),(?<data>.+)").Groups["data"].Value;
        var binData = Convert.FromBase64String(base64Data);
        var path = Path.Combine(Directory.GetCurrentDirectory(), _docOption.FuelImage, fileName);
        File.WriteAllBytes(path, binData);

        return await Task.FromResult(new FileResult
        {
            FileName = fileName,
            FilePath = fileName,
            Type = "jpg;png"
        });
    }


    public async Task CopyStream(IFormFile file, string path)
    {
        using (var fileStream = new FileStream(path, FileMode.Create, FileAccess.Write))
        {
            await file.CopyToAsync(fileStream);
        }
    }
}
