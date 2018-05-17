using System;

namespace Dijkstra {
	public class Edge {
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

