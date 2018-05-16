using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dijkstra {
    class WeightedGraph {
        public PrintableList<Node> Nodes { get; }
        public PrintableList<Edge> Edges { get; }

        public WeightedGraph() {
            Nodes = new PrintableList<Node>();
            Edges = new PrintableList<Edge>();
        }

        public Path ShortestPath(int s, int f) {
            Node current = Nodes[s];
            current.Path.Nodes.Add(current);
            current.Path.Length = 0;

            while (current != Nodes[f] && current.Path.Length < int.MaxValue) {
                //PrintUnvisited();
                //PrintVisited();
                //Console.WriteLine($"Considering: {current}");

                foreach (Edge edge in Edges) {
                    if (edge.From == current && !edge.To.Visited) {
                        CheckAndUpdate(edge.To, current, edge);
                    }
                    else if (edge.To == current && !edge.From.Visited) {
                        CheckAndUpdate(edge.From, current, edge);
                    }
                }

                current.Visited = true;
                current = GetNextUnvisited();
            }

            //Console.WriteLine("Done.");
            return Nodes[f].Path;
        }

        void CheckAndUpdate(Node x, Node current, Edge edge) {
            int previous = x.Path.Length;
            int tentative = current.Path.Length + edge.Weight;

            if (tentative < previous) {
                x.Path.Nodes.Clear();

                foreach (Node node in current.Path.Nodes) {
                    x.Path.Nodes.Add(node);
                }

                x.Path.Nodes.Add(x);
                x.Path.Length = tentative;
            }

            //Console.WriteLine($"{x.Path}\n");
        }

        Node GetNextUnvisited() {
            Node output = new Node("-SLUG-");

            foreach (Node node in Nodes) {
                if (!node.Visited && node.CompareTo(output) < 0) {
                    output = node;
                }
            }

            return output;
        }

        void PrintUnvisited() {
            char[] trimmings = { ',', ' ' };
            string output = "Unvisited: {";

            foreach (Node node in Nodes) {
                if (!node.Visited) {
                    output += $"{node}, ";
                }
            }

            output = output.TrimEnd(trimmings) + "}";
            Console.WriteLine(output);
        }

        void PrintVisited() {
            char[] trimmings = { ',', ' ' };
            string output = "Out: {";

            foreach (Node node in Nodes) {
                if (node.Visited) {
                    output += $"{node}, ";
                }
            }

            output = output.TrimEnd(trimmings) + "}";
            Console.WriteLine(output);
        }

        public override string ToString() {
            return $"Nodes: {Nodes.ToString()}\n\nEdges: {Edges.ToString()}\n";
        }

        public static WeightedGraph GenerateRandomWeightedGraph(int n, int maxWeight, double edgeProbability) {
            WeightedGraph graph = new WeightedGraph();
            Random rand = new Random();

            for (int i=0; i<n; i++) {
                graph.Nodes.Add(new Node(NameFor(i)));
            }

            for(int i=0; i<n; i++) {
                for(int j = i+1; j<n; j++) {
                    if(rand.NextDouble() < edgeProbability) {
                        graph.Edges.Add(new Edge(graph.Nodes[i], graph.Nodes[j], rand.Next(1, maxWeight)));
                    }
                }
            }

            return graph;
        }

        public static WeightedGraph GenerateTestGraph() {
            WeightedGraph graph = new WeightedGraph();

            for (int i = 0; i < 8; i++) {
                graph.Nodes.Add(new Node(NameFor(i)));
            }

            graph.Edges.Add(new Edge(graph.Nodes[0], graph.Nodes[1], 5));
            graph.Edges.Add(new Edge(graph.Nodes[0], graph.Nodes[2], 1));
            graph.Edges.Add(new Edge(graph.Nodes[0], graph.Nodes[5], 3));
            graph.Edges.Add(new Edge(graph.Nodes[0], graph.Nodes[6], 6));
            graph.Edges.Add(new Edge(graph.Nodes[1], graph.Nodes[5], 2));
            graph.Edges.Add(new Edge(graph.Nodes[2], graph.Nodes[3], 2));
            graph.Edges.Add(new Edge(graph.Nodes[2], graph.Nodes[6], 1));
            graph.Edges.Add(new Edge(graph.Nodes[3], graph.Nodes[4], 3));
            graph.Edges.Add(new Edge(graph.Nodes[3], graph.Nodes[6], 5));
            graph.Edges.Add(new Edge(graph.Nodes[3], graph.Nodes[7], 7));
            graph.Edges.Add(new Edge(graph.Nodes[4], graph.Nodes[7], 4));
            graph.Edges.Add(new Edge(graph.Nodes[5], graph.Nodes[6], 3));
            graph.Edges.Add(new Edge(graph.Nodes[6], graph.Nodes[7], 3));

            return graph;
        }

        public static string NameFor(int index) {
            return index.ToString();

            /*
            if (index < 26) {
                return Char.ToString((char)((int)'A' + index));
            }

            return NameFor(index / 26 - 1) + NameFor(index % 26);
            */
        }
    }

    class PrintableList<T> : List<T> {
        public override string ToString() {
            char[] trimmings = { ',', ' ' };
            string output = "{";

            foreach (Object obj in this) {
                output += $"{obj.ToString()}, ";
            }

            output = output.TrimEnd(trimmings) + "}";
            return output;
        }
    }

    class Path : IComparable<Path> {
        public PrintableList<Node> Nodes { get; }
        public int Length { get; set; }

        public Path() {
            Nodes = new PrintableList<Node>();
            Length = int.MaxValue;
        }

        public int CompareTo(Path path) {
            return this.Length.CompareTo(path.Length);
        }

        public override string ToString() {
            char[] trimmings = { '\n', '-', '>', ' ' };
            string output = "\n   ";
            int previous = 0;

            foreach (Node node in Nodes) {
                output += $"{node} ({node.Path.Length - previous})\n-> ";
                previous = node.Path.Length;
            }

            output = $"{output.TrimEnd(trimmings)}\nLength: {Length}";
            return output;
        }
    }

    class Node : IComparable<Node> {
        public string Name { get; set; }
        public bool Visited { get; set; }

        public Path Path { get; set; }

        public Node(string name) {
            Name = name;
            Visited = false;
            Path = new Path();
        }

        public int CompareTo(Node node) {
            if (this.Path.CompareTo(node.Path) == 0) {
                return this.Name.CompareTo(node.Name);
            }

            return this.Path.CompareTo(node.Path);
        }

        public override string ToString() {
            return Name;
        }
    }

    class Edge {
        public Node From { get; }
        public Node To { get; }
        public int Weight { get; }

        public Edge(Node from, Node to, int weight) {
            From = from;
            To = to;
            Weight = weight;
        }

        public override string ToString() {
            return $"({From}, {To}, {Weight})";
        }
    }
}
