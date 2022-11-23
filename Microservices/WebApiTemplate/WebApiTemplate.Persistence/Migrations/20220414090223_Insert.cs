using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiTemplate.Persistence.Migrations
{
    public partial class Insert : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"INSERT INTO Products(Name) 
                VALUES 
                ('Ball'),
                ('Table'),
                ('Chair'); ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
