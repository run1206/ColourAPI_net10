using Microsoft.EntityFrameworkCore;

namespace ColourAPI_net10.Models;

public class ColourContext(DbContextOptions<ColourContext> options) : DbContext(options)
{
    public DbSet<Colour> ColourItems { get; set; }
}
