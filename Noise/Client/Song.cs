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
        public string artist_name { get; set; }
        public string name { get; set; }
        public string path { get; set; }
        public string thumbnail_path { get; set; }
        public Int64 publication_date { get; set;  }
        public int genre { get; set; }
        public int plays { get; set; }
        public int length { get; set; }
    }
}
