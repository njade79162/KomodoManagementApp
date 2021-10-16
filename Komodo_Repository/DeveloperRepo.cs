using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Komodo_Repository
{
    class DeveloperRepo
    {
        // list of developers
        private readonly List<Developer> _developerDirectory = new List<Developer>();

        // create developer and store in a list/directory of all developers
        public bool AddDeveloperToDirectory(Developer dev)
        {
            int startingCount = _developerDirectory.Count;
            _developerDirectory.Add(dev);
            // Test if starting count changed
            return _developerDirectory.Count > startingCount;
        }

        // get a single developer by name
        public Developer GetDeveloperByName(string name)
        {
            foreach(Developer dev in _developerDirectory)
            {
                if (dev.Name.ToLower() == name.ToLower())
                {
                    return dev;
                }
            }
            return null;
        }

        // get a single developer by ID
        // get the full directory of developers
        // update a single developer
        // remove a developer from directory
    }
}
