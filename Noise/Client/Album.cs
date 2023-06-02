using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noise.Client
{
    public class Album
    {
        public int id { get; set; }
        public int artist_id { get; set; }
        public int[] songs_id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int length { get; set; }
        public int publication_date { get; set; }
        public string genre { get; set; }
        public string thumbnail_path { get; set; }
    }
}
