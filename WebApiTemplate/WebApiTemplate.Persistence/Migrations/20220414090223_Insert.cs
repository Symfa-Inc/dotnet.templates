using Microsoft.EntityFrameworkCore.Migrations;
using System.Text;

#nullable disable

namespace WebApiTemplate.Persistence.Migrations
{
    public partial class Insert : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine(@"INSERT INTO Products(Name) 
                VALUES 
                ('Ball'),
                ('Table'),
                ('Chair'); ");

            migrationBuilder.Sql(stringBuilder.ToString());
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
