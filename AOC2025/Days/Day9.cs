using System.Numerics;

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

            var greenSquares = GetGreenSquares(lines);

            // loop through all combinations of two red tiles exactly once
            var squares = new Dictionary<(Vector2, Vector2), float>();
            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = i + 1; j < lines.Length; j++)
                {
                    var first = lines[i].Split(',');
                    var second = lines[j].Split(',');
                    var a = new Vector2(long.Parse(first[0]), long.Parse(first[1]));
                    var b = new Vector2(long.Parse(second[0]), long.Parse(second[1]));

                    // the square that we can make is the differencce between x + 1 multiplied with the difference between y + 1
                    var difX = Math.Abs(a.X - b.X) + 1;
                    var difY = Math.Abs(a.Y - b.Y) + 1;
                    var squareSize = difX * difY;
                    squares.TryAdd((a, b), squareSize);
                }
            }
            var orderedSquares = squares.OrderByDescending(s => s.Value).ToList();
            // big squares are checked first so we can exit early
            foreach (var square in orderedSquares)
            {
                var squaresInSquare = GetVectorsInSquare(square.Key.Item1, square.Key.Item2);
                var squareIsAllGreen = squaresInSquare.All(greenSquares.Contains);
                if (squareIsAllGreen)
                {
                    Console.WriteLine("solution is: " + square.Value);
                    break;
                }
            }

        }

        private static List<Vector2> GetGreenSquares(string[] lines)
        {
            var greenSquares = new List<Vector2>();

            for (int i = 0; i < lines.Length; i++)
            {                
                var first = lines[i].Split(',');
                string[] second;
                if (i == lines.Length - 1)
                {
                    second = lines[0].Split(',');

                }
                else
                {
                    second = lines[i + 1].Split(',');
                }
                var a = new Vector2(long.Parse(first[0]), long.Parse(first[1]));
                var b = new Vector2(long.Parse(second[0]), long.Parse(second[1]));
                greenSquares.AddRange(GetVectorsInSquare(a, b));                
            }
            var lowestY = greenSquares.Min(v => v.Y);
            var maxY = greenSquares.Max(v => v.Y);
            greenSquares = greenSquares.Distinct().ToList();
            // then fill in the rest of the squares, going line by line and just making everything green that's between 2 red/green tiles

            var squaresInMiddle = new List<Vector2>();
            for (var y = lowestY; y < maxY; y++)
            {
                var vectorsInRow = greenSquares.Where(v => v.Y == y);
                var lowestX = vectorsInRow.Min(v => v.X);
                var maxX = vectorsInRow.Max(v => v.X);

                squaresInMiddle.AddRange(GetVectorsInSquare(new Vector2(lowestX, y), new Vector2(maxX, y)));
            }
            greenSquares.AddRange(squaresInMiddle);

            return greenSquares.Distinct().ToList();
        }

        private static List<Vector2> GetVectorsInSquare(Vector2 i, Vector2 j)
        {
            var points = new List<Vector2>();
            var xMin = Math.Min(i.X, j.X);
            var xEnd = Math.Max(i.X, j.X);
            var yMin = Math.Min(i.Y, j.Y);
            var yEnd = Math.Max(i.Y, j.Y);

            for (var x = xMin; x <= xEnd; x++)
            {
                for (var y = yMin; y <= yEnd; y++)
                {
                    points.Add(new Vector2(x, y));
                }
            }
            return points;
        }
    }

}