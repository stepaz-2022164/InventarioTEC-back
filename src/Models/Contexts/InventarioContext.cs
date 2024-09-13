using Microsoft.EntityFrameworkCore;

namespace GestorInventario.src.Models.Contexts
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
        // public DbSet<Sede> Sedes {get; set;}
        public DbSet<DepartamentoEmpleado> DepartamentosEmpleados {get; set;}
        public DbSet<AreaEmpleado> AreasEmpleados {get; set;}
        public DbSet<PuestoEmpleado> PuestosEmpleados {get; set;}
        public DbSet<Usuario> Usuarios {get; set;}
        //public DbSet<Edificio> Edificios {get; set;}
        //public DbSet<Oficina> Oficinas {get; set;}
        public DbSet<Equipo> Equipos {get; set;}
        public DbSet<PropietarioEquipo> PropietarioEquipos {get; set;}
        public DbSet<ReporteEquipo> ReporteEquipos {get; set;}
        

        // Creación de las tablas en base a los modelos
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //Pais
            modelBuilder.Entity<Pais>();
            //Region
            modelBuilder.Entity<Region>();
            //HUB
            modelBuilder.Entity<HUB>().HasOne(h => h.Region).WithMany().HasForeignKey(h => h.idRegion).OnDelete(DeleteBehavior.Restrict); //Quitar eliminación en cascada
            modelBuilder.Entity<HUB>().HasOne(h => h.Pais).WithMany().HasForeignKey(h => h.idPais).OnDelete(DeleteBehavior.Restrict);
            //Sede
            // modelBuilder.Entity<Sede>().HasOne(s => s.Pais).WithMany().HasForeignKey(s => s.idPais).OnDelete(DeleteBehavior.Restrict);
            // modelBuilder.Entity<Sede>().HasOne(s => s.Region).WithMany().HasForeignKey(s => s.idRegion).OnDelete(DeleteBehavior.Restrict);
            // modelBuilder.Entity<Sede>().HasOne(s => s.HUB).WithMany().HasForeignKey(s => s.idHUB).OnDelete(DeleteBehavior.Restrict);
            //Departamento Empleado
            modelBuilder.Entity<DepartamentoEmpleado>();
            //Area Empleado
            modelBuilder.Entity<AreaEmpleado>();
            //Puesto Empleado
            modelBuilder.Entity<PuestoEmpleado>();
            //Empleado 
            modelBuilder.Entity<Empleado>().HasOne(e => e.DepartamentoEmpleado).WithMany().HasForeignKey(e => e.idDepartamentoEmpleado).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Empleado>().HasOne(e => e.AreaEmpleado).WithMany().HasForeignKey(e => e.idAreaEmpleado).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Empleado>().HasOne(e => e.Region).WithMany().HasForeignKey(e => e.idRegion).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Empleado>().HasOne(e => e.HUB).WithMany().HasForeignKey(e => e.idHUB).OnDelete(DeleteBehavior.Restrict);
            //Usuario
            modelBuilder.Entity<Usuario>();
            //Edificio
            // modelBuilder.Entity<Edificio>();
            //Oficina
            // modelBuilder.Entity<Oficina>();
            //Marca
            modelBuilder.Entity<Marca>();
            //Tipo de Equipo
            modelBuilder.Entity<TipoDeEquipo>();
            //Equipo
            modelBuilder.Entity<Equipo>().HasOne(equi => equi.TipoDeEquipo).WithMany().HasForeignKey(equi => equi.idTipoDeEquipo).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Equipo>().HasOne(equi => equi.HUB).WithMany().HasForeignKey(equi => equi.idHUB).OnDelete(DeleteBehavior.Restrict);
            //Propietario Equipo
            modelBuilder.Entity<PropietarioEquipo>().HasOne(pe => pe.Empleado).WithMany().HasForeignKey(pe => pe.idEmpleado).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<PropietarioEquipo>().HasOne(pe => pe.Empleado).WithMany().HasForeignKey(pe => pe.idEmpleado).OnDelete(DeleteBehavior.Restrict);
            //Reporte Equipo
            modelBuilder.Entity<ReporteEquipo>();
        }
    }
}