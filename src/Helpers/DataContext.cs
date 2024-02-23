namespace WebApi.Helpers;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WebApi.Entities;

public class DataContext : DbContext
{
    public DbSet<Account> Accounts { get; set; }  
    public DbSet<Devices> GetDevice { get; set; }
   
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(connectionString:
           $"Host={_appSettings.Server}; Database={_appSettings.Database}; Username={_appSettings.UserId}; Password={_appSettings.Password}; Timeout=300; CommandTimeout=300"); 
        base.OnConfiguring(optionsBuilder);
    }

        private AppSettings _appSettings;

    public DataContext(IOptions<AppSettings> appSettings)
    {
        _appSettings = appSettings.Value;
    }
}