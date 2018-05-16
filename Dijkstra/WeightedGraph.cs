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
            Path path = new Path();

            if (s == f) {
                path.Nodes.Add(this.Nodes[s]);
                path.Length = 0;
                return path;
            }

            SortedSet<Node> unvisisted = new SortedSet<Node>();

            foreach(Node node in Nodes) {
                unvisisted.Add(node);
            }

            Node current = Nodes[s];
            current.Path.Nodes.Add(current);
            current.Path.Length = 0;

            HandleNode(current);
            unvisisted.Remove(current);

            //TODO: handle loop and figure out sort

            path = Nodes[f].Path;

            return path;
        }

        void HandleNode(Node current) {
            foreach (Edge edge in Edges) {
                int tentative = current.Path.Length + edge.Weight;

                if (edge.From == current && !edge.To.Visited) {
                    int previous = edge.To.Path.Length;

                    if (tentative < previous) {
                        edge.To.Path = current.Path;
                        edge.To.Path.Nodes.Add(edge.To);

                        edge.To.Path.Length = tentative;
                    }
                }
                else if (edge.To == current && !edge.From.Visited) {
                    int previous = edge.From.Path.Length;

                    if (tentative < previous) {
                        edge.From.Path = current.Path;
                        edge.From.Path.Nodes.Add(edge.From);

                        edge.From.Path.Length = tentative;
                    }
                }
            }

            current.Visited = true;
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
            char[] trims = { ',', ' ' };
            string output = "{";

            foreach(Object obj in this) {
                output += $"{obj.ToString()}, ";
            }

            output = output.TrimEnd(trims) + "}";

            return output;
        }
    }

    class Path {
        public PrintableList<Node> Nodes { get; }
        public int Length { get; set; }

        public Path() {
            Nodes = new PrintableList<Node>();
            Length = int.MaxValue;
        }

        public override string ToString() {
            return null; //TODO: path.tostring
        }
    }

    class Node {
        public string Name { get; set; }
        public bool Visited { get; set; }

        public Path Path { get; set; }

        public Node(string name) {
            Name = name;
            Visited = false;
            Path = new Path();
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
