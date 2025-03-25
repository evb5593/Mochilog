namespace Mochilog.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

public class MochiPhoto
{
    public int Id { get; set; }

    [Required]
    public string Title { get; set; } = string.Empty;

    [Display(Name = "Date Picture Was Taken")]
    [DataType(DataType.Date)]
    public DateTime PicTakenDate { get; set; }

    [Display(Name = "Date Uploaded")]
    public DateTime UploadDate { get; set; } = DateTime.Now;
    public byte[]? ImageData { get; set; }
}
