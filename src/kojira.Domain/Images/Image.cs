using Microsoft.AspNetCore.Http;

namespace kojira.Domain.Images;

public sealed class Image
{
    public Guid Id { get; set; }

    public IFormFile File { get; set; }
    public string FileName { get; set; }
    public string FileExtension { get; set; }
    public string FileDescription { get; set; }
    public long FileSizeInBytes { get; set; }
    public string FilePath { get; set; }
}
