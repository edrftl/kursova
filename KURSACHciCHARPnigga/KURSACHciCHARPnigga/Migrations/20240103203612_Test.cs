using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KURSACHciCHARPnigga.Migrations
{
    /// <inheritdoc />
    public partial class Test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameOfRoom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstRoomId = table.Column<int>(type: "int", nullable: true),
                    SecondRoomId = table.Column<int>(type: "int", nullable: true),
                    ThirdRoomId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "FirstRoomId", "NameOfRoom", "SecondRoomId", "ThirdRoomId" },
                values: new object[,]
                {
                    { 1, 2, "Room 1", 3, 4 },
                    { 2, 3, "Room 2", 4, 5 },
                    { 3, 4, "Room 3", 1, 4 },
                    { 4, 6, "Room 4", 3, 5 },
                    { 5, 4, "Room 5", 6, 7 },
                    { 6, 4, "Room 6", 5, 7 },
                    { 7, 9, "Room 7", 8, 6 },
                    { 8, 6, "Room 8", 11, 10 },
                    { 9, 17, "Room 9", 11, 7 },
                    { 10, 8, "Room 10", 11, 12 },
                    { 11, 9, "Room 11", 8, 11 },
                    { 12, 10, "Room 12", 13, 14 },
                    { 13, 16, "Room 13", 15, 14 },
                    { 14, 12, "Room 14", 13, 15 },
                    { 15, 17, "Room 15", 16, 14 },
                    { 16, 17, "Room 16", 16, 14 },
                    { 17, 16, "Room 17", 18, 15 },
                    { 18, null, "Room 18", null, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rooms");
        }
    }
}
