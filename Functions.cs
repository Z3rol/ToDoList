namespace ToDoList
{
    class Functions
    {
        public static int GetValidInt(string message, int minValue = int.MinValue, int maxValue = int.MaxValue)
        {
            while (true)
            {
                Console.Write($"{message}: "); 
                string input = Console.ReadLine() ?? "";

                // Handle invalid format
                if (!int.TryParse(input, out int num))
                {
                    Console.WriteLine("Invalid format. Please try again");
                    continue;
                }

                // Handle number out of range 
                if (num < minValue || num > maxValue)
                {
                    Console.WriteLine($"Number is out of range. Enter a numeber from {minValue} to {maxValue}");
                    continue;
                }

                return num;
            }
        }

        public static string GetValidString(string message)
        {
            while (true)
            {
                // Print prompt and read input
                Console.Write($"{message}: ");
                string s = Console.ReadLine()?.Trim() ?? "";

                // Handle empty imput
                if (s == "")
                {
                    Console.WriteLine("Error: cannot enter empty text. Please try again");
                    continue;
                }

                return s;
            }
        }

        public static string GetDescription(string message)
        {
            Console.Write($"{message} or press enter to leave it empty: ");
            return Console.ReadLine()?.Trim() ?? "";
        }
    }
}