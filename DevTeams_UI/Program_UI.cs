using DevTeams_POCOs;
using DevTeams_Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace DevTeams_UI
{
    public class Program_UI
    {
        private readonly DeveloperRepository _devRepo;
        private readonly DevTeamsRepository _devTeamDirectory;

        public Program_UI()
        {
            _devRepo = new DeveloperRepository();
            _devTeamDirectory = new DevTeamsRepository(_devRepo);
        }
        public void Run()
        {
            Seed();
            RunApplication();
        }
        private void Seed()
        {
            Developer developerA = new Developer("Han", "Solo", true);
            Developer developerB = new Developer("Johnny", "Smith", true);
            Developer developerC = new Developer("Karen", "Applebottom", false);
            _devRepo.AddDeveloper(developerA);
            _devRepo.AddDeveloper(developerB);
            _devRepo.AddDeveloper(developerC);
        }
        private void RunApplication()
        {
            bool continueToRun = true;
            while (continueToRun)
            {
                WriteLine("Welcome to Dev Teams\n" +
                    "1. Add A Developer\n" +
                    "2. View All Existing Developers\n" +
                    "3. View An Existing Developer\n" +
                    "4. Update An Existing Developer\n" +
                    "5. Delete An Existing Developer\n" +
                    "6. Add a new Dev Team\n" +
                    "7. View All Existing Dev Teams\n" +
                    "8. View An existing Dev Team\n" +
                    "9. Update An existing Dev Team\n" +
                    "10. Delete An existing Dev Team\n" +
                    "50. Exit");

                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "1":
                        AddDeveloper();
                        break;
                    case "2":
                        ViewAllExistingDevelopers();
                        break;
                    case "3":
                        ViewAnExistingDeveloper();
                        break;
                    case "4":
                        UpDateAnExistingDeveloper();
                        break;
                    case "5":
                        DeleteAnExistingDeveloper();
                        break;
                    case "6":
                        AddDevTeam();
                        break;
                    case "7":
                        ViewAllExistingDevTeams();
                        break;
                    case "8":
                        ViewExistingDevTeam();
                        break;
                    case "9":
                        UpdateAnExistingDevTeam();
                        break;
                    case "10":
                        DeleteAnExistingDevTeam();
                        break;
                    case "50":
                        continueToRun = false;
                        break;
                    default:
                        Console.WriteLine("Please enter a number between 1 and 5");
                        WaitForKeyPress();
                        break;
                }
                Console.Clear();
            }
        }
        private void ViewExistingDevTeam()
        {
            Console.Clear();
            Console.WriteLine("Please enter existing Dev Team ID");
            var userInput = int.Parse(Console.ReadLine());
            var foundDevTeam = _devTeamDirectory.GetDevTeamByID(userInput);
            if (foundDevTeam == null)
            {
                Console.WriteLine("Dev Team Not Found");
            }
            else
            {
                ViewDevTeamDetails(foundDevTeam);
            }
            Console.ReadKey();
        }
        private void DeleteAnExistingDevTeam()
        {
            Console.Clear();
            Console.WriteLine("Which Dev Team would you like to remove?");

            int index = 0;
            List<DevTeam> devTeam = _devTeamDirectory.GetContents();
            foreach (DevTeam item in devTeam)
            {
                Console.Write($"{index + 1}. ");
                ViewDevTeamDetails(item);
                index++;
            }
            string optionString = Console.ReadLine();
            int option = Convert.ToInt32(optionString);
            DevTeam itemToDelete = devTeam[option - 1];
            foreach (var dev in itemToDelete.Developers)
            {
                _devRepo.AddDeveloper(dev);
            }
            // OPTIONAL
            Console.WriteLine("Are you sure you want to remove this team? (y/n)");
            ViewDevTeamDetails(itemToDelete);
            if (Console.ReadLine() == "y")
            {
                _devTeamDirectory.DeleteExsistingDeveloperTeam(itemToDelete);
                Console.WriteLine("Team removed!");
            }
            else
            {
                Console.WriteLine("Canceled");
            }
        }
        private void UpdateAnExistingDevTeam()
        {
            Console.Clear();
            Console.WriteLine("Update an existing Dev Team: ");
            Console.WriteLine("Please input an existing Dev Team ID");
            var userInput = int.Parse(Console.ReadLine());
            var foundDevTeam = _devTeamDirectory.GetDevTeamByID(userInput);
            if (foundDevTeam != null)
            {
                Console.Clear();
                DevTeam devTeam = new DevTeam();
                Console.WriteLine("Please input the Dev Team Name");
                devTeam.Name = Console.ReadLine();

                bool success = _devTeamDirectory.UpdateExistingDeveloperTeam(userInput, devTeam);
                if (success)
                {
                    Console.WriteLine("SUCCESS");
                }
                else
                {
                    Console.WriteLine("FAIL");

                }
            }
            Console.ReadKey();
        }
        private void ViewAllExistingDevTeams()
        {
            Console.Clear();
            Console.WriteLine("View all Dev Teams: ");
            List<DevTeam> devTeams = _devTeamDirectory.GetContents();
            foreach (DevTeam devTeam in devTeams)

            {
                ViewDevTeamDetails(devTeam);
            }
            Console.ReadKey();
        }
        //passing in an entire DevTeam devTeam
        private void ViewDevTeamDetails(DevTeam devTeam)
        {
            //write out the specific devTeam.ID and devTeam.Name
            Console.WriteLine($"{devTeam.ID}\n" +
                              $"{devTeam.Name}");
            //loop through the devTeam.Developers collection
            //to print out the info for each individual devloper w/n the devTeam.Developers Collection
            foreach (var devs in devTeam.Developers)
            {
                Console.WriteLine($"{devs.ID} {devs.FullName}");
            }
            Console.WriteLine();
        }
        private void AddDevTeam()
        {
            Console.Clear();
            DevTeam devTeam = new DevTeam();
            Console.WriteLine("Please input the Developer Team Name");
            devTeam.Name = Console.ReadLine();

            List<Developer> developers = new List<Developer>();
            bool hasFilledPostions = false;
            while (!hasFilledPostions)
            {
                Console.Clear();
                List<Developer> devsToChoose = _devRepo.GetDevelopers();
                foreach (Developer dev in devsToChoose)
                {
                    Console.WriteLine($"{dev.ID} {dev.FullName}");
                }
                Console.WriteLine("Please select a Developer by ID");
                int userInputDevId = int.Parse(Console.ReadLine());
                Developer developer = _devRepo.GetDeveloperByID(userInputDevId);
                if (developer != null)
                {
                    devTeam.Developers.Add(developer);
                    devsToChoose.Remove(developer);
                    Console.WriteLine("Do you want to add another Developer y/n");
                    var userInput = Console.ReadLine();
                    if (userInput == "Y".ToLower())
                    {
                        continue;
                    }
                    else
                    {
                        hasFilledPostions = true;
                    }
                }
            }
            bool success = _devTeamDirectory.AddDeveloperTeamToDirectory(devTeam);
            if (success)
            {
                Console.WriteLine("SUCCESS");
            }
            else
            {
                Console.WriteLine("FAIL");

            }
            Console.ReadKey();
        }
        private void AddDeveloper()
        {
            Console.Clear();
            Developer developer = new Developer();
            Console.WriteLine("Please input the Developers First Name");
            developer.FirstName = Console.ReadLine();
            Console.WriteLine("Please input the Developers Last Name");
            developer.LastName = Console.ReadLine();
            Console.WriteLine("Does this person have Pluralsight? y/n");
            string userInput = Console.ReadLine();
            if (userInput == "Y".ToLower())
            {
                developer.HasPluralsight = true;
            }
            else
            {
                developer.HasPluralsight = false;
            }
            bool success = _devRepo.AddDeveloper(developer);
            if (success)
            {
                Console.WriteLine("SUCCESS");
            }
            else
            {
                Console.WriteLine("FAIL");

            }
            Console.ReadKey();
        }

        private void ViewAllExistingDevelopers()
        {
            Console.Clear();
            Console.WriteLine("Developers available: ");
            List<Developer> devTeam = _devRepo.GetDevelopers();
            foreach (Developer developer in devTeam)
            {
                {
                    DisplayContentListItem(developer);
                }
                {
                    WaitForKeyPress();
                }
                Console.ReadKey();
            }
        }
        private void ViewAnExistingDeveloper()
        {
            Console.Clear();
            Console.WriteLine("View a Developer: ");
            List<Developer> developerId = _devRepo.GetDevelopers();
            foreach (Developer developer in developerId)
            {

                DisplayContentListItem(developer);
            }
            Console.ReadKey();
        }
        private void UpDateAnExistingDeveloper()
        {
            Console.Clear();
            Console.WriteLine("Update a Developer: ");
            Console.WriteLine("Please input an existing Developer ID");
            var userInput = int.Parse(Console.ReadLine());
            var foundDeveloper = _devRepo.GetDeveloperByID(userInput);
            if (foundDeveloper != null)
            {
                Console.Clear();
                Developer developer = new Developer();
                Console.WriteLine("Please input the Developers First Name");
                developer.FirstName = Console.ReadLine();
                Console.WriteLine("Please input the Developers Last Name");
                developer.LastName = Console.ReadLine();
                Console.WriteLine("Does this person have Pluralsight? y/n");
                string userInput2 = Console.ReadLine();
                if (userInput2 == "Y".ToLower())
                {
                    developer.HasPluralsight = true;
                }
                else
                {
                    developer.HasPluralsight = false;
                }
                bool success = _devRepo.UpdateExistingDeveloper(userInput, developer);
                if (success)
                {
                    Console.WriteLine("SUCCESS");
                }
                else
                {
                    Console.WriteLine("FAIL");

                }
            }
            Console.ReadKey();
        }
        private void DeleteAnExistingDeveloper()
        {
            Console.Clear();
            Console.WriteLine("Which Developer would you like to remove?");

            int index = 0;
            List<Developer> developers = _devRepo.GetDevelopers();
            foreach (Developer item in developers)
            {
                Console.Write($"{index + 1}. ");
                DisplayContentListItem(item);
                index++;
            }
            string optionString = Console.ReadLine();
            int option = Convert.ToInt32(optionString);

            Developer itemToDelete = developers[option - 1];

            // OPTIONAL
            Console.WriteLine("Are you sure you want to remove this person? (y/n)");
            DisplayContentListItem(itemToDelete);
            if (Console.ReadLine() == "y")
            {
                _devRepo.DeleteExsistingDeveloper(itemToDelete);
                Console.WriteLine("Person removed!");
            }
            else
            {
                Console.WriteLine("Canceled");
            }
            WaitForKeyPress();

        }
        private void DisplayContentListItem(Developer item)
        {
            Console.WriteLine($"{item.ID} \n" +
                              $"{item.FullName}\n" +
                              $"{item.HasPluralsight}\n");
        }
        private void WaitForKeyPress()
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}




