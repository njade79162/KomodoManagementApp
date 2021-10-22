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
        // repositories of developers and dev teams
        protected readonly DeveloperRepo _devRepo = new DeveloperRepo();
        protected readonly DevTeamRepo _devTeamRepo = new DevTeamRepo();

        // creates initial developers and dev teams
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

        // lists all developers in a given list
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

        // lists all dev teams in the repository
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

        // reusable "press key to continue"
        public void PressAnyKey()
        {
            Console.WriteLine("Press any key to continue....");
            Console.ReadKey();
            Console.Clear();
        }

        // reusable error message
        public void InvalidValueMessage()
        {
            Console.WriteLine("That is not one of the accepted options. Please try again.");
            PressAnyKey();
        }

        // initial setup, ask what user wants to do
        public int InitialAction()
        {
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("1: Edit teams \n" +
                "2: Add developers to directory \n" +
                "3: List all developers \n" +
                "4: List developers that need a Pluralsight license \n" +
                "5: Exit");
            Console.WriteLine();
            string input = Console.ReadLine();
            Console.Clear();
            bool isNum = Int32.TryParse(input, out int intInput);
            if (!isNum)
            {
                InvalidValueMessage();
            }
            else if (intInput > 5 || intInput < 1)
            {
                InvalidValueMessage();
            }
            else
            {
                return intInput;
            }
            return 0;
        }

        // create a developer object and add it to the directory
        public void CreateDev()
        {
            Console.WriteLine("Please enter the new developer's name:");
            Console.WriteLine();
            string nameInput = Console.ReadLine();
            Console.Clear();
            bool isNum = false;
            int intId = 0;
            while (!isNum)
            {
                Console.WriteLine("Please enter the new developer's ID number:");
                Console.WriteLine();
                string idInput = Console.ReadLine();
                Console.Clear();
                isNum = Int32.TryParse(idInput, out int idInteger);
                if (!isNum)
                {
                    Console.WriteLine("This is not a valid number. Please try again.");
                    PressAnyKey();
                }
                intId = idInteger;
            }
            bool continueLoop = true;
            bool pluralsightAccess = false;
            while (continueLoop)
            {
                continueLoop = false;
                Console.WriteLine("Does this developer have access to Pluralsight? \n" +
                    "1: Yes \n" +
                    "2: No");
                Console.WriteLine();
                string accessInput = Console.ReadLine();
                Console.Clear();
                switch (accessInput)
                {
                    case "1":
                        pluralsightAccess = true;
                        break;
                    case "2":
                        pluralsightAccess = false;
                        break;
                    default:
                        InvalidValueMessage();
                        continueLoop = true;
                        break;
                }
            }
            Developer dev = new Developer(nameInput, intId, pluralsightAccess);
            Console.WriteLine($"Is all of this info correct? \n" +
                $"\n" +
                $"Name: {dev.Name} \n" +
                $"ID Number: {dev.IDNum} \n" +
                $"Pluralsight Access: {dev.PluralsightAccess}");
            Console.WriteLine();
            Console.WriteLine("1: Yes \n" +
                "2: No");
            Console.WriteLine();
            string confirmInput = Console.ReadLine();
            Console.Clear();
            bool incorrectInput = true;
            while (incorrectInput)
            {
                incorrectInput = false;
                switch (confirmInput)
                {
                    case "1":
                        _devRepo.AddDeveloperToDirectory(dev);
                        Console.WriteLine("Added this developer to the directory.");
                        PressAnyKey();
                        break;
                    case "2":
                        Console.WriteLine("Returning to the start of the program....");
                        PressAnyKey();
                        break;
                    default:
                        InvalidValueMessage();
                        incorrectInput = true;
                        break;
                }
            }
        }

        // list developers that need a pluralsight license
        public void ListDevelopersWithoutPluralsight()
        {
            List<Developer> PluralsightNeeded = new List<Developer>();
            foreach(Developer dev in _devRepo.GetDevDirectory())
            {
                if (dev.PluralsightAccess == false)
                {
                    PluralsightNeeded.Add(dev);
                }
            }
            Console.WriteLine("The following developers need a Pluralsight license:");
            Console.WriteLine();
            foreach(Developer dev in PluralsightNeeded)
            {
                Console.WriteLine($"Name: {dev.Name}");
                Console.WriteLine($"ID Number: {dev.IDNum}");
                Console.WriteLine();
            }
        }

        // ask user what team they want to edit or allows them to make a new team
        public DevTeam GetTeam()
        {
            bool continueRun = true;
            while (continueRun)
            {
                Console.WriteLine("Teams: ");
                Console.WriteLine();
                ListDevTeams();
                Console.WriteLine($"Please enter the number of the team you would like to edit, or enter 0 to make a new team. Enter {_devTeamRepo.GetDevTeamDirectory().Count + 1} to exit.");
                Console.WriteLine();
                string input = Console.ReadLine();
                Console.Clear();
                int intInput;
                bool isNum = Int32.TryParse(input, out intInput);
                if (!isNum)
                {
                    Console.WriteLine("That was not a number. Please try again.");
                    PressAnyKey();
                }
                else if (intInput > _devTeamRepo.GetDevTeamDirectory().Count + 1 || intInput < 0)
                {
                    Console.WriteLine("That was not a valid number. Please try again.");
                    PressAnyKey();
                }
                else if (intInput == 0)
                {
                    continueRun = false;
                    Console.WriteLine("Please enter a name for this new team.");
                    Console.WriteLine();
                    string teamName = Console.ReadLine();
                    Console.Clear();
                    int idNum = 0;
                    bool validNum = false;
                    while (!validNum)
                    {
                        Console.WriteLine("Please enter an ID number for this new team.");
                        Console.WriteLine();
                        string idNumString = Console.ReadLine();
                        Console.Clear();
                        validNum = Int32.TryParse(idNumString, out int numID);
                        if (!validNum)
                        {
                            Console.WriteLine("That was not a valid ID number. Please try again.");
                            PressAnyKey();
                        }
                        idNum = numID;
                    }
                    List<Developer> teamMembers = new List<Developer>();
                    DevTeam team = new DevTeam(teamMembers, teamName, idNum);
                    _devTeamRepo.CreateDevTeam(team);
                    Console.WriteLine($"A team named {teamName} was created with ID {idNum}");
                    PressAnyKey();
                }
                else if (intInput == _devTeamRepo.GetDevTeamDirectory().Count + 1)
                {
                    Console.WriteLine("Exiting to beginning of program....");
                    PressAnyKey();
                    return null;
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
                        Console.Clear();
                        if (confirmInput != "1" && confirmInput != "2")
                        {
                            Console.WriteLine("That is not a valid answer. Please try again.");
                            PressAnyKey();
                        }
                        else if (confirmInput == "1")
                        {
                            Console.WriteLine($"{team.TeamName} selected.");
                            PressAnyKey();
                            return team;
                        }
                        else if (confirmInput == "2")
                        {
                            Console.WriteLine("Exiting to beginning of program....");
                            PressAnyKey();
                            return null;
                        }
                    }
                }
            }
            return null;
        }

        // ask user if they want to add devs to team or remove devs from team
        public int ChooseAddOrRemove()
        {
            bool isCorrectValue = false;
            int intInput = 0;
            while (!isCorrectValue)
            {
                Console.WriteLine("Would you like to add or remove developers from this team? Please enter 1 or 2");
                Console.WriteLine("1: Add Developers \n" +
                    "2: Remove Developers \n" +
                    "3: Restart");
                Console.WriteLine();
                string input = Console.ReadLine();
                Console.Clear();
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

        // select dev(s) to add to team
        public void AddDeveloper(DevTeam team)
        {
            Console.WriteLine("Would you like to add one developer to this team, or multiple at once?");
            Console.WriteLine("1: One Developer \n" +
                "2: Multiple Developers \n" +
                "3: Exit");
            Console.WriteLine();
            string input = Console.ReadLine();
            Console.Clear();
            switch (input)
            {
                case "1":
                    ListDevelopers(_devRepo.GetDevDirectory());
                    Console.WriteLine();
                    Console.WriteLine("Please enter the number corresponding to the developer you would like to add.");
                    Console.WriteLine();
                    string devChoice = Console.ReadLine();
                    Console.Clear();
                    bool isNumber = Int32.TryParse(devChoice, out int devChoiceInt);
                    if (!isNumber)
                    {
                        Console.WriteLine("That is not a valid number. Please try again.");
                        PressAnyKey();
                        break;
                    }
                    else if (devChoiceInt > _devRepo.GetDevDirectory().Count || devChoiceInt < 1)
                    {
                        Console.WriteLine("That is not a valid number. Please try again.");
                        PressAnyKey();
                        break;
                    }
                    else
                    {
                        bool isOnTeam = false;
                        foreach (Developer dev in team.TeamMembers)
                        {
                            if (dev.Name == _devRepo.GetDevDirectory()[devChoiceInt - 1].Name && dev.IDNum == _devRepo.GetDevDirectory()[devChoiceInt - 1].IDNum)
                            {
                                isOnTeam = true;
                            }
                        }
                        if (isOnTeam)
                        {
                            Console.WriteLine("This developer is already on this team. Please try again.");
                            PressAnyKey();
                            break;
                        }
                        else
                        {
                            Console.WriteLine($"Adding {_devRepo.GetDevDirectory()[devChoiceInt - 1].Name} to the team {team.TeamName}");
                            team.AddDeveloperToTeam(_devRepo.GetDevDirectory()[devChoiceInt - 1]);
                            PressAnyKey();
                            break;
                        }
                    }
                case "2":
                    string devInput = "1";
                    List<int> devInputs = new List<int>();
                    while (devInput != "0")
                    {
                        ListDevelopers(_devRepo.GetDevDirectory());
                        Console.WriteLine();
                        Console.WriteLine("Developers to be added: ");
                        Console.WriteLine();
                        foreach (int dev in devInputs)
                        {
                            Console.WriteLine(_devRepo.GetDevDirectory()[dev - 1].Name);
                        }
                        Console.WriteLine();
                        Console.WriteLine("Please enter the number corresponding to the developer you would like to add, or input 0 to finish.");
                        Console.WriteLine();
                        devInput = Console.ReadLine();
                        Console.Clear();
                        bool isNum = Int32.TryParse(devInput, out int intDevInput);
                        if (!isNum)
                        {
                            Console.WriteLine("That is not a valid number. Please try again.");
                            PressAnyKey();
                            continue;
                        }
                        else if (intDevInput > _devRepo.GetDevDirectory().Count || intDevInput < 0)
                        {
                            Console.WriteLine("That is not a valid number. Please try again.");
                            PressAnyKey();
                            continue;
                        }
                        else if (intDevInput != 0)
                        {
                            bool isOnTeam = false;
                            bool isAlreadySelected = false;
                            foreach (Developer dev in team.TeamMembers)
                            {
                                if (dev.Name == _devRepo.GetDevDirectory()[intDevInput - 1].Name && dev.IDNum == _devRepo.GetDevDirectory()[intDevInput - 1].IDNum)
                                {
                                    isOnTeam = true;
                                }
                            }
                            foreach (int num in devInputs)
                            {
                                if (num == intDevInput)
                                {
                                    isAlreadySelected = true;
                                }
                            }
                            if (isOnTeam)
                            {
                                Console.WriteLine("That developer is already on this team. Please try again.");
                                PressAnyKey();
                                continue;
                            }
                            else if (isAlreadySelected)
                            {
                                Console.WriteLine("You have already selected that developer to be added. Please try again.");
                                PressAnyKey();
                                continue;
                            }
                            else
                            {
                                devInputs.Add(intDevInput);
                                Console.WriteLine($"You selected {_devRepo.GetDevDirectory()[intDevInput - 1].Name}");
                                PressAnyKey();
                            }
                        }
                    }
                    Console.WriteLine($"The final list of developers to be added to {team.TeamName} is as follows: ");
                    Console.WriteLine();
                    foreach (int dev in devInputs)
                    {
                        Console.WriteLine(_devRepo.GetDevDirectory()[dev - 1].Name);
                    }
                    Console.WriteLine();
                    Console.WriteLine("Is this correct?");
                    Console.WriteLine("1: Yes \n" +
                        "2: No");
                    Console.WriteLine();
                    input = Console.ReadLine();
                    Console.Clear();
                    bool continueLoop = true;
                    while (continueLoop)
                    {
                        if (input != "1" && input != "2")
                        {
                            Console.WriteLine("That is not a valid number. Please try again.");
                            PressAnyKey();
                        }
                        else if (input == "2")
                        {
                            Console.WriteLine("Exiting the adding process....");
                            PressAnyKey();
                            break;
                        }
                        else
                        {
                            foreach (int dev in devInputs)
                            {
                                Developer developer = _devRepo.GetDevDirectory()[dev - 1];
                                team.AddDeveloperToTeam(developer);
                            }

                            Console.WriteLine($"Added these developers to {team.TeamName}");
                            PressAnyKey();
                            break;
                        }
                    }
                    break;
                case "3":
                    Console.WriteLine("Exiting the adding process....");
                    PressAnyKey();
                    break;
                default:
                    InvalidValueMessage();
                    PressAnyKey();
                    break;
            }
        }

        // ask if user wants to continue adding devs
        public bool ContinueAdd()
        {
            Console.WriteLine("Would you like to continue adding developers?");
            Console.WriteLine("1: Yes \n" +
                "2: No");
            bool invalidValue = true;
            Console.WriteLine();
            string input = Console.ReadLine();
            Console.Clear();
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

        // remove dev(s) from team
        public void RemoveDeveloper(DevTeam team)
        {
            Console.WriteLine("Would you like to remove one developer to this team, or multiple at once?");
            Console.WriteLine("1: One Developer \n" +
                "2: Multiple Developers \n" +
                "3: Exit");
            Console.WriteLine();
            string input = Console.ReadLine();
            Console.Clear();
            switch (input)
            {
                case "1":
                    ListDevelopers(team.TeamMembers);
                    Console.WriteLine();
                    Console.WriteLine("Please enter the number corresponding to the developer you would like to remove.");
                    Console.WriteLine();
                    string devChoice = Console.ReadLine();
                    Console.Clear();
                    bool isNumber = Int32.TryParse(devChoice, out int devChoiceInt);
                    if (!isNumber)
                    {
                        Console.WriteLine("That is not a valid number. Please try again.");
                        PressAnyKey();
                        break;
                    }
                    else if (devChoiceInt > team.TeamMembers.Count || devChoiceInt < 1)
                    {
                        Console.WriteLine("That is not a valid number. Please try again.");
                        PressAnyKey();
                        break;
                    }
                    else
                    {
                        Console.WriteLine($"Removing {team.TeamMembers[devChoiceInt - 1].Name} from the team {team.TeamName}");
                        team.RemoveDeveloperFromTeam(team.TeamMembers[devChoiceInt - 1]);
                        PressAnyKey();
                        break;
                    }
                case "2":
                    string devInput = "1";
                    List<int> devInputs = new List<int>();
                    while (devInput != "0")
                    {
                        ListDevelopers(team.TeamMembers);
                        Console.WriteLine();
                        Console.WriteLine("Developers to be removed: ");
                        Console.WriteLine();
                        foreach (int dev in devInputs)
                        {
                            Console.WriteLine(team.TeamMembers[dev - 1].Name);
                        }
                        Console.WriteLine();
                        Console.WriteLine("Please enter the number corresponding to the developer you would like to remove, or input 0 to finish.");
                        Console.WriteLine();
                        devInput = Console.ReadLine();
                        Console.Clear();
                        bool isNum = Int32.TryParse(devInput, out int intDevInput);
                        if (!isNum)
                        {
                            Console.WriteLine("That is not a valid number. Please try again.");
                            PressAnyKey();
                            continue;
                        }
                        else if (intDevInput > team.TeamMembers.Count || intDevInput < 0)
                        {
                            Console.WriteLine("That is not a valid number. Please try again.");
                            PressAnyKey();
                            continue;
                        }
                        else if (intDevInput != 0)
                        {
                            bool isAlreadySelected = false;
                            foreach (int num in devInputs)
                            {
                                if (num == intDevInput)
                                {
                                    isAlreadySelected = true;
                                }
                            }
                            if (isAlreadySelected)
                            {
                                Console.WriteLine("You have already selected that developer to be removed. Please try again.");
                                PressAnyKey();
                                continue;
                            }
                            else
                            {
                                devInputs.Add(intDevInput);
                                Console.WriteLine($"You selected {team.TeamMembers[intDevInput - 1].Name}");
                                PressAnyKey();
                            }
                        }
                    }
                    Console.WriteLine($"The final list of developers to be removed from {team.TeamName} is as follows: ");
                    Console.WriteLine();
                    foreach (int dev in devInputs)
                    {
                        Console.WriteLine(team.TeamMembers[dev - 1].Name);
                    }
                    Console.WriteLine();
                    Console.WriteLine("Is this correct?");
                    Console.WriteLine("1: Yes \n" +
                        "2: No");
                    Console.WriteLine();
                    input = Console.ReadLine();
                    Console.Clear();
                    bool continueLoop = true;
                    while (continueLoop)
                    {
                        if (input != "1" && input != "2")
                        {
                            Console.WriteLine("That is not a valid number. Please try again.");
                            PressAnyKey();
                        }
                        else if (input == "2")
                        {
                            Console.WriteLine("Exiting the removing process....");
                            PressAnyKey();
                            break;
                        }
                        else
                        {
                            foreach (int dev in devInputs)
                            {
                                List<Developer> originalTeam = team.TeamMembers;
                                Developer developer = team.TeamMembers[dev - 1];
                                team.RemoveDeveloperFromTeam(developer);
                            }

                            Console.WriteLine($"Removed these developers from {team.TeamName}");
                            PressAnyKey();
                            break;
                        }
                    }
                    break;
                case "3":
                    Console.WriteLine("Exiting the removing process....");
                    PressAnyKey();
                    break;
                default:
                    InvalidValueMessage();
                    PressAnyKey();
                    break;
            }
        }

        // ask if user wants to continue removing devs
        public bool ContinueRemove()
        {
            Console.WriteLine("Would you like to continue removing developers?");
            Console.WriteLine("1: Yes \n" +
                "2: No");
            bool invalidValue = true;
            Console.WriteLine();
            string input = Console.ReadLine();
            Console.Clear();
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

        // method to run everything
        public void Run()
        {
            InitializeDevsAndTeams();
            bool run = true;
            bool editTeam = true;
            DevTeam team = null;
            int addOrRemove = 0;
            bool continueProcess = true;
            while (run)
            {
                editTeam = true;
                int action = InitialAction();
                switch (action)
                {
                    case 1:
                        while (editTeam)
                        {
                            continueProcess = true;
                            team = GetTeam();
                            if (team == null)
                            {
                                editTeam = false;
                                continue;
                            }
                            addOrRemove = ChooseAddOrRemove();
                            switch (addOrRemove)
                            {
                                case 1:
                                    while (continueProcess)
                                    {
                                        AddDeveloper(team);
                                        continueProcess = ContinueAdd();
                                        editTeam = continueProcess;
                                    }
                                    break;
                                case 2:
                                    while (continueProcess)
                                    {
                                        RemoveDeveloper(team);
                                        continueProcess = ContinueRemove();
                                        editTeam = continueProcess;
                                    }
                                    break;
                                case 3:
                                    Console.WriteLine("Returning to the start of the program....");
                                    PressAnyKey();
                                    editTeam = false;
                                    continue;
                            }
                        }
                        break;
                    case 2:
                        CreateDev();
                        break;
                    case 3:
                        ListDevelopers(_devRepo.GetDevDirectory());
                        PressAnyKey();
                        break;
                    case 4:
                        ListDevelopersWithoutPluralsight();
                        PressAnyKey();
                        break;
                    case 5:
                        run = false;
                        break;
                }
            }
            Console.WriteLine("Shutting down....");
            PressAnyKey();
        }
    }
}
