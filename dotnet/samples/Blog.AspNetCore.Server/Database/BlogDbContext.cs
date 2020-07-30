using Blog.AspNetCore.Server.DataObjects;
using Microsoft.EntityFrameworkCore;

namespace Blog.AspNetCore.Server.Database
{
    public class BlogDbContext : DbContext
    {
        public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options)
        {
        }

        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<BlogComment> BlogComments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Bookmark> Bookmarks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BlogComment>()
                .HasOne<BlogPost>()
                .WithMany()
                .HasForeignKey(comment => comment.PostId);

            modelBuilder.Entity<BlogComment>()
               .HasOne<User>()
               .WithMany()
               .HasForeignKey(comment => comment.OwnerId);

            modelBuilder.Entity<BlogPost>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(post => post.OwnerId);

            modelBuilder.Entity<Bookmark>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(bookmark => bookmark.UserId);

            modelBuilder.Entity<Bookmark>()
                .HasOne<BlogPost>()
                .WithMany()
                .HasForeignKey(bookmark => bookmark.PostId);
        }
    }
}
