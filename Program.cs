using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;

namespace TaskList
{
    class Program
    {
        const int offset = 1;
        static void Main(string[] args)
        {
            string userInput;
            int menuChoice = 0;
            List<TaskItem> taskList = new List<TaskItem>()
            {
                new TaskItem("Bobby", "Clean garage", DateTime.Parse("10/25/2020 1:00 pm")),
                new TaskItem("Sophie", "Orgranize closet", DateTime.Parse("10/27/2020 9:00 pm"))
            };
            while(menuChoice != 5)
            {
                userInput = DisplayMenu();
                menuChoice = ValidateMenuChoice(userInput);
                ExecuteMenuItem(menuChoice, taskList);
            }
        }

        static string DisplayMenu()
        {
            string userChoice;

            Console.WriteLine("The Best Task List" + Environment.NewLine);
            Console.WriteLine("Please select an option: ");
            Console.WriteLine("1. List Tasks");
            Console.WriteLine("2. Add Task");
            Console.WriteLine("3. Delete Task");
            Console.WriteLine("4. Mark Task Complete");
            Console.WriteLine("5. Quit" + Environment.NewLine);
            Console.Write("Choice: ");
            userChoice = Console.ReadLine();
            Console.WriteLine();
            return userChoice;
        }
        static int ValidateMenuChoice(string userInput)
        {
            bool valid;
            int userChoice;

            valid = int.TryParse(userInput, out userChoice);
            while (valid == false || userChoice <= 0 || userChoice > 5)
            {
                Console.Write("You must enter a number between 1-5: ");
                valid = int.TryParse(Console.ReadLine(), out userChoice);
            }
            return userChoice;
        }
        static void ExecuteMenuItem(int userChoice, List<TaskItem> taskList)
        {

            switch (userChoice)
            {
                case 1:
                    ListTasks(taskList);
                    break;
                case 2:
                    AddTask(taskList);
                    break;
                case 3:
                    DeleteTask(taskList);
                    break;
                case 4:
                    MarkTaskComplete(taskList);
                    break;
                case 5:
                    Console.WriteLine("See ya next time!");
                    break;
                default:
                    break;
            }
        }
        static void ListTasks(List<TaskItem> taskList)
        {
            int counter = 1;
            foreach(var task in taskList)
            {
                if (task.Complete)
                {
                    Console.WriteLine($"Task #{counter}: Has been completed by {task.TaskOwner}. The task was: {task.Description} due on {task.DueDate}");
                }
                else
                {
                    Console.WriteLine($"Task #{counter}: {task.TaskOwner} has to {task.Description} before {task.DueDate}");
                }
                counter++;
            }
            Console.WriteLine();
        }
        static void PrintTask(int index, List<TaskItem> taskList)
        {
            index -= offset;
            Console.WriteLine($"Task #{index + offset}{Environment.NewLine}" +
                $"Task Owner: {taskList[index].TaskOwner}{Environment.NewLine}" +
                $"Description: {taskList[index].Description}{Environment.NewLine}" +
            $"Due Date: {taskList[index].DueDate}{Environment.NewLine}");
        }
        static void AddTask(List<TaskItem> taskList)
        {
            string taskOwner;
            string description;
            DateTime dueDate;

            Console.WriteLine($"Add A Task {Environment.NewLine}{Environment.NewLine}To add a task you must provide the following: ");
            Console.Write("Task Owner: ");
            taskOwner = Console.ReadLine();
            Console.Write("Task description: ");
            description = Console.ReadLine();
            Console.Write("Task due date: ");
            dueDate = DateTime.Parse(Console.ReadLine());

            taskList.Add( 
                new TaskItem(taskOwner, description, dueDate));

            Console.WriteLine($"{Environment.NewLine}A new task has been created for {taskOwner}{Environment.NewLine}" +
                $"\tDescription: {description} {Environment.NewLine}" +
                $"\tDue: {dueDate}{Environment.NewLine}");
        }
        static void DeleteTask(List<TaskItem> taskList)
        {
            string userInput, yesOrNo;
            int userChoice;

            Console.WriteLine($"Please choose a task # to delete{Environment.NewLine}");
            ListTasks(taskList);
            Console.Write("Choice: ");
            userInput = Console.ReadLine();
            Console.WriteLine(); // for spacing
            userChoice = ValidateDeleteChoice(userInput, taskList);
            PrintTask(userChoice, taskList);
            Console.Write("Are you sure you want to delete this task? (y/n): ");


            taskList.RemoveAt(userChoice - offset);

            Console.WriteLine($"Task #{userChoice} has been deleted!{Environment.NewLine}");
        }
        static int ValidateDeleteChoice(string userInput, List<TaskItem> taskList)
        {
            bool valid;
            int userChoice;
            int maxIndex;

            maxIndex = taskList.Count;
            valid = int.TryParse(userInput, out userChoice);
            while (valid == false || userChoice <= 0 || userChoice > maxIndex)
            {
                Console.Write($"You must enter a number between 1-{maxIndex}: ");
                valid = int.TryParse(Console.ReadLine(), out userChoice);
            }
            return userChoice;
        }
        static void MarkTaskComplete(List<TaskItem> taskList)
        {
            string userInput;
            int userChoice;

            Console.WriteLine($"Please choose a task # to mark complete{Environment.NewLine}");
            ListTasks(taskList);
            Console.Write("Choice: ");
            userInput = Console.ReadLine();
            userChoice = ValidateDeleteChoice(userInput, taskList);
            taskList[userChoice - offset].Complete = true;

            Console.WriteLine($"Task #{userChoice} has been mark as complete!{Environment.NewLine}");
        }
    }
}
