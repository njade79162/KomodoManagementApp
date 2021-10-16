using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Komodo_Repository
{
    public class Developer
    {
        // has name and ID numbers, must be able to identify one developer without mistaking them for another.
        public string Name { get; set; }
        public int IDNum { get; set; }
        // need to know if they have access to Pluralsight
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
