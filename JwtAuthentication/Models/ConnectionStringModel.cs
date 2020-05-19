using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JwtAuthentication.Models
{
    public class ConnectionStringModel
    {
        public string cs { get; set; }
        public string Key { get; set; }
        public string Issuer { get; set; }
    }
}
