using Azure.Mobile.Server.Entity;
using BlogServer.DataObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BlogServer.Repositories
{
    public class BlogCommentsRepository : EntityTableRepository<BlogComment>
    {
        public BlogCommentsRepository(DbContext context) : base(context)
        {
        }

        public override async Task<BlogComment> CreateAsync(BlogComment item, CancellationToken cancellationToken = default)
        {
            var post = await this.Context.Set<BlogPost>().FindAsync(item.PostId);

            if(post != null)
            {
                post.CommentCount++;
            }

            return await base.CreateAsync(item, cancellationToken);
        }

        public override async Task DeleteAsync(string id, CancellationToken cancellationToken = default)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var entity = await LookupAsync(id, cancellationToken).ConfigureAwait(false);

            if (entity != null)
            {
                var post = await this.Context.Set<BlogPost>().FindAsync(entity.PostId);
                // We should never get into a situation where the CommentCount is 0 when there's still comments but checking for safetyness.
                post.CommentCount = post.CommentCount > 1 ? post.CommentCount - 1 : 0;
            }
                
            await base.DeleteAsync(id, cancellationToken);
        }

    }
}
