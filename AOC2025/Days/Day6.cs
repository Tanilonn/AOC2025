namespace AOC2025.Days
{
    public class Day6
    {
        public static void DoPuzzlePart1()
        {
            var lines = File.ReadAllLines("./Input/Day6Input.txt");
            var splitLines = new List<string[]>();
            foreach (var line in lines)
            {
                splitLines.Add(line.Split([" "], StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries));
            }
            long solution = 0;

            var operators = splitLines.Last();
            var numbers = splitLines.Take(splitLines.Count - 1);

            for (int i = 0; i < operators.Length; i++)
            {
                var op = operators[i];
                long result = 0;
                if (op == "*")
                {
                    result = 1;
                }
                foreach (var num in numbers)
                {
                    if (op == "*")
                    {
                        result *= int.Parse(num[i]);
                    }
                    if (op == "+")
                    {
                        result += int.Parse(num[i]);
                    }
                }
                solution += result;
            }

            Console.WriteLine("solution is: " + solution);
        }

        public static void DoPuzzlePart2()
        {
            var lines = File.ReadAllLines("./Input/Day6Input.txt");

            long solution = 0;

            Console.WriteLine("solution is: " + solution);
        }
    }

}