using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NailStudio.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("575078bd-3528-497e-a8af-e2f993c31343"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("b937dcb0-2c42-4df6-b6a1-424211579bce"));

            migrationBuilder.DeleteData(
                table: "StaffMembers",
                keyColumn: "Id",
                keyValue: new Guid("d12c229c-5aa3-47c5-a5d3-00fc1303b712"));

            migrationBuilder.DeleteData(
                table: "StaffMembers",
                keyColumn: "Id",
                keyValue: new Guid("d48c343d-a8f9-4eb9-a51c-197a7916de44"));

            migrationBuilder.CreateTable(
                name: "TimeSlots",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Time = table.Column<TimeSpan>(type: "time", nullable: false),
                    IsBooked = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StaffMemberId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeSlots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TimeSlots_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_TimeSlots_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_TimeSlots_StaffMembers_StaffMemberId",
                        column: x => x.StaffMemberId,
                        principalTable: "StaffMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "Id", "Description", "DurationInMinutes", "ImageUrl", "IsDeleted", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("643c2db7-d4a8-4fc1-b78d-8bab6a2cf683"), "A professional manicure service.", 60, "https://example.com/images/manicure.jpg", false, "Manicure", 30.00m },
                    { new Guid("6eb11a65-82da-499c-b364-c6216d4f6591"), "A relaxing pedicure service.", 75, "https://example.com/images/pedicure.jpg", false, "Pedicure", 50.00m }
                });

            migrationBuilder.InsertData(
                table: "StaffMembers",
                columns: new[] { "Id", "IsDeleted", "Name", "PhotoUrl", "Role" },
                values: new object[,]
                {
                    { new Guid("24cbbcea-9824-44ac-b3e6-bfb9b6cbcd72"), false, "Jane Smith", "https://visages.net/wp-content/uploads/2022/06/dsc_3716.jpg", "Manager" },
                    { new Guid("5a3fa68c-6a8e-4136-a2d8-7dd0d792f61d"), false, "Anna Rose", "https://www.flagman.bg/news/2024/10/30/173027983811467.png", "Nail Technician" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TimeSlots_Date_Time_StaffMemberId",
                table: "TimeSlots",
                columns: new[] { "Date", "Time", "StaffMemberId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TimeSlots_ServiceId",
                table: "TimeSlots",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeSlots_StaffMemberId",
                table: "TimeSlots",
                column: "StaffMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeSlots_UserId",
                table: "TimeSlots",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TimeSlots");

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("643c2db7-d4a8-4fc1-b78d-8bab6a2cf683"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("6eb11a65-82da-499c-b364-c6216d4f6591"));

            migrationBuilder.DeleteData(
                table: "StaffMembers",
                keyColumn: "Id",
                keyValue: new Guid("24cbbcea-9824-44ac-b3e6-bfb9b6cbcd72"));

            migrationBuilder.DeleteData(
                table: "StaffMembers",
                keyColumn: "Id",
                keyValue: new Guid("5a3fa68c-6a8e-4136-a2d8-7dd0d792f61d"));

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "Id", "Description", "DurationInMinutes", "ImageUrl", "IsDeleted", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("575078bd-3528-497e-a8af-e2f993c31343"), "A relaxing pedicure service.", 75, "https://example.com/images/pedicure.jpg", false, "Pedicure", 50.00m },
                    { new Guid("b937dcb0-2c42-4df6-b6a1-424211579bce"), "A professional manicure service.", 60, "https://example.com/images/manicure.jpg", false, "Manicure", 30.00m }
                });

            migrationBuilder.InsertData(
                table: "StaffMembers",
                columns: new[] { "Id", "IsDeleted", "Name", "PhotoUrl", "Role" },
                values: new object[,]
                {
                    { new Guid("d12c229c-5aa3-47c5-a5d3-00fc1303b712"), false, "Anna Rose", "https://www.flagman.bg/news/2024/10/30/173027983811467.png", "Nail Technician" },
                    { new Guid("d48c343d-a8f9-4eb9-a51c-197a7916de44"), false, "Jane Smith", "https://visages.net/wp-content/uploads/2022/06/dsc_3716.jpg", "Manager" }
                });
        }
    }
}
