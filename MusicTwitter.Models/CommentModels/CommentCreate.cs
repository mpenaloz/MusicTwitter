using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicTwitter.Models.CommentModels
{
    public class CommentCreate
    {
        [Required]
        public string Content { get; set; }
        [Required]
        public int SongPostId { get; set; }
        public SongPostDetail SongPostDetail { get; set; }
    }
}
