namespace AOC2025.Days
{
    public class Day4Part2
    {
        public static void DoPuzzle()
        {
            var grid = File.ReadAllLines("./Input/Day4Input.txt");

            int cols = grid[0].Length;
            int rows = grid.Length;

            char[,] arr = Create2dArray(grid, cols, rows);

            var totalAccessibleRolls = 0;
            while (true)
            {
                int accessibleRolls = CalcAccessibleRolls(cols, rows, arr, ref totalAccessibleRolls);
                if (accessibleRolls == 0)
                {
                    break;
                }
            }

            Console.WriteLine("solution is: " + totalAccessibleRolls);
        }

        private static int CalcAccessibleRolls(int cols, int rows, char[,] arr, ref int totalAccessibleRolls)
        {
            int accessibleRolls = 0;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (arr[i, j] == '.')
                    {
                        // empty tile
                        continue;
                    }
                    List<int> surroundingTiles = GetAdjacent(arr, i, j);
                    var surroundingRolls = surroundingTiles.Count(t => t == '@');

                    // a roll is accessible if <4 adjecent tiles have a roll
                    if (surroundingRolls < 4)
                    {
                        // empty the tile, we're getting the roll
                        arr[i, j] = '.';
                        accessibleRolls++;
                        totalAccessibleRolls++;
                    }
                }
            }

            return accessibleRolls;
        }

        private static char[,] Create2dArray(string[] grid, int cols, int rows)
        {
            var arr = new char[rows, cols];
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    arr[r, c] = grid[r][c];
                }
            }

            return arr;
        }

        private static List<int> GetAdjacent(char[,] arr, int i, int j)
        {
            int cols = arr.GetLength(0);
            int rows = arr.GetLength(1);

            List<int> adjacent = [];

            // directions
            int[,] dirs = {
                {-1, -1}, {-1, 0}, {-1, 1},
                {0, -1}, {0, 1},
                {1, -1}, {1, 0}, {1, 1}
            };

            for (int k = 0; k < dirs.GetLength(0); k++)
            {
                int x = i + dirs[k, 0];
                int y = j + dirs[k, 1];
                if (IsValidPos(x, y, rows, cols))
                {
                    adjacent.Add(arr[x, y]);
                }
            }

            return adjacent;
        }

        private static bool IsValidPos(int i, int j, int n, int m)
        {
            if (i < 0 || j < 0 || i >= n || j >= m)
            {
                return false;
            }
            return true;
        }
    }
}