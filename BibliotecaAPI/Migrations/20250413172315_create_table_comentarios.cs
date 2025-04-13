using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BibliotecaAPI.Migrations
{
    /// <inheritdoc />
    public partial class create_table_comentarios : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "comentarios",
                columns: table => new
                {
                    id_comentario = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    cuerpo_comentario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    fecha_publicacion_comentario = table.Column<DateTime>(type: "datetime2", nullable: false),
                    id_libro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_comentarios", x => x.id_comentario);
                    table.ForeignKey(
                        name: "FK_comentarios_libros_id_libro",
                        column: x => x.id_libro,
                        principalTable: "libros",
                        principalColumn: "id_libro",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_comentarios_id_libro",
                table: "comentarios",
                column: "id_libro");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "comentarios");
        }
    }
}
