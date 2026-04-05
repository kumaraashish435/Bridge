using System;

namespace Bridge.Backend.Models;

public class File
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid UserId { get; set; }
    public User User { get; set; }

    public string FileName { get; set; }
    public string FilePath { get; set; }

    public string Status { get; set; } = "uploaded"; 
    // uploaded, processing, completed, failed

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation
    public ExtractedData ExtractedData { get; set; }
    public ICollection<Export> Exports { get; set; }

}
