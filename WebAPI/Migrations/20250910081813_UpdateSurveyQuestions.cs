using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSurveyQuestions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CustomerName",
                table: "SurveyResponses",
                newName: "Sector");

            migrationBuilder.AlterColumn<int>(
                name: "Question3",
                table: "SurveyResponses",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "Question2",
                table: "SurveyResponses",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "Question1",
                table: "SurveyResponses",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "SurveyResponses",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Question4",
                table: "SurveyResponses",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Question5",
                table: "SurveyResponses",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Question6",
                table: "SurveyResponses",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Question7",
                table: "SurveyResponses",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Question8",
                table: "SurveyResponses",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Question9",
                table: "SurveyResponses",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "SurveyResponses");

            migrationBuilder.DropColumn(
                name: "Question4",
                table: "SurveyResponses");

            migrationBuilder.DropColumn(
                name: "Question5",
                table: "SurveyResponses");

            migrationBuilder.DropColumn(
                name: "Question6",
                table: "SurveyResponses");

            migrationBuilder.DropColumn(
                name: "Question7",
                table: "SurveyResponses");

            migrationBuilder.DropColumn(
                name: "Question8",
                table: "SurveyResponses");

            migrationBuilder.DropColumn(
                name: "Question9",
                table: "SurveyResponses");

            migrationBuilder.RenameColumn(
                name: "Sector",
                table: "SurveyResponses",
                newName: "CustomerName");

            migrationBuilder.AlterColumn<string>(
                name: "Question3",
                table: "SurveyResponses",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<string>(
                name: "Question2",
                table: "SurveyResponses",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<string>(
                name: "Question1",
                table: "SurveyResponses",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");
        }
    }
}
