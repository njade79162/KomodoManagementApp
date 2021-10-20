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

        // method that lists all developers
        public void ListDevelopers(List<Developer> devList)
        {
            foreach (Developer dev in devList)
            {
                Console.WriteLine($"Name: {dev.Name} \n" +
                    $"ID Number: {dev.IDNum} \n" +
                    $"");
            }
        }

        public void ListDevTeams()
        {
            foreach (DevTeam team in _devTeamRepo.GetDevTeamDirectory())
            {
                Console.WriteLine($"Team Name: {team.TeamName} \n" +
                    $"Team ID: {team.TeamID} \n" +
                    $"Team Members: ");
                ListDevelopers(team.TeamMembers);
            }
        }

        public DevTeam getTeam()
        {
            Console.WriteLine("Please enter the team name or ID of the team you would like to edit.");
            ListDevTeams();
            string teamIdentifier = Console.ReadLine();
            int teamIDNum = 0;
            bool isName = false;
            bool isID = false;
            switch (int.TryParse(teamIdentifier, out teamIDNum))
            {
                case true:
                    foreach (DevTeam team in _devTeamRepo.GetDevTeamDirectory())
                    {
                        if (team.TeamID == teamIDNum)
                        {
                            isID = true;
                        }
                    }

                    if (isID)
                    {
                        return _devTeamRepo.GetDevTeam(teamIDNum);
                    }
                    else
                    {
                        Console.WriteLine("That ID does not match the ID of any existing teams. Please try again.");
                        Console.WriteLine("Press any key to continue....");
                        Console.ReadKey();
                        return null;
                    }
                case false:
                    foreach (DevTeam team in _devTeamRepo.GetDevTeamDirectory())
                    {
                        if (team.TeamName == teamIdentifier)
                        {
                            isName = true;
                        }
                    }
                    break;
            }

            switch (isName)
            {
                case true:
                    return _devTeamRepo.GetDevTeam(teamIdentifier);
                case false:
                    Console.WriteLine("That name does not match the name of any existing team. Please try again.");
                    Console.WriteLine("Press any key to continue....");
                    Console.ReadKey();
                    return null;
            }

            Console.WriteLine("Something went wrong. Please try again.");
            Console.WriteLine("Press any key to continue....");
            Console.ReadKey();
            return null;
        }

        public int ChooseAddOrRemove()
        {
            // ask user if they want to add or remove developers
            Console.WriteLine("Would you like to add or remove developers from this team? Please enter 1 or 2");
            Console.WriteLine("1: Add Developers \n" +
                "2: Remove Developers");
            string input = Console.ReadLine();
        }

        public void AddDeveloperToTeam(DevTeam team, int choice)
        {
            switch (choice)
            {
                // adding a developer
                case 1:
                    // ask user if they want to search by name, ID, or both, and also allow them to see the full list of developers
                    Console.WriteLine("Would you like to find a developer by name, ID, or both?");
                    Console.WriteLine("1: Find by name \n" +
                        "2: Find by ID \n" +
                        "3: Find by both \n" +
                        "4: List all developers");
                    string input = Console.ReadLine();
                    switch (input)
                    {
                        // searching by name
                        case "1":
                            Console.WriteLine("Please enter the name of the developer you would like to add");
                            input = Console.ReadLine();
                            Developer addedDev = _devRepo.GetDeveloper(input);

                            if (addedDev != null)
                            {
                                // check if that developer is already on the team
                                foreach (Developer dev in team.TeamMembers)
                                {
                                    if (dev.Name.ToLower() == addedDev.Name.ToLower())
                                    {
                                        Console.WriteLine("That developer is already on this team. Please try again.");
                                        Console.WriteLine("Press any key to continue....");
                                        Console.ReadKey();
                                        break;
                                    }
                                }

                                // if not, add them to the team
                                Console.WriteLine($"Adding {addedDev.Name} to {team.TeamName}");
                                team.AddDeveloperToTeam(addedDev);
                                break;
                            }
                            // exit if that developer does not exist
                            else
                            {
                                Console.WriteLine($"There is no developer with the name {input} in the directory. Please try again.");
                                Console.WriteLine("Press any key to continue....");
                                Console.ReadKey();
                                break;
                            }
                        // searching by ID
                        case "2":
                            Console.WriteLine("Please enter the ID of the developer you would like to add");
                            input = Console.ReadLine();
                            Developer addedDev2 = null;
                            int inputID = 0;
                            bool isCorrectID = Int32.TryParse(input, out inputID);
                            if (isCorrectID)
                            {
                                addedDev2 = _devRepo.GetDeveloper(inputID);
                            }
                            else
                            {
                                Console.WriteLine($"{input} was not recognized as a valid ID number. Please try again.");
                                Console.WriteLine("Press any key to continue....");
                                Console.ReadKey();
                                break;
                            }

                            if (addedDev2 != null)
                            {
                                // check if dev with that ID number is already on the team
                                foreach (Developer dev in team.TeamMembers)
                                {
                                    if (dev.IDNum == addedDev2.IDNum)
                                    {
                                        Console.WriteLine("That developer is already on this team. Please try again.");
                                        Console.WriteLine("Press any key to continue....");
                                        Console.ReadKey();
                                        break;
                                    }
                                }

                                // add developer to team
                                Console.WriteLine($"Adding {addedDev2.Name} to {team.TeamName}");
                                team.AddDeveloperToTeam(addedDev2);
                                break;
                            }
                            // if dev with that ID number does not exist
                            else
                            {
                                Console.WriteLine($"There is no developer with the ID {inputID} in the directory. Please try again.");
                                Console.WriteLine("Press any key to continue....");
                                Console.ReadKey();
                                break;
                            }
                        // searching by name and ID
                        case "3":
                            Console.WriteLine("Please enter the name of the developer you would like to add");
                            string nameInput = Console.ReadLine();
                            Console.WriteLine("Please enter the ID of the developer you would like to add");
                            string idInput = Console.ReadLine();
                            Developer addedDev3 = null;
                            int inputID2 = 0;
                            bool isCorrectID2 = Int32.TryParse(input, out inputID2);
                            if (isCorrectID2)
                            {
                                addedDev2 = _devRepo.GetDeveloper(nameInput, inputID2);
                            }
                            else
                            {
                                Console.WriteLine($"{input} was not recognized as a valid ID number. Please try again.");
                                Console.WriteLine("Press any key to continue....");
                                Console.ReadKey();
                                break;
                            }

                            if (addedDev3 != null)
                            {
                                foreach (Developer dev in team.TeamMembers)
                                {
                                    if (dev.Name == addedDev3.Name && dev.IDNum == addedDev3.IDNum)
                                    {
                                        Console.WriteLine("That developer is already on this team. Please try again.");
                                        Console.WriteLine("Press any key to continue....");
                                        Console.ReadKey();
                                        break;
                                    }
                                }

                                Console.WriteLine($"Adding {addedDev3.Name} to {team.TeamName}");
                                team.AddDeveloperToTeam(addedDev3);
                                break;
                            }
                            else
                            {
                                Console.WriteLine($"There is no developer with the name {nameInput} and the ID {inputID2} in the directory. Please try again.");
                                Console.WriteLine("Press any key to continue....");
                                Console.ReadKey();
                                break;
                            }
                        case "4":
                            ListDevelopers(_devRepo.GetDevDirectory());
                            break;
                        default:
                            Console.WriteLine("That is not one of the accepted options. Please try again.");
                            Console.WriteLine("Press any key to continue....");
                            Console.ReadKey();
                            break;
                    }
                    break;
            }
        }

        // method that specifically lists developers that don't have access to pluralsight
    }
}
