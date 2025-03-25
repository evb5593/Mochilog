using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Mochilog.Models;

namespace Mochilog.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Mochilog.Models.MochiPhoto> MochiPhoto { get; set; } = default!;
}
