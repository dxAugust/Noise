using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noise.Client
{
    public class Config
    {
        public static string serverURL = "http://localhost:3000";
        public static string apiURL = serverURL + "/api/";

        public static User userInfo = null;

        public static Song currentPlaying = null;
    }
}
