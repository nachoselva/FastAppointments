using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Payments.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PaymentEntities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProviderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    VersionEndFrom = table.Column<DateTime>(type: "datetime2", nullable: false)
                        .Annotation("SqlServer:TemporalIsPeriodEndColumn", true),
                    VersionStartFrom = table.Column<DateTime>(type: "datetime2", nullable: false)
                        .Annotation("SqlServer:TemporalIsPeriodStartColumn", true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentEntities", x => x.Id);
                    table.CheckConstraint("CK_PaymentEntity_ExternalId", "Deleted = 0 OR ((ClientId IS NOT NULL OR ProviderId IS NOT NULL OR CompanyId IS NOT NULL))");
                })
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "PaymentEntitiesHistory")
                .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "VersionEndFrom")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "VersionStartFrom");

            migrationBuilder.CreateTable(
                name: "PaymentAccounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountType = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    AccountData = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PaymentEntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    VersionEndFrom = table.Column<DateTime>(type: "datetime2", nullable: false)
                        .Annotation("SqlServer:TemporalIsPeriodEndColumn", true),
                    VersionStartFrom = table.Column<DateTime>(type: "datetime2", nullable: false)
                        .Annotation("SqlServer:TemporalIsPeriodStartColumn", true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentAccounts_PaymentEntities_PaymentEntityId",
                        column: x => x.PaymentEntityId,
                        principalTable: "PaymentEntities",
                        principalColumn: "Id");
                })
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "PaymentAccountsHistory")
                .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "VersionEndFrom")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "VersionStartFrom");

            migrationBuilder.CreateTable(
                name: "Bills",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    SenderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReceiverId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    VersionEndFrom = table.Column<DateTime>(type: "datetime2", nullable: false)
                        .Annotation("SqlServer:TemporalIsPeriodEndColumn", true),
                    VersionStartFrom = table.Column<DateTime>(type: "datetime2", nullable: false)
                        .Annotation("SqlServer:TemporalIsPeriodStartColumn", true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bills_PaymentAccounts_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "PaymentAccounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Bills_PaymentAccounts_SenderId",
                        column: x => x.SenderId,
                        principalTable: "PaymentAccounts",
                        principalColumn: "Id");
                })
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "BillsHistory")
                .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "VersionEndFrom")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "VersionStartFrom");

            migrationBuilder.CreateTable(
                name: "BillItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PricePerUnit = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    UnitsCount = table.Column<int>(type: "int", nullable: false),
                    UnitsName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    BillId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    VersionEndFrom = table.Column<DateTime>(type: "datetime2", nullable: false)
                        .Annotation("SqlServer:TemporalIsPeriodEndColumn", true),
                    VersionStartFrom = table.Column<DateTime>(type: "datetime2", nullable: false)
                        .Annotation("SqlServer:TemporalIsPeriodStartColumn", true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BillItems_Bills_BillId",
                        column: x => x.BillId,
                        principalTable: "Bills",
                        principalColumn: "Id");
                })
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "BillItemsHistory")
                .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "VersionEndFrom")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "VersionStartFrom");

            migrationBuilder.CreateTable(
                name: "BillSources",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SourceType = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    SourceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BillId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    VersionEndFrom = table.Column<DateTime>(type: "datetime2", nullable: false)
                        .Annotation("SqlServer:TemporalIsPeriodEndColumn", true),
                    VersionStartFrom = table.Column<DateTime>(type: "datetime2", nullable: false)
                        .Annotation("SqlServer:TemporalIsPeriodStartColumn", true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillSources", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BillSources_Bills_BillId",
                        column: x => x.BillId,
                        principalTable: "Bills",
                        principalColumn: "Id");
                })
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "BillSourcesHistory")
                .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "VersionEndFrom")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "VersionStartFrom");

            migrationBuilder.CreateTable(
                name: "BillItemSources",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SourceType = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    SourceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BillItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    VersionEndFrom = table.Column<DateTime>(type: "datetime2", nullable: false)
                        .Annotation("SqlServer:TemporalIsPeriodEndColumn", true),
                    VersionStartFrom = table.Column<DateTime>(type: "datetime2", nullable: false)
                        .Annotation("SqlServer:TemporalIsPeriodStartColumn", true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillItemSources", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BillItemSources_BillItems_BillItemId",
                        column: x => x.BillItemId,
                        principalTable: "BillItems",
                        principalColumn: "Id");
                })
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "BillItemSourcesHistory")
                .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "VersionEndFrom")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "VersionStartFrom");

            migrationBuilder.CreateIndex(
                name: "IX_BillItems_BillId",
                table: "BillItems",
                column: "BillId");

            migrationBuilder.CreateIndex(
                name: "IX_BillItemSources_BillItemId_SourceType_SourceId",
                table: "BillItemSources",
                columns: new[] { "BillItemId", "SourceType", "SourceId" },
                unique: true,
                filter: "Deleted = 0");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_ReceiverId",
                table: "Bills",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_SenderId",
                table: "Bills",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_BillSources_BillId_SourceType_SourceId",
                table: "BillSources",
                columns: new[] { "BillId", "SourceType", "SourceId" },
                unique: true,
                filter: "Deleted = 0");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentAccounts_PaymentEntityId",
                table: "PaymentAccounts",
                column: "PaymentEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentEntities_ClientId",
                table: "PaymentEntities",
                column: "ClientId",
                unique: true,
                filter: "Deleted = 0");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentEntities_ClientId_ProviderId_CompanyId",
                table: "PaymentEntities",
                columns: new[] { "ClientId", "ProviderId", "CompanyId" },
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BillItemSources")
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "BillItemSourcesHistory")
                .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "VersionEndFrom")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "VersionStartFrom");

            migrationBuilder.DropTable(
                name: "BillSources")
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "BillSourcesHistory")
                .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "VersionEndFrom")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "VersionStartFrom");

            migrationBuilder.DropTable(
                name: "BillItems")
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "BillItemsHistory")
                .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "VersionEndFrom")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "VersionStartFrom");

            migrationBuilder.DropTable(
                name: "Bills")
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "BillsHistory")
                .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "VersionEndFrom")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "VersionStartFrom");

            migrationBuilder.DropTable(
                name: "PaymentAccounts")
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "PaymentAccountsHistory")
                .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "VersionEndFrom")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "VersionStartFrom");

            migrationBuilder.DropTable(
                name: "PaymentEntities")
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "PaymentEntitiesHistory")
                .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "VersionEndFrom")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "VersionStartFrom");
        }
    }
}
