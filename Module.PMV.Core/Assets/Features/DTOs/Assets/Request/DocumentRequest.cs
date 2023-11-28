using Microsoft.AspNetCore.Http;

namespace Module.PMV.Core.Assets.Features.DTOs.Assets.Request
{
    public class DocumentRequest
    {
        public int AssetId { get; set; }
        public string? Id { get; set; }
        public string? Title { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public string? DocumentType { get; set; } = string.Empty;
        public string? FileName { get; set; } = string.Empty;
        public string? DocumentPath { get; set; }
        public string? DocumentReferenceNo { get; set; } = string.Empty;
        public IFormFile? Content { get; set; } = null!;
        public string? Tracker { get; set; } = "";
    }
}
