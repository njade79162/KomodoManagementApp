using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Komodo_Repository
{
    public class Developer
    {
        // has name, ID number, and access to pluralsight
        public string Name { get; set; }
        public int IDNum { get; set; }
        public bool PluralsightAccess { get; set; }

        // default constructor
        public Developer()
        {
        }
        // more detailed constructor
        public Developer(string name, int id, bool pluralsightAccess)
        {
            Name = name;
            IDNum = id;
            PluralsightAccess = pluralsightAccess;
        }
    }
}
