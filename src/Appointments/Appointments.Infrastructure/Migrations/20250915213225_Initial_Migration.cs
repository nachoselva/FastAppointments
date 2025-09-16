using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Appointments.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial_Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppointmentConfigurations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DurationInMinutes = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_AppointmentConfigurations", x => x.Id);
                })
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "AppointmentConfigurationsHistory")
                .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "VersionEndFrom")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "VersionStartFrom");

            migrationBuilder.CreateTable(
                name: "Attendes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Category = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    ExternalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsOptional = table.Column<bool>(type: "bit", nullable: false),
                    AppointmentConfigurationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.PrimaryKey("PK_Attendes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attendes_AppointmentConfigurations_AppointmentConfigurationId",
                        column: x => x.AppointmentConfigurationId,
                        principalTable: "AppointmentConfigurations",
                        principalColumn: "Id");
                })
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "AttendesHistory")
                .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "VersionEndFrom")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "VersionStartFrom");

            migrationBuilder.CreateTable(
                name: "Recurrences",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstStartOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastStartOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AppointmentConfigurationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.PrimaryKey("PK_Recurrences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recurrences_AppointmentConfigurations_AppointmentConfigurationId",
                        column: x => x.AppointmentConfigurationId,
                        principalTable: "AppointmentConfigurations",
                        principalColumn: "Id");
                })
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "RecurrencesHistory")
                .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "VersionEndFrom")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "VersionStartFrom");

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UnitsCount = table.Column<int>(type: "int", nullable: false),
                    ExternalServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AppointmentConfigurationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.PrimaryKey("PK_Services", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Services_AppointmentConfigurations_AppointmentConfigurationId",
                        column: x => x.AppointmentConfigurationId,
                        principalTable: "AppointmentConfigurations",
                        principalColumn: "Id");
                })
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "ServicesHistory")
                .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "VersionEndFrom")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "VersionStartFrom");

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RecurrenceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AppointmentConfigurationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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
                    table.PrimaryKey("PK_Events", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Events_AppointmentConfigurations_AppointmentConfigurationId",
                        column: x => x.AppointmentConfigurationId,
                        principalTable: "AppointmentConfigurations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Events_Recurrences_RecurrenceId",
                        column: x => x.RecurrenceId,
                        principalTable: "Recurrences",
                        principalColumn: "Id");
                })
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "EventsHistory")
                .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "VersionEndFrom")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "VersionStartFrom");

            migrationBuilder.CreateIndex(
                name: "IX_Attendes_AppointmentConfigurationId",
                table: "Attendes",
                column: "AppointmentConfigurationId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_AppointmentConfigurationId",
                table: "Events",
                column: "AppointmentConfigurationId",
                unique: true,
                filter: "[AppointmentConfigurationId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Events_RecurrenceId",
                table: "Events",
                column: "RecurrenceId");

            migrationBuilder.CreateIndex(
                name: "IX_Recurrences_AppointmentConfigurationId",
                table: "Recurrences",
                column: "AppointmentConfigurationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Services_AppointmentConfigurationId",
                table: "Services",
                column: "AppointmentConfigurationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attendes")
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "AttendesHistory")
                .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "VersionEndFrom")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "VersionStartFrom");

            migrationBuilder.DropTable(
                name: "Events")
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "EventsHistory")
                .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "VersionEndFrom")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "VersionStartFrom");

            migrationBuilder.DropTable(
                name: "Services")
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "ServicesHistory")
                .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "VersionEndFrom")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "VersionStartFrom");

            migrationBuilder.DropTable(
                name: "Recurrences")
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "RecurrencesHistory")
                .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "VersionEndFrom")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "VersionStartFrom");

            migrationBuilder.DropTable(
                name: "AppointmentConfigurations")
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "AppointmentConfigurationsHistory")
                .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "VersionEndFrom")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "VersionStartFrom");
        }
    }
}
