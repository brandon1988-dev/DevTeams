using DevTeams_POCOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTeams_Repository
{
    public class DeveloperRepository
    {
        private readonly List<Developer> _developerContent = new List<Developer>();
        private int _count;

        //Create
        public bool AddDeveloper(Developer developer)
        {
            if (developer is null)
            {
                return false;
            }
            else
            {
                _count++;
                developer.ID = _count;
                _developerContent.Add(developer);
                return true;
            }
        }
        //Read
        public List<Developer> GetDevelopers()
        {
            return _developerContent;
        }
        public List<Developer> GetDevelopersWithOutPluralsight()
        {
            //make a variable that will hold all of the 'found' items

            List<Developer> hasPluralsight = new List<Developer>();
            foreach (Developer developer in _developerContent)
            {
                if (developer.HasPluralsight == false)

                {
                    hasPluralsight.Add(developer);
                }
            }
            return hasPluralsight;
        }
        public Developer GetDeveloperByID(int id)
        {
            foreach (Developer developer in _developerContent)
            {
                if (developer.ID == id)
                {
                    return developer;
                }
            }
                return null;
        }
        //Update
        public bool UpdateExistingDeveloper(int developerId, Developer content)
        {
            //find the old content...
            Developer oldContent = GetDeveloperByID(developerId);

            if (oldContent != null)
            {
                oldContent.FirstName = content.FirstName;   
                oldContent.LastName = content.LastName;
                oldContent.HasPluralsight = content.HasPluralsight;
                return true;
            }
            else
            {
                return false;
            }
        }

        //Delete
        public bool DeleteExsistingDeveloper(Developer existingContent)
        {
            bool deleteResult = _developerContent.Remove(existingContent);
            return deleteResult;
        }
    }
}
    


    