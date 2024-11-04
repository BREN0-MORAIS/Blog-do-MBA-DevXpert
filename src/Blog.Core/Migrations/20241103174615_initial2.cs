using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog.Core.Migrations
{
    /// <inheritdoc />
    public partial class initial2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c47af98e-beae-4e19-8149-de210199cfed", "AQAAAAIAAYagAAAAEG6Ckx+ovoeLa8Zp4PCXYTBzsdnE3kZyisJmyeBaobuO3Y4WU1c5+w4A3tLSa+P4Yw==", "34cc6eed-499c-4749-bf95-a58dd36bca65" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "06a1050a-99f4-46db-b4fa-aef67f94e72b", "AQAAAAIAAYagAAAAEIUYMfx31nREy7CjOhue/rh3/2MItNBirXK0f1r/okufSwbj6wXSWB7dMKE2fLULVg==", "5172fb82-5211-45d0-8de1-56b6294280c3" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7c628cd9-f856-463d-bbed-b6d383404509", "AQAAAAIAAYagAAAAEIuxUYI6EwagRVCSsn3cV7J5ZFPtGf7dGsCrjlYOODtyWK2xUFE18jjBGDF1BqUlIg==", "11bd07e3-e284-483d-99fb-e7d26d905946" });
        }
    }
}
