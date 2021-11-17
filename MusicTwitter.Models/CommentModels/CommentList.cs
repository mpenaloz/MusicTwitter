using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicTwitter.Models.CommentModels
{
    public class CommentList
    {
        public int SongPostId { get; set; }
        public string Content { get; set; }
        public int CommentId { get; set; }
        public string OwnerId { get; set; }
        
    }
}
