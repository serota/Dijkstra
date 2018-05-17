using System;

namespace Dijkstra {
	public class Node : IComparable<Node> {
		public string Name { get; set; }
		public bool Visited { get; set; }

		public Path Path { get; set; }

		public Node(string name) {
			Name = name;
			Visited = false;
			Path = new Path();
		}

		public int CompareTo(Node node) {
			if (Path.CompareTo(node.Path) == 0) {
				return Name.CompareTo(node.Name);
			}

			return Path.CompareTo(node.Path);
		}

		public override string ToString() {
			return Name;
		}
	}
}

