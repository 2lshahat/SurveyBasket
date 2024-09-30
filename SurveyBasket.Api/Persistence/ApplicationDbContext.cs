using System.Reflection;

namespace SurveyBasket.Api.Persistence;

//using primary Constructor to inject option
public class ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):DbContext(options)
{
    public DbSet<Poll> Polls { get; set; }



    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }


}
