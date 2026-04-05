using System;

namespace Bridge.Backend.Models;

public class FileRecord
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string FileName { get; set; }
    public string FilePath { get; set; }
    public string Status { get; set; } = "uploaded";
    public string ExtractedJson { get; set; }
}
