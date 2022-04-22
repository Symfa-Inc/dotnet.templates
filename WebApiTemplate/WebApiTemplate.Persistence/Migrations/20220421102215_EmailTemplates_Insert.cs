using Microsoft.EntityFrameworkCore.Migrations;
using WebApiTemplate.Domain.Enums.EmailTemplate;

#nullable disable

namespace WebApiTemplate.Persistence.Migrations
{
    public partial class EmailTemplates_Insert : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string dataChangedBody = @"
<!DOCTYPE html>
<html>
<body>
  <div>
    <h2 style=""color:green;"">DataChanged</h2>
    <p>EmailTemplate test: {{DataTitle}}</p>
  </div>
</body>
</html>
";

            string passwordResetBody = @"
<!DOCTYPE html>
<html>
<body>
  <div>
    <h2 style=""color:green;"">PasswordReset</h2>
    <p>EmailTemplate test: {{Code}}</p>
  </div>
</body>
</html>
";

            string loginAttemptsFailBody = @"
<!DOCTYPE html>
<html>
<body>
  <div>
    <h2 style=""color:green;"">LoginAttemptsFail</h2>
    <p>EmailTemplate test: {{AttemptsNumber}}</p>
  </div>
</body>
</html>
";

            migrationBuilder.Sql(@$"INSERT INTO EmailTemplates(Name, Type, Subject, Body) 
                VALUES 
                ('{EmailTemplateType.DataChanged:G}', {EmailTemplateType.DataChanged:D}, 'Data changed', '{dataChangedBody}'),
                ('{EmailTemplateType.PasswordReset:G}', {EmailTemplateType.PasswordReset:D}, 'Password reset', '{passwordResetBody}'),
                ('{EmailTemplateType.LoginAttemptsFail:G}', {EmailTemplateType.LoginAttemptsFail:D}, 'Login attempts fail', '{loginAttemptsFailBody}')
                ;");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
