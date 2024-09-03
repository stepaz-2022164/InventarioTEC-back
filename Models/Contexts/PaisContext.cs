using Microsoft.EntityFrameworkCore;

namespace GestorInventario.Models.Contexts
{
    public class PaisContext : DbContext
    {
        public PaisContext(DbContextOptions<PaisContext>options) : base(options)
        {
        }

        public DbSet<Pais> Paises {get; set;}

        //
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Pais>();
        }
    }
}