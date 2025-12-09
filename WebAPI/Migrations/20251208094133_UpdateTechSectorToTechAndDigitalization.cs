using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTechSectorToTechAndDigitalization : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"UPDATE SurveyResponses 
                  SET Sector = 'Tech & Digitalization' 
                  WHERE Sector = 'Tech';"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"UPDATE SurveyResponses 
                  SET Sector = 'Tech' 
                  WHERE Sector = 'Tech & Digitalization';"
            );
        }
    }
}
