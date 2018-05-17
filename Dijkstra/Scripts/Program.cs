using System;

namespace Dijkstra {
	class Program {
		public static void Main(string[] args) {
			InputStateMachine stateMachine = new InputStateMachine();
			GraphDefinition definition = stateMachine.BuildGraphDefinition();

			WeightedGraph graph = WeightedGraphExtensions.GenerateRandomWeightedGraph(definition);
			Console.WriteLine(graph);

			Path path = graph.GetShortestPath(definition.StartNode, definition.EndNode);
			Console.WriteLine();

			if (path.Nodes.Count == 0) {
				Console.WriteLine($"No path exists from {definition.StartNode} to {definition.EndNode}.");
			}
			else {
				Console.WriteLine($"The shortest path from {definition.StartNode} to {definition.EndNode} is: \n{path}");
			}

			Console.ReadLine();
		}
	}
}
