namespace AOC2025.Days
{
    public class Day5
    {
        public static void DoPuzzlePart1()
        {
            var lines = File.ReadAllLines("./Input/Day5Input.txt");
            var whiteLineIndex = lines.IndexOf("");
            var food = lines.TakeLast(lines.Length - whiteLineIndex - 1);
            var intRanges = CreateIntRanges(lines.Take(whiteLineIndex));

            var freshFoods = 0;
            foreach (var item in food)
            {
                var itemId = long.Parse(item);
                if (intRanges.Any(r => r.IsInRange(itemId)))
                {
                    freshFoods++;
                }
            }

            Console.WriteLine("solution is: " + freshFoods);
        }

        public static void DoPuzzlePart2()
        {
            var lines = File.ReadAllLines("./Input/Day5Input.txt");
            var whiteLineIndex = lines.IndexOf("");
            var intRanges = CreateIntRanges(lines.Take(whiteLineIndex));

            // sort the ranges by From
            intRanges.Sort();

            // now we loop over the ranges, count all ids in the first range
            // if the next range overlaps with the previous range, skip the numbers we already counted by starting at the toAnd of the previous range
            // important: if the toAnd is lower than the toAnd of the previous range, this range fits entirely in that range
            // in this case, we shouldn't count anything and keep the previous range to compare to the next, as it is bigger
            long freshIds = 0;
            var rangeToCompare = new Range(0, 0);
            foreach (var range in intRanges)
            {
                // the range fits entirely in the range to compare
                if (range.ToAnd <= rangeToCompare.ToAnd)
                {
                    continue;
                }
                // get the starting point, either the from if there's a gap or the toAnd if there's an overlap
                var start = Math.Max(rangeToCompare.ToAnd + 1, range.From);
                // count everything between start and range.toAnd
                var numbersBetween = range.ToAnd + 1 - start;
                freshIds += numbersBetween;
                // and make this range the new range to compare to
                rangeToCompare = range;
            }

            Console.WriteLine("solution is: " + freshIds);
        }

        private static List<Range> CreateIntRanges(IEnumerable<string> ranges)
        {
            var intRanges = new List<Range>();
            foreach (var range in ranges)
            {
                var start = long.Parse(range[..range.IndexOf('-')]);
                var end = long.Parse(range[(range.IndexOf('-') + 1)..]);
                intRanges.Add(new Range(start, end));
            }
            return intRanges;
        }
    }

    public readonly record struct Range(long From, long ToAnd) : IComparable<Range>
    {
        // in 3-5 with 4 toCheck 3 is smaller than 4 and 4 is smaller than 5
        // 3 and 5 are also in range because we do smaller or equal =<
        public bool IsInRange(long toCheck) => From <= toCheck && toCheck <= ToAnd;

        public int CompareTo(Range other) => From.CompareTo(other.From);
    }

}