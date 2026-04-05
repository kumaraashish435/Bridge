using System;

namespace Bridge.Backend.Models;

public class Export
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid FileId { get; set; }
    public File File { get; set; }

    public string ExportType { get; set; } // excel, json
    public string FilePath { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

}
