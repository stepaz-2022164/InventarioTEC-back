﻿// <auto-generated />
using System;
using GestorInventario.src.Models.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GestorInventario.Migrations
{
    [DbContext(typeof(InventarioContext))]
    partial class InventarioContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AreaEmpleado", b =>
                {
                    b.Property<int>("idAreaEmpleado")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("idAreaEmpleado");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("idAreaEmpleado"));

                    b.Property<string>("descripcionAreaEmpleado")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("descripcionAreaEmpleado");

                    b.Property<int>("estado")
                        .HasColumnType("int")
                        .HasColumnName("estado");

                    b.Property<int>("idDepartamentoEmpleado")
                        .HasColumnType("int")
                        .HasColumnName("idDepartamentoEmpleado");

                    b.Property<string>("nombreAreaEmpleado")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("nombreAreaEmpleado");

                    b.HasKey("idAreaEmpleado");

                    b.HasIndex("idDepartamentoEmpleado");

                    b.ToTable("AreasEmpleados");
                });

            modelBuilder.Entity("DepartamentoEmpleado", b =>
                {
                    b.Property<int>("idDepartamentoEmpleado")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("idDepartamentoEmpleado");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("idDepartamentoEmpleado"));

                    b.Property<string>("descripcionAreaEmpleado")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(200)")
                        .HasColumnName("descripcionAreaEmpleado");

                    b.Property<string>("nombreDepartamentoEmpleado")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("nombreDepartamentoEmpleado");

                    b.HasKey("idDepartamentoEmpleado");

                    b.ToTable("DepartamentosEmpleados");
                });

            modelBuilder.Entity("Empleado", b =>
                {
                    b.Property<int>("idEmpleado")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("idEmpleado");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("idEmpleado"));

                    b.Property<string>("correoEmpleado")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasColumnName("correoEmpleado");

                    b.Property<int>("estado")
                        .HasColumnType("int")
                        .HasColumnName("estado");

                    b.Property<int>("idAreaEmpleado")
                        .HasColumnType("int")
                        .HasColumnName("idAreaEmpleado");

                    b.Property<int>("idDepartamentoEmpleado")
                        .HasColumnType("int")
                        .HasColumnName("idDepartamentoEmpleado");

                    b.Property<int>("idPuestoEmpleado")
                        .HasColumnType("int")
                        .HasColumnName("idPuestoEmpleado");

                    b.Property<int>("idSede")
                        .HasColumnType("int")
                        .HasColumnName("idSede");

                    b.Property<string>("nombreEmpleado")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("nombreEmpleado");

                    b.Property<int>("numeroDeFicha")
                        .HasColumnType("int")
                        .HasColumnName("numeroDeFicha");

                    b.Property<string>("telefonoEmpleado")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("telefonoEmpleado");

                    b.HasKey("idEmpleado");

                    b.HasIndex("idAreaEmpleado");

                    b.HasIndex("idDepartamentoEmpleado");

                    b.HasIndex("idPuestoEmpleado");

                    b.HasIndex("idSede");

                    b.ToTable("Empleados");
                });

            modelBuilder.Entity("Equipo", b =>
                {
                    b.Property<int>("idEquipo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("idEquipo");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("idEquipo"));

                    b.Property<int>("estado")
                        .HasColumnType("int")
                        .HasColumnName("estado");

                    b.Property<string>("estadoEquipo")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)")
                        .HasColumnName("estadoEquipo");

                    b.Property<DateOnly>("fechaDeIngreso")
                        .HasColumnType("date")
                        .HasColumnName("fechaDeIngreso");

                    b.Property<int>("idTipoDeEquipo")
                        .HasColumnType("int")
                        .HasColumnName("idTipoDeEquipo");

                    b.Property<string>("numeroDeSerie")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("numeroDeSerie");

                    b.HasKey("idEquipo");

                    b.HasIndex("idTipoDeEquipo");

                    b.ToTable("Equipos");
                });

            modelBuilder.Entity("HUB", b =>
                {
                    b.Property<int>("idHUB")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("idHUB");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("idHUB"));

                    b.Property<int>("idPais")
                        .HasColumnType("int")
                        .HasColumnName("idPais");

                    b.Property<int>("idRegion")
                        .HasColumnType("int")
                        .HasColumnName("idRegion");

                    b.Property<string>("nombreHUB")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("nombreHUB");

                    b.HasKey("idHUB");

                    b.HasIndex("idPais");

                    b.HasIndex("idRegion");

                    b.ToTable("HUB");
                });

            modelBuilder.Entity("Marca", b =>
                {
                    b.Property<int>("idMarca")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("idMarca");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("idMarca"));

                    b.Property<string>("descripcionMarca")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("descripcionMarca");

                    b.Property<int>("estado")
                        .HasColumnType("int")
                        .HasColumnName("estado");

                    b.Property<string>("nombreMarca")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("nombreMarca");

                    b.HasKey("idMarca");

                    b.ToTable("Marcas");
                });

            modelBuilder.Entity("Pais", b =>
                {
                    b.Property<int>("idPais")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("idPais");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("idPais"));

                    b.Property<string>("nombrePais")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("nombrePais");

                    b.HasKey("idPais");

                    b.ToTable("Paises");
                });

            modelBuilder.Entity("PropietarioEquipo", b =>
                {
                    b.Property<int>("idPropietarioEquipopacion")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("idPropietarioEquipo");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("idPropietarioEquipopacion"));

                    b.Property<int>("estado")
                        .HasColumnType("int")
                        .HasColumnName("estado");

                    b.Property<DateOnly>("fechaDeEntrega")
                        .HasColumnType("date")
                        .HasColumnName("fechaDeEntrega");

                    b.Property<int>("idEmpleado")
                        .HasColumnType("int")
                        .HasColumnName("idEmpleado");

                    b.Property<int>("idEquipo")
                        .HasColumnType("int")
                        .HasColumnName("idEquipo");

                    b.HasKey("idPropietarioEquipopacion");

                    b.HasIndex("idEmpleado");

                    b.HasIndex("idEquipo");

                    b.ToTable("PropietarioEquipos");
                });

            modelBuilder.Entity("PuestoEmpleado", b =>
                {
                    b.Property<int>("idPuestoEmpleado")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("idPuestoEmpleado");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("idPuestoEmpleado"));

                    b.Property<string>("descripcionPuestoEmpleado")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(200)")
                        .HasColumnName("descripcionPuestoEmpleado");

                    b.Property<int>("estado")
                        .HasColumnType("int")
                        .HasColumnName("estado");

                    b.Property<int>("idAreaEmpleado")
                        .HasColumnType("int")
                        .HasColumnName("idAreaEmpleado");

                    b.Property<string>("nombrePuestoEmpleado")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("nombrePuestoEmpleado");

                    b.HasKey("idPuestoEmpleado");

                    b.HasIndex("idAreaEmpleado");

                    b.ToTable("PuestosEmpleados");
                });

            modelBuilder.Entity("Region", b =>
                {
                    b.Property<int>("idRegion")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("idRegion");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("idRegion"));

                    b.Property<int>("idPais")
                        .HasColumnType("int")
                        .HasColumnName("idPais");

                    b.Property<string>("nombreRegion")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)")
                        .HasColumnName("nombreRegion");

                    b.HasKey("idRegion");

                    b.HasIndex("idPais");

                    b.ToTable("Regiones");
                });

            modelBuilder.Entity("ReporteEquipo", b =>
                {
                    b.Property<int>("idReporteEquipo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("idReporteEquipo");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("idReporteEquipo"));

                    b.Property<string>("descripcionReporteEquipo")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("descripcionReporteEquipo");

                    b.Property<int>("estado")
                        .HasColumnType("int")
                        .HasColumnName("estado");

                    b.Property<DateOnly>("fechaReporte")
                        .HasColumnType("date")
                        .HasColumnName("fechaReporte");

                    b.Property<int>("idEquipo")
                        .HasColumnType("int")
                        .HasColumnName("idEquipo");

                    b.HasKey("idReporteEquipo");

                    b.HasIndex("idEquipo");

                    b.ToTable("ReporteEquipos");
                });

            modelBuilder.Entity("Sede", b =>
                {
                    b.Property<int>("idSede")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("idSede");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("idSede"));

                    b.Property<string>("direccionSede")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("direccionSede");

                    b.Property<int>("estado")
                        .HasColumnType("int")
                        .HasColumnName("estado");

                    b.Property<int>("idHUB")
                        .HasColumnType("int")
                        .HasColumnName("idHUB");

                    b.Property<int>("idPais")
                        .HasColumnType("int")
                        .HasColumnName("idPais");

                    b.Property<int>("idRegion")
                        .HasColumnType("int")
                        .HasColumnName("idRegion");

                    b.Property<string>("nombreSede")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("nombreSede");

                    b.HasKey("idSede");

                    b.HasIndex("idHUB");

                    b.HasIndex("idPais");

                    b.HasIndex("idRegion");

                    b.ToTable("Sedes");
                });

            modelBuilder.Entity("TipoDeEquipo", b =>
                {
                    b.Property<int>("idTipoDeEquipo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("idTipoDeEquipo");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("idTipoDeEquipo"));

                    b.Property<string>("descripcionTipoDeEquipo")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("text")
                        .HasColumnName("descripcionTipoDeEquipo");

                    b.Property<int>("estado")
                        .HasColumnType("int")
                        .HasColumnName("estado");

                    b.Property<int>("idMarca")
                        .HasColumnType("int")
                        .HasColumnName("idMarca");

                    b.Property<string>("nombreTipoDeEquipo")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("nombreTipoDeEquipo");

                    b.Property<int>("stock")
                        .HasColumnType("int")
                        .HasColumnName("stock");

                    b.HasKey("idTipoDeEquipo");

                    b.HasIndex("idMarca");

                    b.ToTable("TiposDeEquipos");
                });

            modelBuilder.Entity("Usuario", b =>
                {
                    b.Property<int>("idUsuario")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("idUsuario");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("idUsuario"));

                    b.Property<int>("estado")
                        .HasColumnType("int")
                        .HasColumnName("estado");

                    b.Property<string>("pass")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)")
                        .HasColumnName("pass");

                    b.Property<string>("usuario")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)")
                        .HasColumnName("usuario");

                    b.HasKey("idUsuario");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("AreaEmpleado", b =>
                {
                    b.HasOne("DepartamentoEmpleado", "DepartamentoEmpleado")
                        .WithMany()
                        .HasForeignKey("idDepartamentoEmpleado")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DepartamentoEmpleado");
                });

            modelBuilder.Entity("Empleado", b =>
                {
                    b.HasOne("AreaEmpleado", "AreaEmpleado")
                        .WithMany()
                        .HasForeignKey("idAreaEmpleado")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("DepartamentoEmpleado", "DepartamentoEmpleado")
                        .WithMany()
                        .HasForeignKey("idDepartamentoEmpleado")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("PuestoEmpleado", "PuestoEmpleado")
                        .WithMany()
                        .HasForeignKey("idPuestoEmpleado")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Sede", "Sede")
                        .WithMany()
                        .HasForeignKey("idSede")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("AreaEmpleado");

                    b.Navigation("DepartamentoEmpleado");

                    b.Navigation("PuestoEmpleado");

                    b.Navigation("Sede");
                });

            modelBuilder.Entity("Equipo", b =>
                {
                    b.HasOne("TipoDeEquipo", "TipoDeEquipo")
                        .WithMany()
                        .HasForeignKey("idTipoDeEquipo")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("TipoDeEquipo");
                });

            modelBuilder.Entity("HUB", b =>
                {
                    b.HasOne("Pais", "Pais")
                        .WithMany()
                        .HasForeignKey("idPais")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Region", "Region")
                        .WithMany()
                        .HasForeignKey("idRegion")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Pais");

                    b.Navigation("Region");
                });

            modelBuilder.Entity("PropietarioEquipo", b =>
                {
                    b.HasOne("Empleado", "Empleado")
                        .WithMany()
                        .HasForeignKey("idEmpleado")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Equipo", "Equipo")
                        .WithMany()
                        .HasForeignKey("idEquipo")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Empleado");

                    b.Navigation("Equipo");
                });

            modelBuilder.Entity("PuestoEmpleado", b =>
                {
                    b.HasOne("AreaEmpleado", "AreaEmpleado")
                        .WithMany()
                        .HasForeignKey("idAreaEmpleado")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AreaEmpleado");
                });

            modelBuilder.Entity("Region", b =>
                {
                    b.HasOne("Pais", "Pais")
                        .WithMany()
                        .HasForeignKey("idPais")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pais");
                });

            modelBuilder.Entity("ReporteEquipo", b =>
                {
                    b.HasOne("Equipo", "Equipo")
                        .WithMany()
                        .HasForeignKey("idEquipo")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Equipo");
                });

            modelBuilder.Entity("Sede", b =>
                {
                    b.HasOne("HUB", "HUB")
                        .WithMany()
                        .HasForeignKey("idHUB")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Pais", "Pais")
                        .WithMany()
                        .HasForeignKey("idPais")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Region", "Region")
                        .WithMany()
                        .HasForeignKey("idRegion")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("HUB");

                    b.Navigation("Pais");

                    b.Navigation("Region");
                });

            modelBuilder.Entity("TipoDeEquipo", b =>
                {
                    b.HasOne("Marca", "Marca")
                        .WithMany()
                        .HasForeignKey("idMarca")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Marca");
                });
#pragma warning restore 612, 618
        }
    }
}
