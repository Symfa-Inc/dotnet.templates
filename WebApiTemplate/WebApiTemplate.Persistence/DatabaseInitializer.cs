using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiTemplate.Persistence
{
    public class DatabaseInitializer
    {
        public static void Initialize(DatabaseContext context)
        {
            // Create database if not exists
            context.Database.EnsureCreated();
        }
    }
}
