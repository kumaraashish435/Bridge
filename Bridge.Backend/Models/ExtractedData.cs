using System;

namespace Bridge.Backend.Models;

public class ExtractedData
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid FileId { get; set; }
    public File File { get; set; }

    public string InvoiceNumber { get; set; }
    public DateTime? InvoiceDate { get; set; }
    public string VendorName { get; set; }
    public decimal? TotalAmount { get; set; }

    public string RawJson { get; set; } // JSONB in DB

    public double ConfidenceScore { get; set; } = 0.0;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation
    public ICollection<LineItem> LineItems { get; set; }
}
