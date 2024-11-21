using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

public class ESGContext : DbContext
{
    public ESGContext(DbContextOptions<ESGContext> options) : base(options) { }

    public DbSet<Edata> Envi { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Edata>().HasKey(e => e.Environmentid);
    }
}

public class Edata
{
    [Key]
    public int Environmentid { get; set; }
    public double TotalEnergy { get; set; }
    public double CrudeFuel { get; set; }
    public double GasFuel { get; set; }
    public double PurchElec { get; set; }
    public DateTime Year { get; set; }
}
