using DevTeams_POCOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTeams_Repository
{
    public class DevTeamsRepository
    {
        // This is my fake data base
        protected readonly List<DevTeam> _devTeamDirectory = new List<DevTeam>();
        private int _count;
        private readonly DeveloperRepository _developerContent;

        public DevTeamsRepository(DeveloperRepository devRepo)
        {
            _developerContent = devRepo;
        }

        //Create adding a team to the 'data base'
        public bool AddDeveloperTeamToDirectory(DevTeam team)
        {
            if (team is null)
            {
                return false;
            }
            else
            {
                _count++;
                team.ID = _count;
                _devTeamDirectory.Add(team);
                return true;
            }
        }
        public DevTeam GetDevTeamByID(int id)
        {
            foreach (DevTeam devTeam in _devTeamDirectory)
            {
                if (devTeam.ID == id)
                {
                    return devTeam;
                }
            }
            return null;
        }
        public bool AddDeveloperToTeam(int DevTeamId, int DevId)
        {
            DevTeam teamData = GetDevTeamByID(DevTeamId);
            if (teamData == null)
                return false;   

            Developer developerData = _developerContent.GetDeveloperByID(DevId);
            if (developerData == null)
                return false;

            teamData.Developers.Add(developerData);
            return true;
        }
        //Read
        public List<DevTeam> GetContents()
        {
            return _devTeamDirectory;
        }
       

        //Update -> this clears out EVERYTHING!!!
        public bool UpdateExistingDeveloperTeam(int devTeamId, DevTeam content)
        {
            //find the old content...
            DevTeam oldContent = GetDevTeamByID(devTeamId);

            if (oldContent != null)
            {
                oldContent.Name = content.Name;
                oldContent.Developers = (content.Developers.Count==0) ?  oldContent.Developers:content.Developers;
                
                return true;
            }
            else
            {
                return false;
            }

        }
        public bool DeleteExsistingDeveloperTeam(DevTeam existingContent)
        {
            bool deleteResult = _devTeamDirectory.Remove(existingContent);
            return deleteResult;
        }
    }
}


