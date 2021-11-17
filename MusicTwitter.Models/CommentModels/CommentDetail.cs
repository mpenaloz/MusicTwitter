using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicTwitter.Models.CommentModels
{
    public class CommentDetail
    {
        public string Content { get; set; }
        public int SongPostId { get; set; }
        public string OwnerId { get; set; }
        public SongPostDetail SongPostDetail { get; set; }
        public int CommentId { get; set; }
    }
}
