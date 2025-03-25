using Microsoft.EntityFrameworkCore;
using Mochilog.Models;

namespace Mochilog.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<MochiPhoto> MochiPhotos { get; set; }
}
