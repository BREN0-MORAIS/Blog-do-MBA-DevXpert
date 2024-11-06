using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog.Core.Migrations
{
    /// <inheritdoc />
    public partial class initial3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comentarios_Posts_PostId",
                table: "Comentarios");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a8fda1d7-3230-4219-a1c8-8f1083f9ce3d", "AQAAAAIAAYagAAAAEIZJLd9qp2qZ+DPrhwZ6U/ZaIEhrpiog0eTNv20b8eHMqN2Fne17TCCjJiEcja14Zw==", "f13f831d-ec46-438e-a20c-c2fb2a58d3ca" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7a95122b-2c05-4c42-bb3c-e0910bcef900", "AQAAAAIAAYagAAAAEPbfzKuxQTLjkyOI8CEq4yKHqbxR3zKhFpkViCJn99SudYJc8j6s26JDhwxpQ/tuvQ==", "e9ba3d76-b574-4690-bbaf-9a3c959c0676" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e2a7f00a-5bbb-4963-8654-2cd89d80a6fe", "AQAAAAIAAYagAAAAEJjPVQiPI9BFBrsTJR7WRrLAcnX6Z4rqu6FJItFiP2QU7FYyfSkFBb8MYRKdq7W5mQ==", "c1e92de7-4169-4707-87a0-8ca13ad1426f" });

            migrationBuilder.AddForeignKey(
                name: "FK_Comentarios_Posts_PostId",
                table: "Comentarios",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comentarios_Posts_PostId",
                table: "Comentarios");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "679ccde4-774e-4e34-92ee-b77bc71f9c26", "AQAAAAIAAYagAAAAENwfu+Qna3lsTc2uzDYw8ttxLRMi1+X+AoPKK2LuuXpaEMtF+TRVCzNNH38HKpt9FA==", "5760b86a-43d3-48e3-858c-6a73b02bf56b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3011efcd-57fc-40d3-8b5f-0934fdcf9d17", "AQAAAAIAAYagAAAAEGaeLAbyUsGwI5V7vLoTjkYPHnFd7tIVEn5TnLN3GkXSN8Y15F2SyGlAkISPJhYayQ==", "374d086b-fd01-4529-b5fc-91ab0376254d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5101cf40-4985-42ad-acee-c09b99c7d584", "AQAAAAIAAYagAAAAECEVGNhdzkgtbQ+7rHlWSlKACXx8x6hPUTZnawvPndNF4kntKwY2uI5H/4f/gk7JEg==", "46644c62-bffa-4a93-9c08-a3886c2aeb2b" });

            migrationBuilder.AddForeignKey(
                name: "FK_Comentarios_Posts_PostId",
                table: "Comentarios",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
