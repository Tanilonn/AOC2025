namespace AOC2025.Days
{
    public class Day4
    {
        public static void DoPuzzlePart1()
        {
            var grid = File.ReadAllLines("./Input/Day4Input.txt");

            int cols = grid[0].Length;
            int rows = grid.Length;

            int accessibleRolls = 0;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (grid[i][j] == '.')
                    {
                        // empty tile
                        continue;
                    }
                    List<int> surroundingTiles = GetAdjacent(grid, i, j);
                    var surroundingRolls = surroundingTiles.Count(t => t == '@');

                    // a roll is accessible if <4 adjecent tiles have a roll
                    if (surroundingRolls < 4)
                    {
                        accessibleRolls++;
                    }
                }
            }
            Console.WriteLine("solution is: " + accessibleRolls);
        }

        private static List<int> GetAdjacent(string[] grid, int i, int j)
        {
            int cols = grid[0].Length;
            int rows = grid.Length;

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
                    adjacent.Add(grid[x][y]);
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