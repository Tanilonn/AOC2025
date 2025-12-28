namespace AOC2025.Days
{
    public class Day9
    {
        public static void DoPuzzlePart1()
        {
            var lines = File.ReadAllLines("../../../Input/Day9Input.txt");

            // loop through all combinations of two red tiles exactly once
            long biggestSquare = 0;
            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = i + 1; j < lines.Length; j++)
                {
                    var first = lines[i].Split(',');
                    var second = lines[j].Split(',');

                    // the square that we can make is the differencce between x + 1 multiplied with the difference between y + 1
                    var difX = Math.Abs(long.Parse(first[0]) - long.Parse(second[0])) + 1;
                    var difY = Math.Abs(long.Parse(first[1]) - long.Parse(second[1])) + 1;
                    var squareSize = difX * difY;
                    if (squareSize > biggestSquare)
                    {
                        biggestSquare = squareSize;
                    }
                }
            }


            Console.WriteLine("solution is: " + biggestSquare);
        }

        public static void DoPuzzlePart2()
        {
            var lines = File.ReadAllLines("../../../Input/Day9Input.txt");

            long solution = 0;

            Console.WriteLine("solution is: " + solution);
        }
    }

}