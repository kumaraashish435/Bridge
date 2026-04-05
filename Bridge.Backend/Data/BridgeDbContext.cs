
using Bridge.Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Bridge.Backend.Data;

public class BridgeDbContext : DbContext
{
    public BridgeDbContext(DbContextOptions<BridgeDbContext> options) : base(options)
    {
    }

    // public DbSet<Models.User> Users { get; set; }
    // public DbSet<Models.File> Files { get; set; }
    // public DbSet<Models.ExtractedData> ExtractedData { get; set; }
    // public DbSet<Models.LineItem> LineItems { get; set; }
    // public DbSet<Models.Usage> Usage { get; set; }
    // public DbSet<Models.RefreshToken> RefreshTokens { get; set; }
    // public DbSet<Models.Export> Exports { get; set; }   

    public DbSet<FileRecord> Files { get; set; }

}
