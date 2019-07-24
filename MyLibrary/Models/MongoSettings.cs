using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyLibrary.Models
{
    // CM 07/23/2019 
    // This class is used for simplification of the mongosettings
    public class MongoSettings
    {
        public string ConnectionString { get; set; }
        public string Database { get; set; }
    }
}
