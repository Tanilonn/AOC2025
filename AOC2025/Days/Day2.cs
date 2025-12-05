namespace AOC2025.Days
{
    public class Day2
    {
        public static void DoPuzzlePart1()
        {
            var ranges = File.ReadAllText("./Input/Day2Input.txt").Split(",");
            var invalidIdsCount = 0;
            long invalidIdsSum = 0;
            foreach (var range in ranges)
            {
                var rangeSplit = range.Split("-");
                var rangeStart = long.Parse( rangeSplit[0]);
                var rangeEnd = long.Parse(rangeSplit[1]);
                
                for (long i = rangeStart; i <= rangeEnd; i++)
                {
                    var iAsString = i.ToString();
                    bool isEven = iAsString.Length % 2 == 0;
                    if (!isEven)
                    {
                        // if the length of the string is uneven we can never perfectly split it in halves so it must be a valid id
                        continue;
                    }
                    var half = iAsString.Length / 2;
                    var firstHalf = iAsString[..half];
                    var secondHalf = iAsString[half..];
                    if (firstHalf == secondHalf)
                    {
                        // if both sides are the same the id is invalid
                        invalidIdsCount++;
                        invalidIdsSum += i;
                    }
                }
            }

            Console.WriteLine("solution is: " + invalidIdsSum);
        }


        public static void DoPuzzlePart2()
        {
            var ranges = File.ReadAllText("./Input/Day2Input.txt").Split(",");
            long invalidIdsSum = 0;
            foreach (var range in ranges)
            {
                var rangeSplit = range.Split("-");
                var rangeStart = long.Parse( rangeSplit[0]);
                var rangeEnd = long.Parse(rangeSplit[1]);
                
                for (long id = rangeStart; id <= rangeEnd; id++)
                {
                    if (!IsIdValid(id))
                    {
                        invalidIdsSum += id;
                    }
                }
            }

            Console.WriteLine("solution is: " + invalidIdsSum);
        }

        private static bool IsIdValid(long id)
        {
            var idString = id.ToString();
            // start with checking if we can see if half the digits are the same, then keep going down one
            // if we start with an uneven length like 123 we should round down, half being 1.5 so 1
            // ababab length (6) can be devided by 2, 3 or 4, 5, 6, or if we start with chunks of 3, then 2 then 1
            var half = (int)Math.Floor((double)idString.Length / 2);

            for (int segmentSize = half; segmentSize >= 1; segmentSize--)
            {
                // now we need to know how many times j can fit inside the length, this should be a perfect fit otherwise it's valid
                if (idString.Length % segmentSize != 0)
                {
                    continue;
                }
                string[] segments = SplitIdBySegmentSize(idString, segmentSize);

                if (segments.Skip(1).All(s => s == segments[0]))
                {
                    return false;
                }
            }
            return true;
        }

        private static string[] SplitIdBySegmentSize(string idString, int segmentSize)
        {
            var fitsXTimes = idString.Length / segmentSize;

            // now we split the string in that many segments and all segments should be the same                        
            string[] result = new string[fitsXTimes];

            for (int a = 0; a < fitsXTimes; a++)
            {
                int startIndex = a * segmentSize;
                result[a] = idString.Substring(startIndex, segmentSize);
            }

            return result;
        }
    }
}