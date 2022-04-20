using Microsoft.EntityFrameworkCore;

namespace WebApiTemplate.Persistence
{
    public class DatabaseInitializer
    {
        public static void Initialize(DatabaseContext context)
        {
            if (!context.Database.CanConnect())
            {
                context.Database.Migrate();
            }
        }
    }
}
