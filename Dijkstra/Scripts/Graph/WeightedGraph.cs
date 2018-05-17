using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dijkstra {
    public class WeightedGraph {
        public PrintableList<Node> Nodes { get; }
        public PrintableList<Edge> Edges { get; }

        public bool Verbose { get; set; }

        public WeightedGraph() {
            this.Nodes = new PrintableList<Node>();
			this.Edges = new PrintableList<Edge>();
			this.Verbose = false;
        }

        public Path GetShortestPath(int s, int f) {
            Node current = Nodes[s];
            current.Path.Nodes.Add(current);
            current.Path.Length = 0;

			while (current != this.Nodes[f] && current.Path.Length < int.MaxValue) {
                if (Verbose) {
					Console.WriteLine($"Unvisited: {this.GetNodesString(true)}");
					Console.WriteLine($"Out: {this.GetNodesString(false)}");
                    Console.WriteLine($"Considering: {current}");
                }

				foreach (Edge edge in this.Edges) {
                    if (edge.From == current && !edge.To.Visited) {
                        CheckAndUpdate(edge.To, current, edge);
                    }
                    else if (edge.To == current && !edge.From.Visited) {
                        CheckAndUpdate(edge.From, current, edge);
                    }
                }

                current.Visited = true;
				current = this.GetNextUnvisited();
            }

            if (this.Verbose) {
                Console.WriteLine("Done.");
            }

			return this.Nodes[f].Path;
        }

        private void CheckAndUpdate(Node x, Node current, Edge edge) {
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

			if (this.Verbose) {
                Console.WriteLine($"{x.Path}\n");
            }
        }

        private Node GetNextUnvisited() {
            Node output = new Node("-SLUG-");

            foreach (Node node in Nodes) {
                if (!node.Visited && node.CompareTo(output) < 0) {
                    output = node;
                }
            }

            return output;
        }

        private string GetNodesString(bool unvisited) {
            char[] trimmings = { ',', ' ' };
            string output = "{";

            foreach (Node node in this.Nodes) {
                if (node.Visited ^ unvisited) {
                    output += $"{node}, ";
                }
            }

            output = output.TrimEnd(trimmings) + "}";
            return output;
        }

        public override string ToString() {
            return $"Nodes: {Nodes.ToString()}\n\nEdges: {Edges.ToString()}\n";
        }
    }
}
