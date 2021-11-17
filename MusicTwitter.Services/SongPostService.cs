using MusicTwitter.Data;
using MusicTwitter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicTwitter.Services
{
    public class SongPostService
    {
        private readonly Guid _userId;
        public SongPostService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateSongPost(SongPostCreate model)
        {
            var entity =
                new SongPost()
                {
                    OwnerId = _userId,
                    SongTitle = model.SongTitle,
                    Message = model.Message,
                    CreatedUtc = DateTimeOffset.Now
                };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.SongPosts.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<SongPostListItem> GetSongPosts()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .SongPosts
                        .Where(e => e.OwnerId == _userId)
                        .Select(
                        e =>
                            new SongPostListItem
                            {
                                SongPostId = e.SongPostId,
                                SongTitle = e.SongTitle,
                                CreatedUtc = e.CreatedUtc
                            }
                       );
                return query.ToArray();
            }
        }

        public SongPostDetail GetSongPostById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .SongPosts
                    .Single(e => id == e.SongPostId && e.OwnerId == _userId);
                return
                    new SongPostDetail
                    {
                        Message = entity.Message,
                        CreatedUtc = entity.CreatedUtc,
                        ModifiedUtc = entity.ModifiedUtc,
                        SongPostId = entity.SongPostId,
                        SongTitle = entity.SongTitle,
                       
                    };
            }
        }

        public bool EditSongPost(SongPostEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .SongPosts
                    .Single(e => e.SongPostId == model.SongPostId && e.OwnerId == _userId);

                entity.SongTitle = model.SongTitle;
                entity.Message = model.Message;
                entity.ModifiedUtc = DateTimeOffset.UtcNow;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteSongPost(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .SongPosts
                    .Single(e => e.SongPostId == id && e.OwnerId == _userId);

                ctx.SongPosts.Remove(entity);
                return ctx.SaveChanges() == 1;
            }
        }
    }
}
