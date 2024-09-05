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


    }
}
