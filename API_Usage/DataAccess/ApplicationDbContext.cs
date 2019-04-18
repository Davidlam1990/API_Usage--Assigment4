using Microsoft.EntityFrameworkCore;
using API_Usage.Models;

namespace API_Usage.DataAccess
{
  public class ApplicationDbContext : DbContext
  {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Company> Companies { get; set; }
    public DbSet<Equity> Equities { get; set; }
    public DbSet<Trade> Trades { get; set; }
    public DbSet<EffectiveSpread> EffectiveSpreads { get; set; }
    public DbSet<Watchlist> Watchlists{ get; set; }
    public DbSet<Dividend> Dividends { get; set; }
    public DbSet<News> News { get; set; }
    public DbSet<TopGainer> TopGainers { get; set; }
    public DbSet<TopLoser> TopLosers { get; set; }
    public DbSet<MostActive> MostActives { get; set; }
    public DbSet<Logo> Logos { get; set; }
    public DbSet<CompanyDetail> CompanyDetails { get; set; }

    }
}