using Microsoft.AspNetCore.Http;

namespace BussinessLayer.DTOs.ModuloCampaña.CmpEmail
{
    public class CmpEmailMessageDto
    {
        public List<string> To { get; set; }

        public List<string> Cc { get; set; }
        public string Subject { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;

        public bool IsHtml { get; set; }
        public List<IFormFile>? Attachments { get; set; }
        public int EmpresaId { get; set; }
        public int ConfiguracionId { get; set; }
    }
    public class CmpEmailAttachmentDto
    {
        public string FileName { get; set; } = string.Empty;
        public byte[] Content { get; set; } = Array.Empty<byte>();
        public string MimeType { get; set; } = "application/octet-stream";
    }
}
