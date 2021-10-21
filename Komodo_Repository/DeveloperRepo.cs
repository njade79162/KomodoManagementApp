using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Komodo_Repository
{
    public class DeveloperRepo
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
        public Developer GetDeveloper(string name)
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
        public Developer GetDeveloper(int ID)
        {
            foreach(Developer dev in _developerDirectory)
            {
                if(dev.IDNum == ID)
                {
                    return dev;
                }
            }
            return null;
        }

        // get a developer that matches both name and ID
        public Developer GetDeveloper(string name, int ID)
        {
            foreach(Developer dev in _developerDirectory)
            {
                if(dev.IDNum == ID && dev.Name.ToLower() == name.ToLower())
                {
                    return dev;
                }
            }
            return null;
        }

        // get the full directory of developers
        public List<Developer> GetDevDirectory()
        {
            return _developerDirectory;
        }

        // update a single developer, with overloaded methods to find developer based on name, id, or both
        public bool UpdateDeveloper(int oldID, Developer newDev)
        {
            Developer oldDev = GetDeveloper(oldID);

            if(oldDev != null)
            {
                oldDev.Name = newDev.Name;
                oldDev.IDNum = newDev.IDNum;
                oldDev.PluralsightAccess = newDev.PluralsightAccess;
                return true;
            }

            return false;
        }

        public bool UpdateDeveloper(string oldName, Developer newDev)
        {
            Developer oldDev = GetDeveloper(oldName);

            if (oldDev != null)
            {
                oldDev.Name = newDev.Name;
                oldDev.IDNum = newDev.IDNum;
                oldDev.PluralsightAccess = newDev.PluralsightAccess;
                return true;
            }

            return false;
        }

        public bool UpdateDeveloper(string oldName, int oldID, Developer newDev)
        {
            Developer oldDev = GetDeveloper(oldName, oldID);

            if (oldDev != null)
            {
                oldDev.Name = newDev.Name;
                oldDev.IDNum = newDev.IDNum;
                oldDev.PluralsightAccess = newDev.PluralsightAccess;
                return true;
            }

            return false;
        }

        // remove a developer from directory
        public bool RemoveDev(Developer currentDev)
        {
            return _developerDirectory.Remove(currentDev);
        }
    }
}
