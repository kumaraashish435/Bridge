using System;

namespace Bridge.Backend.Models;

public class Usage
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid UserId { get; set; }
    public User User { get; set; }

    public int TotalUploads { get; set; } = 0;
    public int CurrentMonthUploads { get; set; } = 0;

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
