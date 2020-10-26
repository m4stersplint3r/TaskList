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
            string userInput = "";
            int menuChoice = 0;
            List<TaskItem> taskList = new List<TaskItem>();
            taskList = InitializeList();

            while(userInput != "q")
            {
                userInput = DisplayMenu();
                menuChoice = ValidateMenuChoice(userInput);
                ExecuteMenuItem(menuChoice, taskList);
            }
        }

        public static List<TaskItem> InitializeList()
        {
            List<TaskItem> taskList = new List<TaskItem>()
            {
                new TaskItem("Bobby", "Clean garage", DateTime.Parse("10/25/2020 1:00 pm")),
                new TaskItem("Sophie", "Organize closet", DateTime.Parse("10/27/2020 9:00 pm"))
            };
            return taskList;
        }
        public static string DisplayMenu()
        {
            string userChoice;

            Console.WriteLine($"The Best Task List {Environment.NewLine}{Environment.NewLine}Main Menu");
            Console.WriteLine("Please select an option: ");
            Console.WriteLine("1. List Tasks");
            Console.WriteLine("2. List Tasks By Owner");
            Console.WriteLine("3. List Tasks Due Before Date");
            Console.WriteLine("4. Add Task");
            Console.WriteLine("5. Edit Task");
            Console.WriteLine("6. Delete Task");
            Console.WriteLine("7. Mark Task Complete");
            Console.WriteLine("Q. Quit" + Environment.NewLine);
            Console.Write("Choice: ");
            userChoice = Console.ReadLine().Trim().ToLower();
            Console.WriteLine();
            return userChoice;
        }
        public static void ExecuteMenuItem(int userChoice, List<TaskItem> taskList)
        {

            switch (userChoice)
            {
                case 1:
                    ListTasks(taskList);
                    break;
                case 2:
                    ListTasksByOwner(taskList);
                    break;
                case 3:
                    ListTasksByDate(taskList);
                    break;
                case 4:
                    AddTask(taskList);
                    break;
                case 5:
                    EditTask(taskList);
                    break;
                case 6:
                    DeleteTask(taskList);
                    break;
                case 7:
                    MarkTaskComplete(taskList);
                    break;
                case 'q':
                    Console.WriteLine("See ya next time!");
                    break;
                default:
                    break;
            }
        }
        public static int ValidateMenuChoice(string userInput)
        {
            bool valid;
            int userChoice;

            if(userInput.Trim().ToLower() == "q")
            {
                return userInput.ToLower().ToCharArray()[0];
            }

            valid = int.TryParse(userInput, out userChoice);
            while (valid == false || userChoice <= 0 || userChoice > 8)
            {
                Console.Write("You must enter a number between 1-8 or Q: ");
                valid = int.TryParse(Console.ReadLine(), out userChoice);
            }
            return userChoice;
        }
        public static int ValidateDeleteChoice(string userInput, List<TaskItem> taskList)
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
        public static bool ValidateYesOrNo(string userInput)
        {
            bool validInput = true;
            userInput = userInput.Trim().ToLower();
            if (userInput == "y")
            {
                return true;
            } else if(userInput == "n")
            {
                return false;
            }
            else
            {
                do
                {
                    Console.Write("Please enter \"y\" or \"n\": ");
                    userInput = Console.ReadLine().Trim().ToLower(); 
                    if (!(userInput == "y" || userInput == "n"))
                    {
                        validInput = false;
                    }
                    else if (userInput == "y")
                    {
                        return true;
                    }
                    else if (userInput == "n")
                    {
                        return false;
                    }
                }
                while (validInput == false);
                return false;
            }
        }
        public static void ListTasks(List<TaskItem> taskList)
        {
            int counter = 1;
            int index;
            foreach(var task in taskList)
            {
                index = counter - 1;
                Console.WriteLine($"Task #{index + offset}");
                if (taskList[index].Complete)
                {
                    Console.WriteLine($"Status: Complete");
                }
                else
                {
                    Console.WriteLine($"Status: Incomplete");
                }
                Console.WriteLine($"Task Owner: {taskList[index].TaskOwner}{Environment.NewLine}" +
                    $"Due Date: {taskList[index].DueDate}{Environment.NewLine}" +
                    $"Description: {taskList[index].Description}{Environment.NewLine}");
                counter++;
            }
        }
        public static void ListTasksByOwner(List<TaskItem> taskList)
        {
            int counter = 1;
            int noTasksCounter = 0;
            int index;
            string taskOwner;

            Console.Write("Please enter a name: ");
            taskOwner = Console.ReadLine().Trim().ToLower();
            Console.WriteLine();
            foreach (var task in taskList)
            {
                index = counter - 1;
                if (task.TaskOwner.ToLower() == taskOwner)
                {
                    Console.WriteLine($"Task #{index + offset}");
                    if (taskList[index].Complete)
                    {
                        Console.WriteLine($"Status: Complete");
                    }
                    else
                    {
                        Console.WriteLine($"Status: Incomplete");
                    }
                    Console.WriteLine($"Task Owner: {taskList[index].TaskOwner}{Environment.NewLine}" +
                        $"Due Date: {taskList[index].DueDate}{Environment.NewLine}" +
                        $"Description: {taskList[index].Description}{Environment.NewLine}");
                    noTasksCounter++;
                }                
                counter++;
            }
            if (noTasksCounter == 0)
            {
                Console.WriteLine($"We do not have any tasks for {taskOwner}");
            }
        }
        public static void ListTasksByDate(List<TaskItem> taskList)
        {
            int counter = 1;
            int noTasksCounter = 0;
            int index;
            DateTime cutoffDate;

            Console.Write("Please enter a due date cutoff: ");
            while(DateTime.TryParse(Console.ReadLine().Trim().ToLower(), out cutoffDate) == false)
            {
                Console.Write("You must enter a valid date (mm/dd/yyyy): ");
            }
            Console.WriteLine();
            foreach (var task in taskList)
            {
                index = counter - 1;
                if (task.DueDate < cutoffDate)
                {
                    Console.WriteLine($"Task #{index + offset}");
                    if (taskList[index].Complete)
                    {
                        Console.WriteLine($"Status: Complete");
                    }
                    else
                    {
                        Console.WriteLine($"Status: Incomplete");
                    }
                    Console.WriteLine($"Task Owner: {taskList[index].TaskOwner}{Environment.NewLine}" +
                        $"Due Date: {taskList[index].DueDate}{Environment.NewLine}" +
                        $"Description: {taskList[index].Description}{Environment.NewLine}");
                    noTasksCounter++;
                }                
                counter++;
            }
            if (noTasksCounter == 0)
            {
                Console.WriteLine($"We do not have any tasks due before {cutoffDate}{Environment.NewLine}");
            }
        }
        public static void PrintTask(int index, List<TaskItem> taskList)
        {
            index -= offset;
            Console.WriteLine($"Task #{index + offset}");
            if (taskList[index].Complete)
            {
                Console.WriteLine($"Status: Complete");
            }
            else
            {
                Console.WriteLine($"Status: Incomplete");
            }
            Console.WriteLine($"Task Owner: {taskList[index].TaskOwner}{Environment.NewLine}" +
                $"Due Date: {taskList[index].DueDate}{Environment.NewLine}" +
                $"Description: {taskList[index].Description}{Environment.NewLine}");
        }
        public static void AddTask(List<TaskItem> taskList)
        {
            string taskOwner;
            string description;
            DateTime dueDate;

            Console.WriteLine($"Add A Task {Environment.NewLine}{Environment.NewLine}To add a task you must provide the following: ");
            do
            {
                Console.Write("Task Owner: ");
                taskOwner = Console.ReadLine().Trim();
            } while (taskOwner.Length == 0);
            do
            {
                Console.Write("Task description: ");
                description = Console.ReadLine().Trim();
            } while (description.Length == 0);
            
            Console.Write("Task due date: ");
            while(DateTime.TryParse(Console.ReadLine().Trim(), out dueDate) == false)
            {
                Console.Write("You must enter a valid date (mm/dd/yyyy): ");
            }
            

            taskList.Add( 
                new TaskItem(taskOwner, description, dueDate));

            Console.WriteLine($"{Environment.NewLine}A new task has been created for {taskOwner}{Environment.NewLine}" +
                $"Description: {description} {Environment.NewLine}" +
                $"Due: {dueDate}{Environment.NewLine}");
        }
        public static void EditTask(List<TaskItem> taskList)
        {
            string userInput, taskOwner, description;
            int userChoice;
            DateTime dueDate;

            Console.WriteLine($"Please choose a task # to edit{Environment.NewLine}");
            ListTasks(taskList);
            Console.Write("Choice: ");
            userInput = Console.ReadLine();
            Console.WriteLine(); // for spacing

            userChoice = ValidateDeleteChoice(userInput, taskList);
            PrintTask(userChoice, taskList);
            Console.Write($"What would you like to edit?{Environment.NewLine}Task Owner, Description or Due Date: ");
            userInput = Console.ReadLine().Trim().ToLower();
            while (userInput != "task owner" && userInput != "description" && userInput != "due date")
            {
                Console.Write("You must enter \"Task Owner\", \"Description\" or \"Due Date\": ");
                userInput = Console.ReadLine().Trim().ToLower();
            }
            if(userInput == "task owner")
            {
                Console.WriteLine($"{taskList[userChoice - offset].TaskOwner} is the current task owner.");
                do
                {
                    Console.Write("Please enter a new task owner: ");
                    taskOwner = Console.ReadLine().Trim();
                } while (taskOwner.Length == 0);
                taskList[userChoice - offset].TaskOwner = taskOwner;                
            }
            else if (userInput == "description")
            {
                Console.WriteLine($"{taskList[userChoice - offset].Description} is the current task description.");
                do
                {
                    Console.Write("Please enter a new description: ");
                    description = Console.ReadLine().Trim();
                } while (description.Length == 0);
                taskList[userChoice - offset].Description = description;
            }
            else if (userInput == "due date")
            {
                Console.WriteLine($"{taskList[userChoice - offset].DueDate.ToShortDateString()} is the current task due date.");
                Console.Write("Please enter a new due date: ");
                while (DateTime.TryParse(Console.ReadLine().Trim(), out dueDate) == false)
                {
                    Console.Write("You must enter a valid date (mm/dd/yyyy): ");
                }
                taskList[userChoice - offset].DueDate = dueDate;
            }
            Console.WriteLine($"{Environment.NewLine}The task has been updated.{Environment.NewLine}");
            PrintTask(userChoice, taskList);
        }
        public static void DeleteTask(List<TaskItem> taskList)
        {
            string userInput;
            int userChoice;

            Console.WriteLine($"Please choose a task # to delete{Environment.NewLine}");
            ListTasks(taskList);
            Console.Write("Choice: ");
            userInput = Console.ReadLine();
            Console.WriteLine(); // for spacing

            userChoice = ValidateDeleteChoice(userInput, taskList);
            PrintTask(userChoice, taskList);
            Console.Write("Are you sure you want to delete this task? (y/n): ");
            userInput = Console.ReadLine();
            if (ValidateYesOrNo(userInput))
            {
                taskList.RemoveAt(userChoice - offset);
                Console.WriteLine($"Task #{userChoice} has been deleted!{Environment.NewLine}");
            }
            else
            {
                Console.WriteLine($"Task #{userChoice} has not been deleted.{Environment.NewLine}");
            }
        }
        public static void MarkTaskComplete(List<TaskItem> taskList)
        {
            string userInput;
            int userChoice;

            Console.WriteLine($"Please choose a task # to mark complete{Environment.NewLine}");
            ListTasks(taskList);
            Console.Write("Choice: ");
            userInput = Console.ReadLine();
            Console.WriteLine(); // for spacing

            userChoice = ValidateDeleteChoice(userInput, taskList);
            PrintTask(userChoice, taskList);
            Console.Write("Are you sure you want to mark this task complete? (y/n): ");
            userInput = Console.ReadLine();
            if (ValidateYesOrNo(userInput))
            {
                taskList[userChoice - offset].Complete = true;
                Console.WriteLine($"Task #{userChoice} has been marked complete!{Environment.NewLine}");
            }
            else
            {
                Console.WriteLine($"Task #{userChoice} has not been changed.{Environment.NewLine}");
            }            
        }
    }
}