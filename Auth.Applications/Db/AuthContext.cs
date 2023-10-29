using Auth.Applications.Commons.Interfaces;
using Auth.Core.Menu;
using Auth.Core.Users;
using Microsoft.EntityFrameworkCore;

namespace Auth.Applications.Db;

public class AuthContext : DbContext, IAuthContext
{
    public DbSet<User> Users { get; set; }

    public DbSet<AppUserMenu> AppUserMenu { get; set; }

    public DbSet<AppMenu> AppMenus { get; set; }
    public AuthContext(DbContextOptions<AuthContext> opt) : base(opt)
    {

    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema("HlmAuth");

        //create user relationships
        modelBuilder.Entity<User>()
            .HasMany(u => u.DocTypeAccess)
            .WithOne(p => p.User)
            .HasForeignKey(u => u.UserId);

        //create user projectaccess relationship
        modelBuilder.Entity<User>()
        .HasMany(u => u.ProjectAccess)
        .WithOne(p => p.User)
        .HasForeignKey(u => u.UserId);


        //create user storeaccess relationship
        modelBuilder.Entity<User>()
            .HasMany(u => u.StoreAccess)
            .WithOne(p => p.User)
            .HasForeignKey(u => u.UserId);


        modelBuilder.Entity<AppUserMenu>()
            .HasKey(t => new { t.UserId, t.AppMenuId });

        modelBuilder.Entity<User>()
            .HasMany(u => u.Menus)
            .WithMany(m => m.Users)
            .UsingEntity<AppUserMenu>();


        //user create seed
        modelBuilder.Entity<User>().HasData(
            new User(Guid.NewGuid(), "H22095411", "Arnold Mercado", "PMV", "HUMB", "Programmer", DateTime.Now, true, "amercado@helm.qa", "DOHA@1234")
        );

        modelBuilder.Entity<AppMenu>().HasData(
            new AppMenu("Home", "", "PMV", "/home", "home", 1, true, null),
            new AppMenu("Asset", "", "PMV", "", "asset", 1, true, null),
            new AppMenu("Fuel", "", "PMV", "", "asset", 1, true, null),
            new AppMenu("Timesheet", "", "PMV", "", "timesheet", 1, true, null)
        );
    }


}
