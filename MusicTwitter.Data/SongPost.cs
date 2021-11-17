using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicTwitter.Data
{
    public class SongPost
    {
        [Key]
        public int SongPostId { get; set; }
        [Required]
        public Guid OwnerId { get; set; }
        [Required]
        public string SongTitle { get; set; }
        [Required]
        public string Message { get; set; }
        [Required]
        public DateTimeOffset CreatedUtc { get; set; }
        public DateTimeOffset? ModifiedUtc { get; set; }
    }
}
