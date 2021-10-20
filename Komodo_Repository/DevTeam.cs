using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Komodo_Repository
{
    public class DevTeam
    {
        // contains team members (developers), team name, and team ID
        public List<Developer> TeamMembers { get; set; }
        public string TeamName { get; set; }
        public int TeamID { get; set; }

        // add a developer to the team
        public bool AddDeveloperToTeam(Developer dev)
        {
            int startingCount = TeamMembers.Count;
            TeamMembers.Add(dev);
            return TeamMembers.Count > startingCount;
        }

        // remove a developer from the team
        public bool RemoveDeveloperFromTeam(Developer dev)
        {
            return TeamMembers.Remove(dev);
        }

        // default constructor
        public DevTeam()
        {

        }

        // more detailed constructor
        public DevTeam(List<Developer> teamMembers, string teamName, int teamID)
        {
            TeamMembers = teamMembers;
            TeamName = teamName;
            TeamID = teamID;
        }
    }
}
