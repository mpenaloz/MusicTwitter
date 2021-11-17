using MusicTwitter.Data;
using MusicTwitter.Models.CommentModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicTwitter.Services
{
    public class CommentServices
    {
        private readonly Guid _userId;

        public CommentServices(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateComment(CommentCreate model)
        {

            var entity =
                new Comment()
                {
                    OwnerId = _userId,
                    SongPostId = model.SongPostId,
                    Content = model.Content,
                    
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Comments.Add(entity);
                return ctx.SaveChanges() == 1;
            }
            
        }

        public IEnumerable<CommentList> GetComments()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Comments
                        .Where(e => e.OwnerId == _userId)
                        .Select(
                        e =>
                            new CommentList
                            {
                                SongPostId = e.SongPostId,
                                Content = e.Content,
                                CommentId = e.CommentId
                            }
                       );
                return query.ToArray();
            }
        }
        public CommentDetail GetCommentById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Comments
                    .Single(e => id == e.CommentId && e.OwnerId == _userId);
                return
                     new CommentDetail
                     {
                         Content = entity.Content,
                         CommentId = entity.CommentId,
                         SongPostId = entity.SongPostId,


                     };
                       
            }
        }

        public bool DeleteComment(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Comments
                    .Single(e => e.CommentId == id && e.OwnerId == _userId);

                ctx.Comments.Remove(entity);
                return ctx.SaveChanges() == 1;
            }
        }

    }
}
