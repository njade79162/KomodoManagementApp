using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Komodo_Repository
{
    public class DevTeamRepo
    {
        // list of dev teams
        private readonly List<DevTeam> _devTeamDirectory = new List<DevTeam>();

        // create new dev team
        public bool CreateDevTeam(DevTeam team)
        {
            int startingCount = _devTeamDirectory.Count;
            _devTeamDirectory.Add(team);
            return _devTeamDirectory.Count > startingCount;
        }

        // get a dev team by name
        public DevTeam GetDevTeam(string name)
        {
            foreach(DevTeam team in _devTeamDirectory)
            {
                if(team.TeamName.ToLower() == name.ToLower())
                {
                    return team;
                }
            }
            return null;
        }

        // get a dev team by ID
        public DevTeam GetDevTeam(int ID)
        {
            foreach (DevTeam team in _devTeamDirectory)
            {
                if (team.TeamID == ID)
                {
                    return team;
                }
            }
            return null;
        }

        // get a dev team by both name and ID
        public DevTeam GetDevTeam(string name, int ID)
        {
            foreach(DevTeam team in _devTeamDirectory)
            {
                if(team.TeamName == name && team.TeamID == ID)
                {
                    return team;
                }
            }
            return null;
        }

        // get the full directory of dev teams
        public List<DevTeam> GetDevTeamDirectory()
        {
            return _devTeamDirectory;
        }

        // update a dev team, with overloaded methods for finding a team based on name, id, or both
        public bool UpdateDeveloper(string oldName, DevTeam newTeam)
        {
            DevTeam oldTeam = GetDevTeam(oldName);

            if(oldTeam != null)
            {
                oldTeam.TeamName = newTeam.TeamName;
                oldTeam.TeamID = newTeam.TeamID;
                oldTeam.TeamMembers = newTeam.TeamMembers;
                return true;
            }

            return false;
        }

        public bool UpdateDeveloper(int oldID, DevTeam newTeam)
        {
            DevTeam oldTeam = GetDevTeam(oldID);

            if (oldTeam != null)
            {
                oldTeam.TeamName = newTeam.TeamName;
                oldTeam.TeamID = newTeam.TeamID;
                oldTeam.TeamMembers = newTeam.TeamMembers;
                return true;
            }

            return false;
        }

        public bool UpdateDeveloper(string oldName, int oldID, DevTeam newTeam)
        {
            DevTeam oldTeam = GetDevTeam(oldName, oldID);

            if (oldTeam != null)
            {
                oldTeam.TeamName = newTeam.TeamName;
                oldTeam.TeamID = newTeam.TeamID;
                oldTeam.TeamMembers = newTeam.TeamMembers;
                return true;
            }

            return false;
        }

        // remove a dev team from the directory
        public bool RemoveDevTeam(DevTeam currentTeam)
        {
            return _devTeamDirectory.Remove(currentTeam);
        }
    }
}
