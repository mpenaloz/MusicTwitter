﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicTwitter.Models
{
    public class SongPostEdit
    {
        public int SongPostId { get; set; }
        public string SongTitle { get; set; }
        public string Message { get; set; }
    }
}
