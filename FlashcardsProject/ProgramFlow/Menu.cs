using FlashcardsProject.Controllers;
using FlashcardsProject.Models;
using Microsoft.Identity.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stack = FlashcardsProject.Models.Stack;

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
                    PrintStacks(stacks);
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
            bool returnToMainMenu = false;

            while (!returnToMainMenu)
            {
                string? choice;

                Console.Clear();
                var stacks = StacksController.GetAll();

                if (stacks.Count == 0)
                {
                    Console.WriteLine("No stacks available in database! Press any key to return to main menu...");
                    Console.ReadKey();
                    returnToMainMenu = true;
                    continue;
                }

                PrintStacks(stacks);

                Console.WriteLine();
                Console.WriteLine("Please enter name of stack of flashcards to interact with or 0 to return to main menu:");
                choice = Console.ReadLine();

                if (choice == "0")
                {
                    returnToMainMenu = true;
                    continue;
                }
                else if (stacks.Where(s => s.Name == choice).Count() == 0)
                {
                    Console.WriteLine("No stack with such name. Press any key to try again...");
                    Console.ReadKey();
                    continue;
                }

                int stackId = stacks.FirstOrDefault(s => s.Name == choice).StackId;
                bool returnToSubMenu = false;

                while (!returnToSubMenu)
                {
                    Console.Clear();
                    var cardsList = FlashCardsController.GetByStackId(stackId);

                    if (cardsList.Count == 0)
                    {
                        Console.WriteLine("No flashcards in this stack yet...\n");
                    }
                    else
                    {
                        PrintFlashCards(cardsList);
                    }

                    Console.WriteLine("1 - Add a flashcard in current stack\n2 - Delete a flashcard\n3 - Update a flashcard\n4 - Return to main menu");
                    choice = Console.ReadLine();

                    while (!int.TryParse(choice, out int num) || (num < 1 || num > 4))
                    {
                        Console.WriteLine("Incorrect choice. Try again...");
                        choice = Console.ReadLine();
                    }

                    Flashcard? card = null;
                    int cardId = -1;
                    switch (choice)
                    {
                        case "1":
                            card = UserInput.GetFlashCardInfo();
                            if (card == null)
                            {
                                break;
                            }
                            else
                            {
                                card.StackId = stackId;
                                FlashCardsController.Add(card);
                            }
                            break;
                        case "2":
                            cardId = UserInput.GetCardRealId(cardsList);
                            if(cardId == -1)
                            {
                                break;
                            }
                            else
                            {
                                FlashCardsController.Delete(cardId);
                            }
                            break;
                        case "3":
                            card = UserInput.GetCardUpdateInfo(cardsList);
                            if (card == null)
                            {
                                break;
                            }
                            else
                            {
                                card.StackId = stackId;
                                FlashCardsController.Update(card);
                            }
                            break;
                        case "4":
                            returnToMainMenu = true;
                            returnToSubMenu = true;
                            break;
                        default:
                            break;
                    }
                }




            }
        }

        private void StartStudying()
        {

        }

        private void StudiesSubMenu()
        {

        }

        private void PrintStacks(List<Stack> stacks)
        {
            Console.WriteLine("+-------------------+");
            string tmp = ("| " + "Name").PadRight(20) + "|";
            Console.WriteLine(tmp);
            Console.WriteLine("+-------------------+");

            foreach (var st in stacks)
            {
                tmp = ("| " + st.Name).PadRight(20) + "|";
                Console.WriteLine(tmp);
                Console.WriteLine("+-------------------+");
            }
        }

        private void PrintFlashCards(List<Flashcard> cards)
        {
            var longestPhrase = cards.Select(c => c.Front.Length).Max();
            var longestTranslation = cards.Select(c => c.Back.Length).Max();
            var idCellWidth = 5;

            PrintTabulationMarks(idCellWidth);
            PrintTabulationMarks(longestPhrase);
            PrintTabulationMarks(longestTranslation+1);
            Console.WriteLine();
            string tmp = ("| " + "Id").PadRight(idCellWidth) + "| ";
            Console.Write(tmp);
            tmp = ("Front").PadRight(longestPhrase) + " | ";
            Console.Write(tmp);
            tmp = ("Back").PadRight(longestTranslation) + " |";
            Console.WriteLine(tmp);
            PrintTabulationMarks(idCellWidth);
            PrintTabulationMarks(longestPhrase);
            PrintTabulationMarks(longestTranslation+1);
            Console.WriteLine();

            for(int i = 0; i < cards.Count; i++)
            {
                tmp = ("| " + (i+1)).PadRight(idCellWidth) + "| ";
                Console.Write(tmp);
                tmp = (cards[i].Front).PadRight(longestPhrase) + " | ";
                Console.Write(tmp);
                tmp = (cards[i].Back).PadRight(longestTranslation) + " |";
                Console.WriteLine(tmp);
                PrintTabulationMarks(idCellWidth);
                PrintTabulationMarks(longestPhrase);
                PrintTabulationMarks(longestTranslation+1);
                Console.WriteLine();
            }

        }

        private void PrintTabulationMarks(int width)
        {
            Console.Write("-");
            for (int i = 0; i < width; i++)
            {
                Console.Write("-");
            }
            Console.Write("-");
        }
    }
}
