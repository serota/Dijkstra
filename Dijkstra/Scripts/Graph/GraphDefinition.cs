using System;

namespace Dijkstra {
	public class GraphDefinition {
		// Graph
		public bool IsVerbose { get; set; }
		public int NodeCount { get; set; }
		public int EdgeWeight { get; set; }
		public double EdgeProbability { get; set; }

		// Path
		public int StartNode { get; set; }
		public int EndNode { get; set; }
	}
}

