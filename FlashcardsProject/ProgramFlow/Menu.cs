using FlashcardsProject.Controllers;
using FlashcardsProject.Models;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashcardsProject.ProgramFlow
{
    public class Menu
    {
        public bool exit = false;

        public void Run()
        {
            while (!exit)
            {
                string? choice;

                Console.Clear();
                Console.WriteLine("1 - Manage stacks\n2 - Manage flashcards\n3 - Study\n4 - View study sessions\n5 - Exit");
                choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        StacksSubMenu();
                        break;
                    case "2":
                        FlashCardsSubMenu();
                        break;
                    case "3":
                        StartStudying();
                        break;
                    case "4":
                        StudiesSubMenu();
                        break;
                    case "5":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Press any key to try again...");
                        Console.ReadKey();
                        break;

                }
            }
        }

        private void StacksSubMenu()
        {
            bool returnToMainMenu = false;

            while (!returnToMainMenu)
            {
                string? choice;

                Console.Clear();
                var stacks = StacksController.GetAll();

                if (stacks.Count == 0)
                {
                    Console.WriteLine("No stacks available in database!");
                }
                else
                {
                    Console.WriteLine("+-------------------+");
                    string tmp = ("| " + "Name").PadRight(20) + "|";
                    Console.WriteLine(tmp);
                    Console.WriteLine("+-------------------+");

                    foreach ( var st in stacks)
                    {
                        tmp = ("| " + st.Name).PadRight(20) + "|";
                        Console.WriteLine(tmp);
                        Console.WriteLine("+-------------------+");
                    }
                }
                Console.WriteLine();
                Console.WriteLine("1 - Add new stack\n2 - Update stack\n3 - Delete stack\n4 - Return to main menu");
                choice = Console.ReadLine();
                Stack? stack = null;
                string? stackName = null;

                switch (choice)
                {
                    case "1":
                        stack = UserInput.GetStackInfo();
                        if (stack == null)
                        {
                            break;
                        }
                        else
                        {
                            StacksController.Add(stack);
                        }
                        break;
                    case "2":
                        stack = UserInput.GetStackUpdateInfo();
                        if (stack == null)
                        {
                            break;
                        }
                        else
                        {
                            StacksController.Update(stack);
                        }
                        break;
                    case "3":
                        stackName = UserInput.GetStackNameForDeletion();
                        if (string.IsNullOrEmpty(stackName))
                        {
                            break;
                        }
                        else
                        {
                            StacksController.Delete(stackName);
                        }
                        break;
                    case "4":
                        returnToMainMenu = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Press any key to try again...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void FlashCardsSubMenu()
        {

        }

        private void StartStudying()
        {

        }

        private void StudiesSubMenu()
        {

        }
    }
}
