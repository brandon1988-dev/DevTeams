using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTeams_POCOs
{
    /*
       Teams need to contain their Team members(Developers) and their Team Name, and Team ID.
       Our managers need to be able to add and remove members to/from a team by their unique identifier.
       They should be able to see a list of existing developers to choose from and add to existing teams.
       Odds are, the manager will create a team, and then add Developers individually from the Developer Directory to that team.
    */
    public class DevTeam
    {
        public DevTeam() { }


        public DevTeam(int id, string name)
        {
            ID = id;
            Name = name;
            Developers = new List<Developer>();
            
        }

        //unique identifier

        public int ID { get; set; }
        public string Name { get; set; }

        public List<Developer> Developers { get; set; } = new List<Developer>();   
    }
}
