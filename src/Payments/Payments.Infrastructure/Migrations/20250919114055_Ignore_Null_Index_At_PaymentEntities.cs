#nullable disable

namespace Payments.Infrastructure.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    /// <inheritdoc />
    public partial class Ignore_Null_Index_At_PaymentEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PaymentEntities_ClientId",
                table: "PaymentEntities");

            migrationBuilder.DropIndex(
                name: "IX_PaymentEntities_CompanyId",
                table: "PaymentEntities");

            migrationBuilder.DropIndex(
                name: "IX_PaymentEntities_ProviderId",
                table: "PaymentEntities");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentEntities_ClientId",
                table: "PaymentEntities",
                column: "ClientId",
                unique: true,
                filter: "Deleted = 0 AND ClientId IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentEntities_CompanyId",
                table: "PaymentEntities",
                column: "CompanyId",
                unique: true,
                filter: "Deleted = 0 AND CompanyId IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentEntities_ProviderId",
                table: "PaymentEntities",
                column: "ProviderId",
                unique: true,
                filter: "Deleted = 0 AND ProviderId IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PaymentEntities_ClientId",
                table: "PaymentEntities");

            migrationBuilder.DropIndex(
                name: "IX_PaymentEntities_CompanyId",
                table: "PaymentEntities");

            migrationBuilder.DropIndex(
                name: "IX_PaymentEntities_ProviderId",
                table: "PaymentEntities");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentEntities_ClientId",
                table: "PaymentEntities",
                column: "ClientId",
                unique: true,
                filter: "Deleted = 0");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentEntities_CompanyId",
                table: "PaymentEntities",
                column: "CompanyId",
                unique: true,
                filter: "Deleted = 0");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentEntities_ProviderId",
                table: "PaymentEntities",
                column: "ProviderId",
                unique: true,
                filter: "Deleted = 0");
        }
    }
}
