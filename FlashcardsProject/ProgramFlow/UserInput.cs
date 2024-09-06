using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlashcardsProject.Controllers;
using FlashcardsProject.Models;

namespace FlashcardsProject.ProgramFlow
{
    public static class UserInput
    {

        public static Stack? GetStackInfo()
        {
            Stack stack = new Stack();
            Console.WriteLine("Please enter stack name or type 0 for exit:");
            string? choice = Console.ReadLine();

            while (String.IsNullOrEmpty(choice) || StacksController.DoesStackWithSuchNameExist(choice))
            {
                Console.WriteLine("Incorrect input. Please enter stack name(it should be unique) or type 0 for exit:");
                choice = Console.ReadLine();
            }

            if(choice == "0")
            {
                return null;
            }
            else
            {
                stack.Name = choice;
            }

            return stack;
        }

        public static Stack? GetStackUpdateInfo()
        {
            Stack? stack = null;
            Console.WriteLine("Please enter stack name to update or type 0 for exit:");
            string? choice = Console.ReadLine();

            while (String.IsNullOrEmpty(choice) || !StacksController.DoesStackWithSuchNameExist(choice))
            {
                Console.WriteLine("Incorrect input. Please enter stack name to update or type 0 for exit:");
                choice = Console.ReadLine();
            }

            if (choice == "0")
            {
                return null;
            }
            else
            {
                stack = StacksController.GetByName(choice);
            }

            Console.WriteLine("Please enter new name for the stack(it should be unique):");
            choice = Console.ReadLine();
            while (String.IsNullOrEmpty(choice) || StacksController.DoesStackWithSuchNameExist(choice))
            {
                Console.WriteLine("Incorrect input. Please enter new name for the stack(it should be unique):");
                choice = Console.ReadLine();
            }
            stack.Name = choice;

            return stack;
        }

        public static string? GetStackNameForDeletion()
        {
            Console.WriteLine("Please enter stack name to delete or type 0 for exit:");
            string? choice = Console.ReadLine();

            while (String.IsNullOrEmpty(choice) || !StacksController.DoesStackWithSuchNameExist(choice))
            {
                Console.WriteLine("Incorrect input. Please enter stack name to delete or type 0 for exit:");
                choice = Console.ReadLine();
            }

            if (choice == "0")
            {
                return null;
            }

            return choice;
        }

        public static Flashcard? GetFlashCardInfo()
        {
            Flashcard flashcard = new Flashcard();
            Console.WriteLine("Please enter Front of card or type 0 for exit:");
            string? choice = Console.ReadLine();

            while (String.IsNullOrEmpty(choice))
            {
                Console.WriteLine("Incorrect input. Please enter Front of card or type 0 for exit:");
                choice = Console.ReadLine();
            }

            if (choice == "0")
            {
                return null;
            }
            else
            {
                flashcard.Front = choice;
            }

            Console.WriteLine("Please enter Back of card or type 0 for exit:");
            choice = Console.ReadLine();

            while (String.IsNullOrEmpty(choice))
            {
                Console.WriteLine("Incorrect input. Please enter Back of card or type 0 for exit:");
                choice = Console.ReadLine();
            }

            if (choice == "0")
            {
                return null;
            }
            else
            {
                flashcard.Back = choice;
            }

            return flashcard;
        }

        public static int GetCardRealId(List<Flashcard> cardsList)
        {
            Console.WriteLine();
            Console.WriteLine("Enter id of card to perform operation or type 0 to return:");
            string? choice = Console.ReadLine();
            int id = 0;
            while(!int.TryParse(choice, out id) || (id < 0 || id > cardsList.Count))
            {
                Console.WriteLine("Incorrect input. Try again or type 0 to return:");
                choice = Console.ReadLine();
            }

            return id == 0 ? -1 : cardsList.ElementAt(id - 1).CardId;
        }

        public static Flashcard? GetCardUpdateInfo(List<Flashcard> cardsList)
        {
            int id = GetCardRealId(cardsList);

            if(id == -1)
            {
                return null;
            }

            Flashcard? flashcard = GetFlashCardInfo();

            if(flashcard == null)
            {
                return null;
            }

            flashcard.CardId = id;

            return flashcard;
        }

    }
}
