using System.IO;
using System.Text.Json;

namespace ToDoList
{
    class Program
    {
        private const string FilePath = "tasks.json";
        private static readonly string[] ValidCommands = ["add", "delete", "complete", "list", "clear", "quit"];

        private static List<ToDoItem> List = new();

        static void Main()
        {
            Console.Clear();
            bool isRunning = true;

            LoadTasks();

            Console.WriteLine("\t--- ToDoList ---");
            while (isRunning)
            {
                string command = GetValidCommand("Enter command");

                switch (command)
                {
                    case "add": AddTask(); break;
                    case "delete": DeleteTask(); break;
                    case "complete": CompleteTask(); break;
                    case "list": PrintTasks(); break;
                    case "clear": ClearList(); break;
                    case "quit":
                        Console.WriteLine("Saving...");
                        SaveTasks();
                        isRunning = false;
                        break;
                }
            }
        }



        public static void AddTask()
        {
            string title = Functions.GetValidString("Enter a title");

            if (List.Exists(task => task.Title.ToLower() == title.ToLower()))
            {
                Console.WriteLine("This task already exists!");
                return;
            }

            string desc = Functions.GetDescription("Add description");
            
            List.Add(new ToDoItem(title, desc));
            Console.WriteLine("New task successfully added!");
        }

        public static void DeleteTask()
        {
            if (List.Count == 0)
            {
                Console.WriteLine("Oops! Your List is empty! You can add a task with \"add\" command");
                return;
            }

            if (List.Count == 1)
            {
                List.RemoveAt(0);
                Console.WriteLine("Succesfully deleted!");
                return;
            }

            int index = Functions.GetValidInt("Enter index", 1, List.Count);

            List.RemoveAt(index - 1);
            Console.WriteLine($"Task at index {index} was succesfully deleted!");
        }

        public static void CompleteTask()
        {
            if (List.Count == 0)
            {
                Console.WriteLine("Oops! Your List is empty! Try to add a task with \"add\"");
                return;
            }

            if (List.Count == 1)
            {
                List[0].IsDone = true;
                Console.WriteLine("Succesfully Completed!");
                return;
            }

            int index = Functions.GetValidInt("Enter index", 1, List.Count);

            List[index - 1].IsDone = true;
            Console.WriteLine($"Task at index {index} was marked as completed!");
        }

        public static void PrintTasks()
        {
            if (List.Count == 0)
            {
                Console.WriteLine("Oops! Your List is empty! Try adding a task with \"add\"");
                return;
            }

            Console.WriteLine("\n--- Your Tasks ---");

            for (int i = 0; i < List.Count; i++)
                Console.WriteLine($" {i + 1}: {List[i]}");

            Console.WriteLine("------------------");
        }

        public static void ClearList()
        {
            if (List.Count == 0)
            {
                Console.WriteLine("Oops! You cannot clear an empty list!");
                return;
            }

            List.Clear();
            Console.WriteLine("Succesfully cleared the list");
        }


        
        public static void SaveTasks()
        {
            var options = new JsonSerializerOptions {WriteIndented = true};

            string jsonString = JsonSerializer.Serialize(List, options);

            File.WriteAllText(FilePath, jsonString);
        }

        public static void LoadTasks()
        {
            if (File.Exists(FilePath))
            {
                string jsonString = File.ReadAllText(FilePath);
                List = JsonSerializer.Deserialize<List<ToDoItem>>(jsonString) ?? new();
            }
        }

        static string GetValidCommand(string message)
        {
            while (true)
            {
                Console.Write($"{message}: ");
                string input = Console.ReadLine()?.Trim().ToLower() ?? "";

                if (input == "help")
                {
                    PrintCommands();
                    continue;
                }

                if (ValidCommands.Contains(input))
                    return input;

                Console.WriteLine("Invalid command. Enter \"help\" to see all available commands");
            }
        }

        static void PrintCommands()
        {
            foreach(var item in ValidCommands)
                Console.WriteLine($" {item}");
        }
    }
}