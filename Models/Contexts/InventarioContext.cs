using Microsoft.EntityFrameworkCore;

namespace GestorInventario.Models.Contexts
{
    public class InventarioContext : DbContext
    {
        // Constructor para establecer las opciones de la conexión
        public InventarioContext(DbContextOptions<InventarioContext>options) : base(options)
        {
        }

        // Definir las tablas
        public DbSet<Pais> Paises {get; set;}
        public DbSet<Region> Regiones {get; set;}
        public DbSet<HUB> HUB {get; set;}
        public DbSet<Marca> Marcas {get; set;}
        public DbSet<TipoDeEquipo> TiposDeEquipos {get; set;} 
        public DbSet<Empleado> Empleados {get; set;}
        public DbSet<Sede> Sedes {get; set;}
        public DbSet<DepartamentoEmpleado> DepartamentosEmpleados {get; set;}
        public DbSet<AreaEmpleado> AreasEmpleados {get; set;}
        public DbSet<PuestoEmpleado> PuestosEmpleados {get; set;}
        public DbSet<Usuario> Usuarios {get; set;}
        public DbSet<Edificio> Edificios {get; set;}
        public DbSet<Oficina> Oficinas {get; set;}
        public DbSet<Equipo> Equipos {get; set;}
        public DbSet<PropietarioEquipo> PropietarioEquipos {get; set;}
        public DbSet<ReporteEquipo> ReporteEquipos {get; set;}
        

        // Creación de las tablas en base a los modelos
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Pais>();
            modelBuilder.Entity<Region>();
            modelBuilder.Entity<HUB>();
            modelBuilder.Entity<Marca>();
            modelBuilder.Entity<TipoDeEquipo>();
            modelBuilder.Entity<Empleado>();
            modelBuilder.Entity<Sede>();
            modelBuilder.Entity<DepartamentoEmpleado>();
            modelBuilder.Entity<AreaEmpleado>();
            modelBuilder.Entity<PuestoEmpleado>();
            modelBuilder.Entity<Usuario>();
            modelBuilder.Entity<Edificio>();
            modelBuilder.Entity<Oficina>();
            modelBuilder.Entity<Equipo>();
            modelBuilder.Entity<PropietarioEquipo>();
            modelBuilder.Entity<ReporteEquipo>();
        }

    }
}