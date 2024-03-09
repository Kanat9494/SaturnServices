namespace SaturnServices.Data;

public class SaturnDBContext : DbContext
{
    public SaturnDBContext(DbContextOptions<SaturnDBContext> options) : base(options) { }
    public DbSet<User> Users { get; set; }
}
