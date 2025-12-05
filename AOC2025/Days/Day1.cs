namespace AOC2025.Days
{
    public class Day1
    {
        private static readonly List<string> testRotations = ["R50", "R100"];

        public static void DoPuzzle()
        {
            var rotations = File.ReadAllLines("./Input/test.txt");
            var dialPosition = 50;
            var zeroCounter = 0;
            foreach (var r in rotations)
            {
                var direction = r[..1];
                var rotation = int.Parse(r[1..]);
                zeroCounter += CalcZeros(dialPosition, rotation, direction);
                if (direction == "L")
                {
                    rotation *= -1;
                }
                dialPosition = RotateDial(dialPosition, rotation);
            }
            Console.WriteLine("password is: " + zeroCounter);
        }

        private static int CalcZeros(int pos, int rotation, string direction)
        {
            var distance = pos;
            if (direction == "R")
            {
                distance = 100 - pos;
            }
            var brrr = rotation - distance;
            if (brrr < 0)
            {
                return 0;
            }
            var zeroInFirstCircle = 1;
            if (distance == 0)
            {
                zeroInFirstCircle = 0;
            }
            return (brrr / 100) + zeroInFirstCircle;
        }

        private static int RotateDial(int start, int rotation)
        {
            var value = start + rotation;
            return (value % 100 + 100) % 100;
        }
    }
}