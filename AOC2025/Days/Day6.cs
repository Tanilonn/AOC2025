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
                var actual = new List<string>();
                foreach (var num in numbers)
                {
                    actual.Add(num[i]);
                }
                solution += CalculateColumnResult(op[0], actual);
            }

            Console.WriteLine("solution is: " + solution);
        }

        public static void DoPuzzlePart2()
        {
            var lines = File.ReadAllLines("./Input/Day6Input.txt");
            List<Stack<char>> splitLines = CreateStacks(lines);
            long solution = 0;

            while (splitLines.Last().Count > 0)
            {
                var endOfColumn = false;
                var op = ' ';
                var actual = new List<string>();
                var verticalNum = "";
                while (!endOfColumn)
                {
                    foreach (var stack in splitLines)
                    {
                        var c = stack.Pop();
                        if (c == '+' || c == '*')
                        {
                            endOfColumn = true;
                            op = c;
                            break;
                        }
                        verticalNum += c;
                    }
                    actual.Add(verticalNum);
                    verticalNum = "";
                }
                actual = [.. actual.Where(n => !string.IsNullOrWhiteSpace(n))];

                solution += CalculateColumnResult(op, actual);
            }
            Console.WriteLine("solution is: " + solution);
        }

        private static long CalculateColumnResult(char op, List<string> actual)
        {
            long result = op == '*' ? 1 : 0;
            foreach (var num in actual)
            {
                if (op == '*')
                {
                    result *= int.Parse(num);
                }
                if (op == '+')
                {
                    result += int.Parse(num);
                }
            }
            return result;
        }

        private static List<Stack<char>> CreateStacks(string[] lines)
        {
            var splitLines = new List<Stack<char>>();
            foreach (var line in lines)
            {
                var stack = new Stack<char>();
                foreach (var num in line)
                {
                    stack.Push(num);
                }
                splitLines.Add(stack);
            }
            return splitLines;
        }
    }
}