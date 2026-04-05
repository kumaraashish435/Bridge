using System;

namespace Bridge.Backend.Models;

public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Name { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }

    public string Plan { get; set; } = "free";

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation
    public ICollection<File> Files { get; set; }
    public Usage Usage { get; set; }
}
