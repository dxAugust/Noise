using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noise.Client
{
    public class Song
    {
        public int id { get; set; }
        public int artist_id { get; set; }
        public string name { get; set; }
        public string path { get; set; }
        public string length { get; set; }
        public int publication_date { get; set; }
        public string genre { get; set; }
        public string thumnail_path { get; set; }

    }
}
