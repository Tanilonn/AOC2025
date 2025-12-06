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
            long solution = 0;

            var operators = splitLines.Last();

            while (operators.Count > 0)
            {
                var endOfColumn = false;
                var op = ' ';
                var actual = new List<string>();
                var cur = "";
                var result = 0;
                while (!endOfColumn)
                {
                    foreach (var stack in splitLines)
                    {
                        // pop the first char in each stack
                        var c = stack.Pop();
                        // if we found an operator we're at the end of the column and should start calcing
                        if (c == '+' || c == '*')
                        {
                            endOfColumn = true;
                            op = c;
                            break;
                        }
                        // else we concat it to the current num
                        cur += c;
                    }
                    // the vertical row is a number 
                    actual.Add(cur);
                    cur = "";
                }

                if (op == '*')
                {
                    result = 1;
                }
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
                solution += result;
            }

            Console.WriteLine("solution is: " + solution);
        }
    }

}