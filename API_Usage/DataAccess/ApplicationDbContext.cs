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

    }
}