using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicTwitter.Models
{
    public class SongPostDetail
    {
        public int SongPostId { get; set; }
        public string SongTitle { get; set; }
        public string Message { get; set; }
        [Display(Name = "Created")]
        public DateTimeOffset CreatedUtc { get; set; }
        [Display(Name = "modified")]
        public DateTimeOffset? ModifiedUtc { get; set; }
    
    }
}
