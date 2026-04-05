using System;

namespace Bridge.Backend.Models;

public class LineItem
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid ExtractedDataId { get; set; }
    public ExtractedData ExtractedData { get; set; }

    public string ItemName { get; set; }
    public int? Quantity { get; set; }
    public decimal? Price { get; set; }
    public decimal? Total { get; set; }
}
