namespace AOC2025.Days
{
    public class Day3
    {
        public static void DoPuzzlePart1()
        {
            var banks = File.ReadAllLines("./Input/Day3Input.txt");
            var maxJoltageSum = 0;
            foreach (var bank in banks)
            {
                var batteries = bank.Select(c => int.Parse(c.ToString()));
                var batteriesMinusLast = batteries.Take(batteries.Count() -1);
                var firstHighest = batteriesMinusLast.Max();
                var index = bank.IndexOf(firstHighest.ToString());
                var secondPartBatteries = bank[(index + 1)..];
                var secondPartHighest = secondPartBatteries.Select(c => int.Parse(c.ToString())).Max();
                var highestJoltage = int.Parse(firstHighest.ToString() + secondPartHighest.ToString());
                maxJoltageSum += highestJoltage;
            }

            Console.WriteLine("solution is: " + maxJoltageSum);
        }

        public static void DoPuzzlePart2()
        {
            var banks = File.ReadAllLines("./Input/Day3Input.txt");
            long maxJoltageSum = 0;
            foreach (var bank in banks)
            {
                var digitsToExclude = bank.Length - 12;
                var remainingBatteriesToCheck = bank;
                var result = "";
                while (digitsToExclude > 0)
                {
                    if (remainingBatteriesToCheck.Length == digitsToExclude)
                    {
                        remainingBatteriesToCheck = "";
                        break;
                    }
                    var batteries = remainingBatteriesToCheck.Select(c => long.Parse(c.ToString()));
                    var set = batteries.Take(digitsToExclude + 1);
                    var highestInSet = set.Max();
                    var index = remainingBatteriesToCheck.IndexOf(highestInSet.ToString());
                    remainingBatteriesToCheck = remainingBatteriesToCheck[(index + 1)..];
                    result += highestInSet.ToString();
                    digitsToExclude -= index;
                }
                result += remainingBatteriesToCheck;
                maxJoltageSum += long.Parse(result);
            }

            Console.WriteLine("solution is: " + maxJoltageSum);
        }
    }
}