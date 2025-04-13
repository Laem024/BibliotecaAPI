using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BibliotecaAPI.Migrations
{
    /// <inheritdoc />
    public partial class add_column_table_autores : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "nombre_autor",
                table: "autores",
                newName: "nombres_autor");

            migrationBuilder.AddColumn<string>(
                name: "apellidos_autor",
                table: "autores",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "identificacion_autor",
                table: "autores",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "apellidos_autor",
                table: "autores");

            migrationBuilder.DropColumn(
                name: "identificacion_autor",
                table: "autores");

            migrationBuilder.RenameColumn(
                name: "nombres_autor",
                table: "autores",
                newName: "nombre_autor");
        }
    }
}
