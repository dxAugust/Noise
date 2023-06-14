using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noise.Client
{
    public class Artist
    {
        public int id { get; set; }
        public int belong_id { get; set; }
        public string username { get; set; }
        public string description { get; set; }
        public string location { get; set; }
        public string genre { get; set; }
        public string banner { get; set; }
    }
}
