namespace Mochilog.Models;

public class MochiPhoto
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public required string ImageFileName { get; set; }
    public DateTime UploadedAt { get; set; }
}
