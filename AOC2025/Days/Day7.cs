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

            var branches = new List<Branch>();
            var root = new Node(0, "root");
            var l = 0;
            var allNodes = new List<Node>
            {
                root
            };
            foreach (var line in lines)
            {
                l++;
                // first line
                var indexOfS = line.IndexOf('S');
                if (indexOfS != -1)
                {
                    branches.Add(new Branch(indexOfS, root));
                    // this is our root
                    root.Index = indexOfS;
                    continue;
                }
                char[] lineAsChars = line.ToCharArray();

                var toRemove = new List<Branch>();
                var toAdd = new List<Branch>();
                var newNodes = new List<Node>();
                //Console.WriteLine("looping over " + branches.Count + " branches");
                foreach (var branch in branches)
                {
                    if (lineAsChars[branch.IndexOrigin] == '.')
                    {
                        lineAsChars[branch.IndexOrigin] = '|';
                    }
                    if (lineAsChars[branch.IndexOrigin] == '^')
                    {
                        lineAsChars[branch.IndexOrigin - 1] = '|';
                        lineAsChars[branch.IndexOrigin + 1] = '|';
                        var n = newNodes.FirstOrDefault(n => n.Index == branch.IndexOrigin) ?? new Node(branch.IndexOrigin, l.ToString() + branch.IndexOrigin.ToString());
                        newNodes.Add(n);
                        branch.NodeAtOrigin.Children.Add(n);
                        if (!toAdd.Any(b => b.IndexOrigin == branch.IndexOrigin - 1 && b.NodeAtOrigin == n))
                        {
                            toAdd.Add(new Branch(branch.IndexOrigin - 1, n));
                        }
                        if (!toAdd.Any(b => b.IndexOrigin == branch.IndexOrigin + 1 && b.NodeAtOrigin == n))
                        {
                            toAdd.Add(new Branch(branch.IndexOrigin + 1, n));
                        }
                        toRemove.Add(branch); // this branch has ended
                    }
                }
                allNodes.AddRange(newNodes.Distinct());
                foreach (var branch in toRemove)
                {
                    branches.Remove(branch);
                }
                branches.AddRange(toAdd);

                newNodes.Clear();
            }
            var toAdd1 = new List<Branch>();
            var newNodes1 = new List<Node>();
            foreach (var branch in branches)
            {
                var n = newNodes1.FirstOrDefault(n => n.Index == branch.IndexOrigin) ?? new Node(branch.IndexOrigin, l.ToString() + branch.IndexOrigin.ToString());
                newNodes1.Add(n);
                branch.NodeAtOrigin.Children.Add(n);
                if (!toAdd1.Any(b => b.IndexOrigin == branch.IndexOrigin - 1 && b.NodeAtOrigin == n))
                {
                    toAdd1.Add(new Branch(branch.IndexOrigin - 1, n));
                }
                if (!toAdd1.Any(b => b.IndexOrigin == branch.IndexOrigin + 1 && b.NodeAtOrigin == n))
                {
                    toAdd1.Add(new Branch(branch.IndexOrigin + 1, n));
                }

            }
            allNodes.AddRange(newNodes1.Distinct());
            Console.WriteLine("solution is: " + PathCounter.CountPaths(root, allNodes));
        }
    }

    internal class Node(int index, string name)
    {
        public int Index { get; set; } = index;

        public string Name { get; set; } = name;
        public List<Node> Children { get; set; } = [];
    }

    internal class Branch(int indexOrigin, Node nodeAtOrigin)
    {
        // the index of the char in the line
        public int IndexOrigin { get; set; } = indexOrigin;

        public Node NodeAtOrigin { get; set; } = nodeAtOrigin;
    }


    internal static class PathCounter
    {
        public static long CountPaths(Node root, List<Node> nodes)
        {
            var paths = new Dictionary<Node, long>();
            foreach (var n in nodes)
            {
                paths[n] = 0;
            }
            paths[root] = 1;

            foreach (var node in nodes)
            {
                var pathsToParent = paths[node];

                foreach (var child in node.Children)
                {
                    paths[child] += pathsToParent;
                }
            }

            // count how many paths there are to each leaf
            long total = 0;
            foreach (var n in nodes)
            {
                if (n.Children.Count == 0)
                {
                    total += paths[n];
                }
            }
            return total;
        }
    }
}