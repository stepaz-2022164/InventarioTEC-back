using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestorInventario.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DepartamentosEmpleados",
                columns: table => new
                {
                    idDepartamentoEmpleado = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombreDepartamentoEmpleado = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    descripcionAreaEmpleado = table.Column<string>(type: "varchar(200)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartamentosEmpleados", x => x.idDepartamentoEmpleado);
                });

            migrationBuilder.CreateTable(
                name: "Marcas",
                columns: table => new
                {
                    idMarca = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombreMarca = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    descripcionMarca = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marcas", x => x.idMarca);
                });

            migrationBuilder.CreateTable(
                name: "Paises",
                columns: table => new
                {
                    idPais = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombrePais = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paises", x => x.idPais);
                });

            migrationBuilder.CreateTable(
                name: "PuestosEmpleados",
                columns: table => new
                {
                    idPuestoEmpleado = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombrePuestoEmpleado = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    descripcionPuestoEmpleado = table.Column<string>(type: "varchar(200)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PuestosEmpleados", x => x.idPuestoEmpleado);
                });

            migrationBuilder.CreateTable(
                name: "AreasEmpleados",
                columns: table => new
                {
                    idAreaEmpleado = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombreAreaEmpleado = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    descripcionAreaEmpleado = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    idDepartamentoEmpleado = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AreasEmpleados", x => x.idAreaEmpleado);
                    table.ForeignKey(
                        name: "FK_AreasEmpleados_DepartamentosEmpleados_idDepartamentoEmpleado",
                        column: x => x.idDepartamentoEmpleado,
                        principalTable: "DepartamentosEmpleados",
                        principalColumn: "idDepartamentoEmpleado",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TiposDeEquipos",
                columns: table => new
                {
                    idTipoDeEquipo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombreTipoDeEquipo = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    descripcionTipoDeEquipo = table.Column<string>(type: "text", maxLength: 100, nullable: false),
                    stock = table.Column<int>(type: "int", nullable: false),
                    idMarca = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposDeEquipos", x => x.idTipoDeEquipo);
                    table.ForeignKey(
                        name: "FK_TiposDeEquipos_Marcas_idMarca",
                        column: x => x.idMarca,
                        principalTable: "Marcas",
                        principalColumn: "idMarca",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Regiones",
                columns: table => new
                {
                    idRegion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombreRegion = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    idPais = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regiones", x => x.idRegion);
                    table.ForeignKey(
                        name: "FK_Regiones_Paises_idPais",
                        column: x => x.idPais,
                        principalTable: "Paises",
                        principalColumn: "idPais",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Equipos",
                columns: table => new
                {
                    idEquipo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    numeroDeSerie = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    estado = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    fechaDeIngreso = table.Column<DateOnly>(type: "date", nullable: false),
                    idTipoDeEquipo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipos", x => x.idEquipo);
                    table.ForeignKey(
                        name: "FK_Equipos_TiposDeEquipos_idTipoDeEquipo",
                        column: x => x.idTipoDeEquipo,
                        principalTable: "TiposDeEquipos",
                        principalColumn: "idTipoDeEquipo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HUB",
                columns: table => new
                {
                    idHUB = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombreHUB = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    idRegion = table.Column<int>(type: "int", nullable: false),
                    RegionidRegion = table.Column<int>(type: "int", nullable: false),
                    idPais = table.Column<int>(type: "int", nullable: false),
                    PaisidPais = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HUB", x => x.idHUB);
                    table.ForeignKey(
                        name: "FK_HUB_Paises_PaisidPais",
                        column: x => x.PaisidPais,
                        principalTable: "Paises",
                        principalColumn: "idPais",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HUB_Regiones_RegionidRegion",
                        column: x => x.RegionidRegion,
                        principalTable: "Regiones",
                        principalColumn: "idRegion",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReporteEquipos",
                columns: table => new
                {
                    idReporteEquipo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fechaReporte = table.Column<DateOnly>(type: "date", nullable: false),
                    descripcionReporteEquipo = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    idEquipo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReporteEquipos", x => x.idReporteEquipo);
                    table.ForeignKey(
                        name: "FK_ReporteEquipos_Equipos_idEquipo",
                        column: x => x.idEquipo,
                        principalTable: "Equipos",
                        principalColumn: "idEquipo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Empleados",
                columns: table => new
                {
                    idEmpleado = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    numeroDeFicha = table.Column<int>(type: "int", nullable: false),
                    idPuestoEmpleado = table.Column<int>(type: "int", nullable: false),
                    nombreEmpleado = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    telefonoEmpleado = table.Column<string>(type: "varchar(20)", maxLength: 100, nullable: false),
                    correoEmpleado = table.Column<string>(type: "varchar(50)", nullable: false),
                    idDepartamento = table.Column<int>(type: "int", nullable: false),
                    idAreaEmpleado = table.Column<int>(type: "int", nullable: false),
                    idHUB = table.Column<int>(type: "int", nullable: false),
                    idDepartamentoEmpleado = table.Column<int>(type: "int", nullable: false),
                    idRegion = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empleados", x => x.idEmpleado);
                    table.ForeignKey(
                        name: "FK_Empleados_AreasEmpleados_idAreaEmpleado",
                        column: x => x.idAreaEmpleado,
                        principalTable: "AreasEmpleados",
                        principalColumn: "idAreaEmpleado",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Empleados_DepartamentosEmpleados_idDepartamentoEmpleado",
                        column: x => x.idDepartamentoEmpleado,
                        principalTable: "DepartamentosEmpleados",
                        principalColumn: "idDepartamentoEmpleado",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Empleados_HUB_idHUB",
                        column: x => x.idHUB,
                        principalTable: "HUB",
                        principalColumn: "idHUB",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Empleados_PuestosEmpleados_idPuestoEmpleado",
                        column: x => x.idPuestoEmpleado,
                        principalTable: "PuestosEmpleados",
                        principalColumn: "idPuestoEmpleado",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Empleados_Regiones_idRegion",
                        column: x => x.idRegion,
                        principalTable: "Regiones",
                        principalColumn: "idRegion",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sedes",
                columns: table => new
                {
                    idSede = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombreSede = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    direccionSede = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    idPais = table.Column<int>(type: "int", nullable: false),
                    idRegion = table.Column<int>(type: "int", nullable: false),
                    idHUB = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sedes", x => x.idSede);
                    table.ForeignKey(
                        name: "FK_Sedes_HUB_idHUB",
                        column: x => x.idHUB,
                        principalTable: "HUB",
                        principalColumn: "idHUB",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sedes_Paises_idPais",
                        column: x => x.idPais,
                        principalTable: "Paises",
                        principalColumn: "idPais",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sedes_Regiones_idRegion",
                        column: x => x.idRegion,
                        principalTable: "Regiones",
                        principalColumn: "idRegion",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PropietarioEquipos",
                columns: table => new
                {
                    idPropietarioEquipo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idEmpleado = table.Column<int>(type: "int", nullable: false),
                    idEquipo = table.Column<int>(type: "int", nullable: false),
                    fechaDeEntrega = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropietarioEquipos", x => x.idPropietarioEquipo);
                    table.ForeignKey(
                        name: "FK_PropietarioEquipos_Empleados_idEmpleado",
                        column: x => x.idEmpleado,
                        principalTable: "Empleados",
                        principalColumn: "idEmpleado",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PropietarioEquipos_Equipos_idEquipo",
                        column: x => x.idEquipo,
                        principalTable: "Equipos",
                        principalColumn: "idEquipo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    idUsuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    usuario = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    pass = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    idEmpleado = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.idUsuario);
                    table.ForeignKey(
                        name: "FK_Usuarios_Empleados_idEmpleado",
                        column: x => x.idEmpleado,
                        principalTable: "Empleados",
                        principalColumn: "idEmpleado",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Edificios",
                columns: table => new
                {
                    idEdificio = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cantidadOficinas = table.Column<int>(type: "int", nullable: false),
                    cantidadNiveles = table.Column<int>(type: "int", nullable: false),
                    nombreEdificio = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    descripcionEdificio = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    idSede = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Edificios", x => x.idEdificio);
                    table.ForeignKey(
                        name: "FK_Edificios_Sedes_idSede",
                        column: x => x.idSede,
                        principalTable: "Sedes",
                        principalColumn: "idSede",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Oficinas",
                columns: table => new
                {
                    idOficina = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombreOficina = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    descripcionOficina = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    niveloficina = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    idEdificio = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Oficinas", x => x.idOficina);
                    table.ForeignKey(
                        name: "FK_Oficinas_Edificios_idEdificio",
                        column: x => x.idEdificio,
                        principalTable: "Edificios",
                        principalColumn: "idEdificio",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AreasEmpleados_idDepartamentoEmpleado",
                table: "AreasEmpleados",
                column: "idDepartamentoEmpleado");

            migrationBuilder.CreateIndex(
                name: "IX_Edificios_idSede",
                table: "Edificios",
                column: "idSede");

            migrationBuilder.CreateIndex(
                name: "IX_Empleados_idAreaEmpleado",
                table: "Empleados",
                column: "idAreaEmpleado");

            migrationBuilder.CreateIndex(
                name: "IX_Empleados_idDepartamentoEmpleado",
                table: "Empleados",
                column: "idDepartamentoEmpleado");

            migrationBuilder.CreateIndex(
                name: "IX_Empleados_idHUB",
                table: "Empleados",
                column: "idHUB");

            migrationBuilder.CreateIndex(
                name: "IX_Empleados_idPuestoEmpleado",
                table: "Empleados",
                column: "idPuestoEmpleado");

            migrationBuilder.CreateIndex(
                name: "IX_Empleados_idRegion",
                table: "Empleados",
                column: "idRegion");

            migrationBuilder.CreateIndex(
                name: "IX_Equipos_idTipoDeEquipo",
                table: "Equipos",
                column: "idTipoDeEquipo");

            migrationBuilder.CreateIndex(
                name: "IX_HUB_PaisidPais",
                table: "HUB",
                column: "PaisidPais");

            migrationBuilder.CreateIndex(
                name: "IX_HUB_RegionidRegion",
                table: "HUB",
                column: "RegionidRegion");

            migrationBuilder.CreateIndex(
                name: "IX_Oficinas_idEdificio",
                table: "Oficinas",
                column: "idEdificio");

            migrationBuilder.CreateIndex(
                name: "IX_PropietarioEquipos_idEmpleado",
                table: "PropietarioEquipos",
                column: "idEmpleado");

            migrationBuilder.CreateIndex(
                name: "IX_PropietarioEquipos_idEquipo",
                table: "PropietarioEquipos",
                column: "idEquipo");

            migrationBuilder.CreateIndex(
                name: "IX_Regiones_idPais",
                table: "Regiones",
                column: "idPais");

            migrationBuilder.CreateIndex(
                name: "IX_ReporteEquipos_idEquipo",
                table: "ReporteEquipos",
                column: "idEquipo");

            migrationBuilder.CreateIndex(
                name: "IX_Sedes_idHUB",
                table: "Sedes",
                column: "idHUB");

            migrationBuilder.CreateIndex(
                name: "IX_Sedes_idPais",
                table: "Sedes",
                column: "idPais");

            migrationBuilder.CreateIndex(
                name: "IX_Sedes_idRegion",
                table: "Sedes",
                column: "idRegion");

            migrationBuilder.CreateIndex(
                name: "IX_TiposDeEquipos_idMarca",
                table: "TiposDeEquipos",
                column: "idMarca");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_idEmpleado",
                table: "Usuarios",
                column: "idEmpleado");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Oficinas");

            migrationBuilder.DropTable(
                name: "PropietarioEquipos");

            migrationBuilder.DropTable(
                name: "ReporteEquipos");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Edificios");

            migrationBuilder.DropTable(
                name: "Equipos");

            migrationBuilder.DropTable(
                name: "Empleados");

            migrationBuilder.DropTable(
                name: "Sedes");

            migrationBuilder.DropTable(
                name: "TiposDeEquipos");

            migrationBuilder.DropTable(
                name: "AreasEmpleados");

            migrationBuilder.DropTable(
                name: "PuestosEmpleados");

            migrationBuilder.DropTable(
                name: "HUB");

            migrationBuilder.DropTable(
                name: "Marcas");

            migrationBuilder.DropTable(
                name: "DepartamentosEmpleados");

            migrationBuilder.DropTable(
                name: "Regiones");

            migrationBuilder.DropTable(
                name: "Paises");
        }
    }
}
