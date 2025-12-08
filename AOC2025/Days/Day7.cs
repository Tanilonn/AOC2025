namespace AOC2025.Days
{
    public class Day7
    {
        public static void DoPuzzlePart1()
        {
            var lines = File.ReadAllLines("./Input/Day7Input.txt");
            // step 1: start at the S
            // step 2: loop over all lines
            // create a line at the index of the S / previous | if there's a dot at that index
            // if there's a ^ at that index, instead make two lines on index - 1 and index + 1 (also increase split counter)

            var splitCounter = 0;

            var foundIndexes = new List<int>();
            
            foreach (var line in lines)
            {
                // first line
                var indexOfS = line.IndexOf('S');
                if (indexOfS != -1)
                {
                    foundIndexes.Add(indexOfS);
                    continue;   
                }
                char[] lineAsChars = line.ToCharArray();

                foreach (var index in foundIndexes)
                {
                    if (lineAsChars[index] == '.')
                    {
                        lineAsChars[index] = '|';
                    }
                    if (lineAsChars[index] == '^')
                    {
                        splitCounter++;
                        lineAsChars[index - 1] = '|';
                        lineAsChars[index + 1] = '|';
                    }                    
                }
                var charsAsString = new string(lineAsChars);
                foundIndexes.Clear();

                for (int i = charsAsString.IndexOf('|'); i > -1; i = charsAsString.IndexOf('|', i + 1))
                {
                    foundIndexes.Add(i);
                }
            }

            Console.WriteLine("solution is: " + splitCounter);
        }

        public static void DoPuzzlePart2()
        {
            var lines = File.ReadAllLines("./Input/Day7Input.txt");

            long solution = 0;

            Console.WriteLine("solution is: " + solution);
        }
    }

}