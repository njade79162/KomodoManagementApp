using Komodo_Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Komodo_Console
{
    class ProgramUI
    {
        protected readonly DeveloperRepo _devRepo = new DeveloperRepo();
        protected readonly DevTeamRepo _devTeamRepo = new DevTeamRepo();

        public void InitializeDevsAndTeams()
        {
            _devRepo.AddDeveloperToDirectory(new Developer("John Smith", 58374, true));
            _devRepo.AddDeveloperToDirectory(new Developer("Bob Parr", 42918, false));
            _devRepo.AddDeveloperToDirectory(new Developer("Billy Kidd", 38913, false));
            _devRepo.AddDeveloperToDirectory(new Developer("Mario Mario", 94234, true));

            List<Developer> teamOneMembers = new List<Developer>();
            teamOneMembers.Add(_devRepo.GetDeveloper("John Smith"));
            teamOneMembers.Add(_devRepo.GetDeveloper("Bob Parr"));

            List<Developer> teamTwoMembers = new List<Developer>();
            teamTwoMembers.Add(_devRepo.GetDeveloper(38913));
            teamTwoMembers.Add(_devRepo.GetDeveloper(94234));

            _devTeamRepo.CreateDevTeam(new DevTeam(teamOneMembers, "Team One", 8243678));
            _devTeamRepo.CreateDevTeam(new DevTeam(teamTwoMembers, "Team Two", 4019234));
        }

        // method that lists all developers
        public void ListDevelopers(List<Developer> devList)
        {
            int count = 1;
            foreach (Developer dev in devList)
            {
                Console.WriteLine($"Dev {count}: \n" +
                    $"Name: {dev.Name} \n" +
                    $"ID Number: {dev.IDNum} \n" +
                    $"");
                count++;
            }
        }

        public void ListDevTeams()
        {
            int count = 1;
            foreach (DevTeam team in _devTeamRepo.GetDevTeamDirectory())
            {
                Console.WriteLine($"Team {count}: \n" +
                    $"Team Name: {team.TeamName} \n" +
                    $"Team ID: {team.TeamID} \n" +
                    $"Team Members: \n" +
                    $"");
                ListDevelopers(team.TeamMembers);
                Console.WriteLine("------------------------------------");
                Console.WriteLine("");
                count++;
            }
        }

        public void PressAnyKey()
        {
            Console.WriteLine("Press any key to continue....");
            Console.ReadKey();
            Console.WriteLine();
        }
        
        public void InvalidValueMessage()
        {
            Console.WriteLine("That is not one of the accepted options. Please try again.");
            PressAnyKey();
        }

        public DevTeam GetTeam()
        {
            Console.WriteLine("Teams: ");
            Console.WriteLine();
            ListDevTeams();
            Console.WriteLine("Please enter the number of the team you would like to edit, or enter 0 to make a new team.");
            string input = Console.ReadLine();
            int intInput;
            bool isNum = Int32.TryParse(input, out intInput);
            if (!isNum)
            {
                Console.WriteLine("That was not a number. Please try again.");
                PressAnyKey();
                return null;
            }
            else if (intInput > _devTeamRepo.GetDevTeamDirectory().Count || intInput < 0)
            {
                Console.WriteLine("That was not a valid number. Please try again.");
                PressAnyKey();
            }
            else if(intInput == 0)
            {
                Console.WriteLine("Please enter a name for this new team.");
                string teamName = Console.ReadLine();
                Console.WriteLine();
                Console.WriteLine("Please enter an ID number for this new team.");
                string idNumString = Console.ReadLine();
                int idNum;
                Console.WriteLine();
                bool validNum = Int32.TryParse(idNumString, out idNum);
                if (!validNum)
                {
                    Console.WriteLine("That was not a valid ID number. Please try again.");
                    PressAnyKey();
                    return null;
                }
                List<Developer> teamMembers = new List<Developer>();
                DevTeam team = new DevTeam(teamMembers, teamName, idNum);
                _devTeamRepo.CreateDevTeam(team);
                Console.WriteLine($"Team {teamName} was created with ID {idNum}");
            }
            else
            {
                DevTeam team = _devTeamRepo.GetDevTeamDirectory()[intInput - 1];
                bool confirmRun = true;
                while (confirmRun)
                {
                    Console.WriteLine($"You have selected the team named {team.TeamName} with an ID of {team.TeamID}. Is this correct? \n" +
                        $"1: Yes \n" +
                        $"2: No");
                    Console.WriteLine();
                    string confirmInput = Console.ReadLine();
                    if (confirmInput != "1" && confirmInput != "2")
                    {
                        Console.WriteLine("That is not a valid answer. Please try again.");
                        PressAnyKey();
                    }
                    else if (confirmInput == "1")
                    {
                        PressAnyKey();
                        return team;
                    }
                    else if (confirmInput == "2")
                    {
                        Console.WriteLine("Restarting....");
                        PressAnyKey();
                        return null;
                    }
                }
            }
            return null;
        }

        public int ChooseAddOrRemove()
        {
            // ask user if they want to add or remove developers
            bool isCorrectValue = false;
            int intInput = 0;
            while (!isCorrectValue)
            {
                Console.WriteLine("Would you like to add or remove developers from this team? Please enter 1 or 2");
                Console.WriteLine("1: Add Developers \n" +
                    "2: Remove Developers \n" +
                    "3: Restart");
                string input = Console.ReadLine();
                Console.WriteLine();
                isCorrectValue = Int32.TryParse(input, out intInput);
                if (!isCorrectValue)
                {
                    InvalidValueMessage();
                }
                else if (intInput > 3 || intInput < 1)
                {
                    isCorrectValue = false;
                    InvalidValueMessage();
                }
            }
            return intInput;
        }

        public int ChooseSearchMethod()
        {

            // ask user if they want to search by name, ID, or both, and also allow them to see the full list of developers
            Console.WriteLine("Would you like to find a developer by name, ID, or both?");
            Console.WriteLine("1: Find by name \n" +
                "2: Find by ID \n" +
                "3: Find by both \n" +
                "4: List all developers");
            string input = Console.ReadLine();
            Console.WriteLine();
            int intInput;
            bool CorrectValue = Int32.TryParse(input, out intInput);
            if (!CorrectValue)
            {
                InvalidValueMessage();
            }
            else
            {
                return intInput;
            }

            return 0;
        }

        // search by name to add
        public void AddByName(DevTeam team)
        {
            Console.WriteLine("Please enter the name of the developer you would like to add");
            string input = Console.ReadLine();
            Console.WriteLine();
            bool alreadyOnTeam = false;
            Developer addedDev = _devRepo.GetDeveloper(input);

            if (addedDev != null)
            {
                // check if that developer is already on the team
                foreach (Developer dev in team.TeamMembers)
                {
                    if (dev.Name.ToLower() == addedDev.Name.ToLower())
                    {
                        alreadyOnTeam = true;
                        Console.WriteLine("That developer is already on this team. Please try again.");
                        PressAnyKey();
                        break;
                    }
                }

                // if not, add them to the team
                if (!alreadyOnTeam)
                {
                    Console.WriteLine($"Adding {addedDev.Name} to {team.TeamName}");
                    Console.WriteLine();
                    team.AddDeveloperToTeam(addedDev);
                }
            }
            // exit if that developer does not exist
            else
            {
                Console.WriteLine($"There is no developer with the name {input} in the directory. Please try again.");
                PressAnyKey();
            }
        }

        // search by ID to add
        public void AddByID(DevTeam team)
        {
            Console.WriteLine("Please enter the ID of the developer you would like to add");
            string input = Console.ReadLine();
            Console.WriteLine();
            bool alreadyOnTeam = false;
            Developer addedDev = null;
            int inputID = 0;
            bool isCorrectID = Int32.TryParse(input, out inputID);
            if (isCorrectID)
            {
                addedDev = _devRepo.GetDeveloper(inputID);
            }
            else
            {
                Console.WriteLine($"{input} was not recognized as a valid ID number. Please try again.");
                PressAnyKey();
            }

            if (addedDev != null)
            {
                // check if dev with that ID number is already on the team
                foreach (Developer dev in team.TeamMembers)
                {
                    if (dev.IDNum == addedDev.IDNum)
                    {
                        alreadyOnTeam = true;
                        Console.WriteLine("That developer is already on this team. Please try again.");
                        PressAnyKey();
                        break;
                    }
                }

                // add developer to team
                if (!alreadyOnTeam)
                {
                    Console.WriteLine($"Adding {addedDev.Name} to {team.TeamName}");
                    team.AddDeveloperToTeam(addedDev);
                }
            }
            // if dev with that ID number does not exist
            else if (isCorrectID && addedDev == null)
            {
                Console.WriteLine($"There is no developer with the ID {inputID} in the directory. Please try again.");
                PressAnyKey();
            }
        }

        public void AddByNameAndID(DevTeam team)
        {
            Console.WriteLine("Please enter the name of the developer you would like to add");
            string nameInput = Console.ReadLine();
            Console.WriteLine();
            Console.WriteLine("Please enter the ID of the developer you would like to add");
            string idInput = Console.ReadLine();
            Console.WriteLine();
            bool alreadyOnTeam = false;
            Developer addedDev = null;
            int inputID = 0;
            bool isCorrectID = Int32.TryParse(idInput, out inputID);
            if (isCorrectID)
            {
                addedDev = _devRepo.GetDeveloper(nameInput, inputID);
            }
            else
            {
                Console.WriteLine($"{idInput} was not recognized as a valid ID number. Please try again.");
                PressAnyKey();
            }

            if (addedDev != null)
            {
                foreach (Developer dev in team.TeamMembers)
                {
                    if (dev.Name == addedDev.Name && dev.IDNum == addedDev.IDNum)
                    {
                        alreadyOnTeam = true;
                        Console.WriteLine("That developer is already on this team. Please try again.");
                        PressAnyKey();
                        break;
                    }
                }

                if (!alreadyOnTeam)
                {
                    Console.WriteLine($"Adding {addedDev.Name} to {team.TeamName}");
                    Console.WriteLine();
                    team.AddDeveloperToTeam(addedDev);
                }
            }
            else if (addedDev == null && isCorrectID)
            {
                Console.WriteLine($"There is no developer with the name {nameInput} and the ID {inputID} in the directory. Please try again.");
                PressAnyKey();
            }
        }

        public bool ContinueAdd()
        {
            Console.WriteLine("Would you like to continue adding developers?");
            Console.WriteLine("1: Yes \n" +
                "2: No");
            bool invalidValue = true;
            string input = Console.ReadLine();
            Console.WriteLine();
            while (invalidValue)
            {
                switch (input)
                {
                    case "1":
                        Console.WriteLine("Returning to the beginning of the adding process....");
                        PressAnyKey();
                        return true;
                    case "2":
                        Console.WriteLine("Returning to the beginning of the program....");
                        PressAnyKey();
                        return false;
                    default:
                        InvalidValueMessage();
                        break;
                }
            }
            return false;
        }

        // search by name to remove
        public void RemoveByName(DevTeam team)
        {
            Console.WriteLine("Please enter the name of the developer you would like to remove");
            string input = Console.ReadLine();
            Console.WriteLine();
            bool alreadyOnTeam = false;
            Developer removedDev = _devRepo.GetDeveloper(input);

            // make sure the dev is on the team already
            if (removedDev != null)
            {
                // check if that developer is already on the team
                foreach (Developer dev in team.TeamMembers)
                {
                    if (dev.Name.ToLower() == removedDev.Name.ToLower())
                    {
                        alreadyOnTeam = true;
                        break;
                    }
                }

                if (!alreadyOnTeam)
                {
                    Console.WriteLine($"There is no developer with the name {input} on this team. Please try again.");
                    PressAnyKey();
                }
                else
                {
                    team.RemoveDeveloperFromTeam(removedDev);
                    Console.WriteLine($"{removedDev.Name} has been removed from {team.TeamName}");
                    PressAnyKey();
                }
            }
            else
            {
                Console.WriteLine($"There is no developer with the name {input}. Please try again.");
                PressAnyKey();
            }
        }

        public void RemoveByID(DevTeam team)
        {
            Console.WriteLine("Please enter the ID of the developer you would like to remove");
            string input = Console.ReadLine();
            Console.WriteLine();
            bool alreadyOnTeam = false;
            Developer removedDev = null;
            int inputID = 0;
            bool isCorrectID = Int32.TryParse(input, out inputID);
            if (isCorrectID)
            {
                removedDev = _devRepo.GetDeveloper(inputID);
            }
            else
            {
                Console.WriteLine($"{input} was not recognized as a valid ID number. Please try again.");
                PressAnyKey();
            }

            if (removedDev != null)
            {
                // check if that developer is already on the team
                foreach (Developer dev in team.TeamMembers)
                {
                    if (dev.Name.ToLower() == removedDev.Name.ToLower())
                    {
                        alreadyOnTeam = true;
                        break;
                    }
                }
                if (!alreadyOnTeam)
                {
                    Console.WriteLine($"There is no developer with the ID {inputID} on this team. Please try again.");
                    PressAnyKey();
                }
                else
                {
                    team.RemoveDeveloperFromTeam(removedDev);
                    Console.WriteLine($"{removedDev.Name} has been removed from {team.TeamName}");
                    PressAnyKey();
                }
            }
            else
            {
                Console.WriteLine($"There is no developer with the ID {inputID}. Please try again.");
                PressAnyKey();
            }
        }

        public void RemoveByNameAndID(DevTeam team)
        {
            Console.WriteLine("Please enter the name of the developer you would like to add");
            string nameInput = Console.ReadLine();
            Console.WriteLine();
            Console.WriteLine("Please enter the ID of the developer you would like to add");
            string idInput = Console.ReadLine();
            Console.WriteLine();
            bool alreadyOnTeam = false;
            Developer removedDev = null;
            int inputID = 0;
            bool isCorrectID = Int32.TryParse(idInput, out inputID);
            if (isCorrectID)
            {
                removedDev = _devRepo.GetDeveloper(nameInput, inputID);
            }
            else
            {
                Console.WriteLine($"{idInput} was not recognized as a valid ID number. Please try again.");
                PressAnyKey();
            }

            if (removedDev != null)
            {
                // check if that developer is already on the team
                foreach (Developer dev in team.TeamMembers)
                {
                    if (dev.Name.ToLower() == removedDev.Name.ToLower())
                    {
                        alreadyOnTeam = true;
                        break;
                    }
                }
                if (!alreadyOnTeam)
                {
                    Console.WriteLine($"There is no developer with the name {nameInput} ID {inputID} on this team. Please try again.");
                    PressAnyKey();
                }
                else
                {
                    team.RemoveDeveloperFromTeam(removedDev);
                    Console.WriteLine($"{removedDev.Name} has been removed from {team.TeamName}");
                    PressAnyKey();
                }
            }
            else
            {
                Console.WriteLine($"There is no developer with the name {nameInput} ID {inputID}. Please try again.");
                PressAnyKey();
            }
        }

        public bool ContinueRemove()
        {
            Console.WriteLine("Would you like to continue removing developers?");
            Console.WriteLine("1: Yes \n" +
                "2: No");
            bool invalidValue = true;
            string input = Console.ReadLine();
            Console.WriteLine();
            while (invalidValue)
            {
                switch (input)
                {
                    case "1":
                        Console.WriteLine("Returning to the beginning of the removing process....");
                        PressAnyKey();
                        return true;
                    case "2":
                        Console.WriteLine("Returning to the beginning of the program....");
                        PressAnyKey();
                        return false;
                    default:
                        InvalidValueMessage();
                        break;
                }
            }
            return false;
        }

        // method that specifically lists developers that don't have access to pluralsight

        public void Run()
        {
            InitializeDevsAndTeams();
            bool run = true;
            DevTeam team = null;
            int addOrRemove = 0;
            int searchMethod = 0;
            bool continueProcess = true;
            while (run)
            {
                continueProcess = true;
                team = GetTeam();
                if (team == null)
                {
                    continue;
                }
                addOrRemove = ChooseAddOrRemove();
                switch (addOrRemove)
                {
                    case 1:
                        while (continueProcess)
                        {
                            searchMethod = ChooseSearchMethod();
                            switch (searchMethod)
                            {
                                case 1:
                                    AddByName(team);
                                    break;
                                case 2:
                                    AddByID(team);
                                    break;
                                case 3:
                                    AddByNameAndID(team);
                                    break;
                                case 4:
                                    ListDevelopers(_devRepo.GetDevDirectory());
                                    PressAnyKey();
                                    break;
                            }
                            continueProcess = ContinueAdd();
                        }
                        break;
                    case 2:
                        while (continueProcess)
                        {
                            searchMethod = ChooseSearchMethod();
                            switch (searchMethod)
                            {
                                case 1:
                                    RemoveByName(team);
                                    break;
                                case 2:
                                    RemoveByID(team);
                                    break;
                                case 3:
                                    RemoveByNameAndID(team);
                                    break;
                                case 4:
                                    ListDevelopers(_devRepo.GetDevDirectory());
                                    PressAnyKey();
                                    break;
                            }
                            continueProcess = ContinueRemove();
                        }
                        break;
                    case 3:
                        Console.WriteLine("Returning to the start of the program....");
                        PressAnyKey();
                        continue;
                }
            }
        }
    }
}
