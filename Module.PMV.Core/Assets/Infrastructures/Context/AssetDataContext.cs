using Microsoft.EntityFrameworkCore;
using Module.PMV.Core.Assets.Models.GroupAlerts;

namespace Module.PMV.Core.Assets.Infrastructures.Context;

public class AssetDataContext : DbContext
{
    public DbSet<GroupAlert> Alerts { get; set; }
    public AssetDataContext(DbContextOptions<AssetDataContext> options) : base(options)
    {

    }


}
