using QuikGraph;
using QuikGraph.Algorithms.ConnectedComponents;

namespace AOC2025.Days
{
    public class Day8
    {
        public static void DoPuzzlePart1()
        {
            var lines = File.ReadAllLines("../../../Input/Day8Input.txt");
            UndirectedGraph<Coordinate, TaggedUndirectedEdge<Coordinate, double>> graph = CreateGraphOfInput(lines);

            var shortestEdges = graph.Edges.OrderBy(g => g.Tag).Take(1000).ToList();

            var sub = new UndirectedGraph<Coordinate, TaggedUndirectedEdge<Coordinate, double>>(allowParallelEdges: false);

            foreach (var e in shortestEdges)
            {
                sub.AddVertex(e.Source);
                sub.AddVertex(e.Target);
                sub.AddEdge(e);
            }

            var cc = new ConnectedComponentsAlgorithm<Coordinate, TaggedUndirectedEdge<Coordinate, double>>(sub);
            cc.Compute();

            var componentsBySize = cc.Components
                .GroupBy(kv => kv.Value)
                .Select(g => new
                {
                    ComponentId = g.Key,
                    Size = g.Count()
                })
                .OrderByDescending(c => c.Size)
                .ToList();

            var solution = componentsBySize[0].Size * componentsBySize[1].Size * componentsBySize[2].Size;

            Console.WriteLine("solution is: " + solution);
        }

        private static UndirectedGraph<Coordinate, TaggedUndirectedEdge<Coordinate, double>> CreateGraphOfInput(string[] lines)
        {
            var graph = new UndirectedGraph<Coordinate, TaggedUndirectedEdge<Coordinate, double>>(allowParallelEdges: false);
            var n = lines.Length;

            foreach (var line in lines)
            {
                var nums = line.Split(',');
                var coordinate = new Coordinate(lines.IndexOf(line), long.Parse(nums[0]), long.Parse(nums[1]), long.Parse(nums[2]));
                graph.AddVertex(coordinate);
            }

            var coordinates = graph.Vertices.ToList();

            for (int u = 0; u < n; u++)
            {
                for (int v = u + 1; v < n; v++)
                {
                    var distance = coordinates[u].Distance(coordinates[v]);
                    graph.AddEdge(new TaggedUndirectedEdge<Coordinate, double>(coordinates[u], coordinates[v], distance));
                }
            }

            return graph;
        }

        public static void DoPuzzlePart2()
        {
            var lines = File.ReadAllLines("../../../Input/Day8Input.txt");
            UndirectedGraph<Coordinate, TaggedUndirectedEdge<Coordinate, double>> graph = CreateGraphOfInput(lines);

            var orderedEdges = graph.Edges.OrderBy(g => g.Tag).ToList();

            var sub = new UndirectedGraph<Coordinate, TaggedUndirectedEdge<Coordinate, double>>(allowParallelEdges: false);

            var i = 0;
            var e = orderedEdges[i];
            while (graph.VertexCount > sub.VertexCount)
            {
                e = orderedEdges[i];
                sub.AddVertex(e.Source);
                sub.AddVertex(e.Target);
                sub.AddEdge(e);
                i++;
            }

            var solution = e.Source.x * e.Target.x;

            Console.WriteLine("solution is: " + solution);
        }
    }

    public class Coordinate : IComparable<Coordinate>
    {
        public int id {  get; set; }
        public long x { get; set; }
        public long y { get; set; }
        public long z { get; set; }

        public Coordinate(int id, long x, long y, long z)
        {
            this.id = id;
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public double Distance(Coordinate to)
        {
            return Math.Sqrt(Math.Pow(to.x - x, 2) + Math.Pow(to.y - y, 2) + Math.Pow(to.z - z, 2));
        }

        public int CompareTo(Coordinate? obj)
        {
            return id.CompareTo(obj?.id);
        }
    }
}