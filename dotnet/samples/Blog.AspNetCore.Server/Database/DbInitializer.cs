using Microsoft.EntityFrameworkCore;

namespace Blog.AspNetCore.Server.Database
{
    public static class DbInitializer
    {
        public static void Initialize(BlogDbContext context)
        {
            context.Database.Migrate();
            context.SaveChanges();
        }
    }
}
