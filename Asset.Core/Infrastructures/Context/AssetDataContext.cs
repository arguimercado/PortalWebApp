using Asset.Core.Models.GroupAlerts;
using Microsoft.EntityFrameworkCore;

namespace Asset.Core.Infrastructures.Context;

public class AssetDataContext : DbContext
{
    public DbSet<GroupAlert> Alerts { get; set; }
    public AssetDataContext(DbContextOptions<AssetDataContext> options) : base(options)
    {
        
    }

    
}
