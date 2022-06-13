using Microsoft.EntityFrameworkCore;

namespace Diabetes.Persistence
{
    public class DbInitializer
    {
        public static void Initialize(DbContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }
    }
}