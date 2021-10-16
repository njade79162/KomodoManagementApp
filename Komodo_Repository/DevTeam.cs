using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Komodo_Repository
{
    class DevTeam
    {
        // contains team members (developers), team name, and team ID
        public List<Developer> TeamMembers { get; set; }
        public string TeamName { get; set; }
        public int TeamID { get; set; }

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
