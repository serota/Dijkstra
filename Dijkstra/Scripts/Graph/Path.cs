using System;

namespace Dijkstra {
	public class Path : IComparable<Path> {
		public PrintableList<Node> Nodes { get; }
		public int Length { get; set; }

		public Path() {
			Nodes = new PrintableList<Node>();
			Length = int.MaxValue;
		}

		public int CompareTo(Path path) {
			return Length.CompareTo(path.Length);
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
}

