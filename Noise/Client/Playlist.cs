using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noise.Client
{
    public class Playlist
    {
        public int id { get; set; } 
        public int user_id { get; set; }
        public int[] songs_id { get; set; }
        public string name { get; set; }
        public int length { get; set; }
        public string thumbnail_path { get; set; }
    }
}
