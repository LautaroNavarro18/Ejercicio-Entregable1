using Microsoft.EntityFrameworkCore.Migrations;

namespace AccesoDatos.Migrations
{
    public partial class AddCategoriaAndBajaLogica : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.Id);
                });

            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "Libros",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<int>(
                name: "CategoriaId",
                table: "Libros",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Libros_CategoriaId",
                table: "Libros",
                column: "CategoriaId");

            // Nota: SQLite no admite agregar una restriccion FOREIGN KEY a una
            // tabla ya existente mediante ALTER TABLE, por lo que la relacion
            // Libro-Categoria queda validada a nivel de modelo de EF Core
            // (columna CategoriaId + Include) y no como constraint fisica.
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categorias");

            migrationBuilder.DropIndex(
                name: "IX_Libros_CategoriaId",
                table: "Libros");

            migrationBuilder.DropColumn(
                name: "CategoriaId",
                table: "Libros");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "Libros");
        }
    }
}
