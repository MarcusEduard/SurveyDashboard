using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

public class DNDContext : DbContext
{
    public DNDContext(DbContextOptions<DNDContext> options) : base(options) { }

    public DbSet<SurveyResponse> SurveyResponses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SurveyResponse>().HasKey(s => s.SurveyResponseId);
    }
}
