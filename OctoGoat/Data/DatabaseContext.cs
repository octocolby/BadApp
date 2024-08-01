using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OctoGoat.Models;

namespace OctoGoat.Data;

//public class DatabaseContext : DbContext
public class DatabaseContext : IdentityDbContext<IdentityUser>
{
    //public DbSet<IdentityUser> IdentityUsers { get; set; }
    public DbSet<TweeterModel> TweeterModels { get; set; }
    public DbSet<CheckmarkModel> CheckmarkModels { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Filename=MyDatabase.db");
    }
}
