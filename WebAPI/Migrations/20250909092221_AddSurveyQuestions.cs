using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddSurveyQuestions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Answer",
                table: "SurveyResponses",
                newName: "Question3");

            migrationBuilder.AddColumn<string>(
                name: "Question1",
                table: "SurveyResponses",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Question2",
                table: "SurveyResponses",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Question1",
                table: "SurveyResponses");

            migrationBuilder.DropColumn(
                name: "Question2",
                table: "SurveyResponses");

            migrationBuilder.RenameColumn(
                name: "Question3",
                table: "SurveyResponses",
                newName: "Answer");
        }
    }
}
