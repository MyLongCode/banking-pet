using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Notifications.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EditNotifications : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmailMessages_Notifications_NotificationId",
                schema: "notifications",
                table: "EmailMessages");

            migrationBuilder.DropIndex(
                name: "IX_EmailMessages_NotificationId",
                schema: "notifications",
                table: "EmailMessages");

            migrationBuilder.DropColumn(
                name: "NotificationId",
                schema: "notifications",
                table: "EmailMessages");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "NotificationId",
                schema: "notifications",
                table: "EmailMessages",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_EmailMessages_NotificationId",
                schema: "notifications",
                table: "EmailMessages",
                column: "NotificationId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EmailMessages_Notifications_NotificationId",
                schema: "notifications",
                table: "EmailMessages",
                column: "NotificationId",
                principalSchema: "notifications",
                principalTable: "Notifications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
