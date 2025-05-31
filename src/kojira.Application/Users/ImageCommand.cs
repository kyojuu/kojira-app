using kojira.Application.Abstractions.Messaging;
using Microsoft.AspNetCore.Http;

namespace kojira.Application.Users;

internal sealed class ImageCommand : ICommand
{
    public IFormFile File { get; set; }
    public string FileName { get; set; }
    public string FilePath { get; set; }
}
