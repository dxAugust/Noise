using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noise.Client
{
    public class User
    {
        public int id {  get; set; }
        public string session_token { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public int role_id { get; set; }

        public int subscription_date { get; set; }
    }
}
