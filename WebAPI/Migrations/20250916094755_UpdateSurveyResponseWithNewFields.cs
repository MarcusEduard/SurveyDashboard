using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSurveyResponseWithNewFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CompanyName",
                table: "SurveyResponses",
                newName: "SurveyId");

            migrationBuilder.AddColumn<string>(
                name: "CustomerName",
                table: "SurveyResponses",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCompleted",
                table: "SurveyResponses",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateSent",
                table: "SurveyResponses",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerName",
                table: "SurveyResponses");

            migrationBuilder.DropColumn(
                name: "DateCompleted",
                table: "SurveyResponses");

            migrationBuilder.DropColumn(
                name: "DateSent",
                table: "SurveyResponses");

            migrationBuilder.RenameColumn(
                name: "SurveyId",
                table: "SurveyResponses",
                newName: "CompanyName");
        }
    }
}
