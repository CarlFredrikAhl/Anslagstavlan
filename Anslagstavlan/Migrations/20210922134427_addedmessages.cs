using Microsoft.EntityFrameworkCore.Migrations;

namespace Anslagstavlan.Migrations
{
    public partial class addedmessages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatMessageModel_Rooms_ChatRoomId",
                table: "ChatMessageModel");

            migrationBuilder.DropForeignKey(
                name: "FK_ChatMessageModel_Users_ChatUserId",
                table: "ChatMessageModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChatMessageModel",
                table: "ChatMessageModel");

            migrationBuilder.RenameTable(
                name: "ChatMessageModel",
                newName: "Messages");

            migrationBuilder.RenameIndex(
                name: "IX_ChatMessageModel_ChatUserId",
                table: "Messages",
                newName: "IX_Messages_ChatUserId");

            migrationBuilder.RenameIndex(
                name: "IX_ChatMessageModel_ChatRoomId",
                table: "Messages",
                newName: "IX_Messages_ChatRoomId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Messages",
                table: "Messages",
                column: "ChatMessageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Rooms_ChatRoomId",
                table: "Messages",
                column: "ChatRoomId",
                principalTable: "Rooms",
                principalColumn: "ChatRoomId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Users_ChatUserId",
                table: "Messages",
                column: "ChatUserId",
                principalTable: "Users",
                principalColumn: "ChatUserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Rooms_ChatRoomId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Users_ChatUserId",
                table: "Messages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Messages",
                table: "Messages");

            migrationBuilder.RenameTable(
                name: "Messages",
                newName: "ChatMessageModel");

            migrationBuilder.RenameIndex(
                name: "IX_Messages_ChatUserId",
                table: "ChatMessageModel",
                newName: "IX_ChatMessageModel_ChatUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Messages_ChatRoomId",
                table: "ChatMessageModel",
                newName: "IX_ChatMessageModel_ChatRoomId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChatMessageModel",
                table: "ChatMessageModel",
                column: "ChatMessageId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatMessageModel_Rooms_ChatRoomId",
                table: "ChatMessageModel",
                column: "ChatRoomId",
                principalTable: "Rooms",
                principalColumn: "ChatRoomId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChatMessageModel_Users_ChatUserId",
                table: "ChatMessageModel",
                column: "ChatUserId",
                principalTable: "Users",
                principalColumn: "ChatUserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
