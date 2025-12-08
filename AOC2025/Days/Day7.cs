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
            var root = new Node(0);

            foreach (var line in lines)
            {
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

                var newBranches = new List<Branch>();
                var newNodes = new List<Node>();
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
                        var n = newNodes.FirstOrDefault(n => n.Index == branch.IndexOrigin) ?? new Node(branch.IndexOrigin);
                        newNodes.Add(n);
                        branch.NodeAtOrigin.Children.Add(n);
                        newBranches.Add(new Branch(branch.IndexOrigin - 1, n));
                        newBranches.Add(new Branch(branch.IndexOrigin + 1, n));
                    }
                }

                branches.Clear();
                branches.AddRange(newBranches);
                newNodes.Clear();
                newBranches.Clear();
            }

            Console.WriteLine("solution is: " + DFSIterativeCountPaths(root));
        }

        private static int DFSIterativeCountPaths(Node root)
        {
            var visited = new HashSet<Node>();
            var stack = new Stack<Node>();
            var paths = 0;

            stack.Push(root);

            while (stack.Count > 0)
            {
                var node = stack.Pop();

                if (!visited.Contains(node))
                {
                    visited.Add(node);
                    if (node.Children.Count == 0)
                    {
                        // this is a leaf, we have found a path to a leaf
                        paths++;
                    }

                    foreach (var neighboringCity in node.Children)
                    {
                        stack.Push(neighboringCity);
                    }
                }
            }
            return paths;
        }
    }

    internal class Node(int index)
    {
        public int Index { get; set; } = index;
        public List<Node> Children { get; set; } = [];
    }

    internal class Branch(int indexOrigin, Node nodeAtOrigin)
    {
        // the index of the char in the line
        public int IndexOrigin { get; set; } = indexOrigin;

        public Node NodeAtOrigin { get; set; } = nodeAtOrigin;
    }

}