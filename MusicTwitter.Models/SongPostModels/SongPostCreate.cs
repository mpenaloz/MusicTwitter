using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicTwitter.Models
{
    public class SongPostCreate
    {
        [Required]
        public string SongTitle { get; set; }
        [Required]
        public string Message { get; set; }
    }
}
