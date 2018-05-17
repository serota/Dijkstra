using System;

namespace Dijkstra {
	// Separate out static methods from the main class implementation.
	// There are two options: a Factory pattern non-static class, or a static extension class.
	// In general, if the methods are not related to the functionality of the class, I prefer to rip them out into a separate file.
	public static class WeightedGraphExtensions {
		public static WeightedGraph GenerateRandomWeightedGraph(GraphDefinition definition) {
			WeightedGraph graph = new WeightedGraph();
			Random rand = new Random();

			for (int i=0; i<definition.NodeCount; i++) {
				graph.Nodes.Add(new Node(WeightedGraphExtensions.NameForIndex(i)));
			}

			for(int i=0; i<definition.NodeCount; i++) {
				for(int j = i+1; j<definition.NodeCount; j++) {
					if(rand.NextDouble() < definition.EdgeProbability) {
						graph.Edges.Add(new Edge(graph.Nodes[i], graph.Nodes[j], rand.Next(1, definition.EdgeWeight)));
					}
				}
			}

			graph.Verbose = definition.IsVerbose;

			return graph;
		}

		public static WeightedGraph GenerateTestGraph() {
			WeightedGraph graph = new WeightedGraph();

			for (int i = 0; i < 8; i++) {
				graph.Nodes.Add(new Node(WeightedGraphExtensions.NameForIndex(i)));
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

		// NameFor is too vague for a method name. It should be readable without the parameter name.
		private static string NameForIndex(int index) {
			return index.ToString();

			/*
            if (index < 26) {
                return Char.ToString((char)((int)'A' + index));
            }

            return NameFor(index / 26 - 1) + NameFor(index % 26);
            */
		}
	}
}

